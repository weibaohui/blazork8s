using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorApp.Utils.Prometheus.Models;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using k8s;

namespace BlazorApp.Utils.Prometheus;

public class PrometheusMetricsParser
{
    const string MetricInfoRegex  = @"# (\w+) (\w*) (.*)";
    const string MeasurementRegex = "([^{\\ ]+)({.+})* ((?:-?\\d+(?:\\.\\d*)*)*(?:NaN)*)+ *(\\d*)*";

    public static async Task<List<IMetric>> ParseAsync(Stream rawMetricsStream)
    {
        var originalPosition = rawMetricsStream.Position;

        // Go to the beginning of the stream
        if (rawMetricsStream.CanSeek)
        {
            rawMetricsStream.Seek(0, SeekOrigin.Begin);
        }

        // Interpret the metrics
        var metrics = await InterpretRawMetricsStreamAsync(rawMetricsStream);

        // Reset stream to original position
        if (rawMetricsStream.CanSeek)
        {
            rawMetricsStream.Seek(originalPosition, SeekOrigin.Begin);
        }

        return metrics;
    }

    private static async Task<List<IMetric>> InterpretRawMetricsStreamAsync(Stream rawMetricsStream)
    {
        var metrics      = new List<IMetric>();
        var streamReader = new StreamReader(rawMetricsStream);

        Gauge       lastGauge        = null;
        MetricTypes currentMetricType      = MetricTypes.NotSpecified;
        var         done = false;
        //逐行进行处理
        var line = await streamReader.ReadLineAsync();
        if (string.IsNullOrWhiteSpace(line))
        {
            return metrics;
        }

        do
        {


            if (line.StartsWith('#'))
            {
                //TYPE 或者 HELP 行
                if (done == true)
                {//如果done，说明上一行的数据已经处理完了，现在碰到#开头，说明上一个大段已经处理完了。
                    //那么就把这个Gauge添加到列表中
                    // We're done, starting to interpret the next gauge
                    metrics.Add(lastGauge);
                    lastGauge = null;
                }

                lastGauge ??= new Gauge();

                var metricInfoMatch = Regex.Match(line, MetricInfoRegex);
                if (metricInfoMatch.Success == false)
                {
                    throw new Exception("Unable to parse the metric context information");
                }

                var scenario = metricInfoMatch.Groups[1].Value;
                switch (scenario)
                {
                    case "HELP":
                        lastGauge.Description = metricInfoMatch.Groups[3].Value;
                        break;
                    case "TYPE":
                        lastGauge.Name = metricInfoMatch.Groups[2].Value;
                        currentMetricType = Enum.Parse<MetricTypes>(metricInfoMatch.Groups[3].Value, ignoreCase: true);
                        break;
                }
                // 标记为未完成，此时只是读取了2行说明行
                done = false;
            }
            else
            {
                if (currentMetricType != MetricTypes.Gauge)
                {
                    Console.WriteLine($"Current metric type {currentMetricType} is not a gauge, ignoring.");
                }
                else
                {
                    if (!line.EndsWith("NaN") && !line.EndsWith("+Inf"))
                    {
                        //一行数据以+Inf正无穷大或者NaN表示，就不处理了
                        Console.WriteLine("line {0}", line);
                        GaugeMeasurement measurement = ParseMeasurement(line);
                        Console.WriteLine(
                            $"measurement {KubernetesJson.Serialize(measurement.Labels)} {measurement.Value} {measurement.Timestamp}");
                        lastGauge?.Measurements.Add(measurement);
                    }
                }
                //读完一行,标记为已完成。只有再碰到#开头的行，才可以将lastGauge添加到metrics
                done = true;
            }

            //处理完前面的行了，再读
            line = await streamReader.ReadLineAsync();
        } while (rawMetricsStream.Position <= rawMetricsStream.Length && string.IsNullOrWhiteSpace(line) == false);

        //如果是最后一行都处理完了，不会再有#开头的了，那么应该把这个加入到metircs中
        if (metrics.Find(f => f.Name == lastGauge.Name) == null)
        {
            metrics.Add(lastGauge);
        }

        return metrics;
    }

    /// <summary>
    /// process_virtual_memory_max_bytes 1.8446744073709552e+19
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static GaugeMeasurement ParseMeasurement(string line)
    {
        Console.WriteLine($"ParseMeasurement line {line}");

        var measurement  = new GaugeMeasurement();
        var regexOutcome = Regex.Match(line, MeasurementRegex);
        if (regexOutcome.Success == false)
        {
            throw new Exception(
                $"Measurement doesn't follow the required Regex statement '{MeasurementRegex}' for entry '{line}'");
        }


        Console.WriteLine($"ParseMeasurement regexOutcome = {regexOutcome}");
        // Assign value
        measurement.Value = ParseMetricValue(regexOutcome);

        // Get all contextual information
        ParseMetricLabels(regexOutcome, measurement);

        // Assign time, if available
        measurement.Timestamp = ParseMetricTimestamp(regexOutcome);

        return measurement;
    }

    private static double ParseMetricValue(Match regexOutcome)
    {
        var rawMetricValue = regexOutcome.Groups[3].Captures.FirstOrDefault()?.Value;
        Console.WriteLine($"ParseMetricValue rawMetricValue {rawMetricValue}");
        Console.WriteLine($"regexOutcome.Groups.Count {regexOutcome.Groups.Count}");

        if (regexOutcome.Groups.Count < 4 || string.IsNullOrWhiteSpace(rawMetricValue))
        {
            throw new Exception($"No metric value was found {rawMetricValue} {regexOutcome.Groups.Count}");
        }

        return double.Parse(rawMetricValue);
    }

    private static DateTimeOffset? ParseMetricTimestamp(Match regexOutcome)
    {
        var rawUnixTimeInSeconds = regexOutcome.Groups[4].Captures.FirstOrDefault()?.Value;
        if (regexOutcome.Groups.Count < 5 || string.IsNullOrWhiteSpace(rawUnixTimeInSeconds))
        {
            return null;
        }

        var unixTimeInSeconds = long.Parse(rawUnixTimeInSeconds);
        return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeInSeconds);
    }

    private static void ParseMetricLabels(Match regexOutcome, GaugeMeasurement measurement)
    {
        var rawLabels = regexOutcome.Groups[2].Value;

        // When there are no labels, return
        if (string.IsNullOrWhiteSpace(rawLabels))
        {
            return;
        }

        // Our capture group includes the leading { and trailing }, so we have to remove it
        rawLabels = rawLabels.Remove(0, 1);
        rawLabels = rawLabels.Remove(rawLabels.Length - 1);

        // Get every individual raw label
        foreach (var rawLabel in rawLabels.Split(','))
        {
            // Split label into information
            var splitLabelInfo = rawLabel.Split('=');

            // Add to the outcome
            measurement.Labels.Add(splitLabelInfo[0], splitLabelInfo[1].Replace("\"", ""));
        }
    }
}

using System;

namespace Extensions
{
    public static class AgeExtensions
    {
        /// <summary>
        /// 计算时间年龄
        /// </summary>
        /// <param name="time"></param>
        /// <returns>
        /// </returns>
        /// <example>
        /// <code>
        /// DateTime.Now.AddSeconds(-20).Age()//20s
        /// DateTime.Now.AddMinutes(-20).Age()//20m
        /// DateTime.Now.AddHours(-20).Age()//20h
        /// DateTime.Now.AddDays(-20).Age()//20d
        /// DateTime.Now.AddYears(-20).Age()//20y
        /// </code>
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Age(this DateTime time)
        {
            return DateTime.Now.Age(time);
        }

        public static string AgeFromUtc(this DateTime time)
        {
            var dateTime = DateTime.Parse(time.ToString("yyyy-MM-dd'T'HH:mm:ssZ"));
            return dateTime.Age();
        }


        public static string Age(this DateTime time, DateTime burnTime)
        {
            var s = time.Subtract(burnTime).TotalSeconds;
            var m = (int)time.Subtract(burnTime).TotalMinutes;
            var h = (int)time.Subtract(burnTime).TotalHours;
            var d = (int)time.Subtract(burnTime).TotalDays;
            var y =(int) s / 60 / 60 / 24 / 365;

            var ys = time.Subtract(burnTime).Seconds;
            var ym = time.Subtract(burnTime).Minutes;
            var yh = time.Subtract(burnTime).Hours;
            var str = s switch
            {
                < 0                                     => "0s",
                <= 60                                   => $"{s}s",
                > 60 when m > 0 & m < 60 & s % 60 == 0  => $"{m:0}m",
                > 60 when m > 0 & m < 60                => $"{m:0}m{ys}s",
                > 60 when h > 0 & h < 24 & m % 60 == 0  => $"{h:0}h",
                > 60 when h > 0 & h < 24                => $"{h:0}h{ym}m",
                > 60 when d > 0 & d < 365 & h % 24 == 0 => $"{d:0}d",
                > 60 when d > 0 & d < 365               => $"{d:0}d{yh}h",
                > 60 when y > 0 & d % 365 == 0          => $"{y:0}y",
                > 60 when y > 0                         => $"{y:0}y{d % 365:0}d",
                _                                       => throw new ArgumentOutOfRangeException()
            };
            return str;
        }
    }
}

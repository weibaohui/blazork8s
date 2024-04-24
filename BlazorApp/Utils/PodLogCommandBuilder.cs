using System;
using Extension;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PodLogCommandBuilder
{
    private static readonly ILogger<PodLogCommandBuilder> Logger = LoggingHelper<PodLogCommandBuilder>.Logger();
    private bool _allContainers = false;
    private string _command;
    private string _containerName;
    private bool _follow;
    private bool _ignoreErrors;
    private string _namespace;

    private string _podName;
    private string _podRunningTimeout;
    private bool _prefix;
    private bool _previous;
    private bool _showTimestamp;
    private string _since;
    private DateTime? _sinceTimestamp;
    private string _tail;


    public PodLogCommandBuilder Build()
    {
        _command = "";
        var extCmd = "";
        if (_follow)
            extCmd += " --follow=true ";
        if (_prefix)
            extCmd += " --prefix=true ";
        if (_previous)
            extCmd += " --previous=true ";
        if (_ignoreErrors)
            extCmd += " --ignore-errors=true ";
        if (_showTimestamp)
            extCmd += " --timestamps=true ";
        if (!_podRunningTimeout.IsNullOrEmpty())
            extCmd += $" --pod-running-timeout={_podRunningTimeout} ";
        if (!_tail.IsNullOrEmpty())
            extCmd += $" --tail={_tail} ";

        //设置了since并且不是指定
        if (!_since.IsNullOrEmpty() && _since != "specified") extCmd += $" --since={_since} ";

        //只设置了sinceTimestamp的情况
        if (_since.IsNullOrEmpty() && _sinceTimestamp != null)
            extCmd += $" --since-time='{new DateTimeOffset(_sinceTimestamp.Value):yyyy-MM-dd'T'HH:mm:ss.fffK}' ";

        //since设置为指定，同时也设置了sinceTimestamp的情况
        if (!_since.IsNullOrEmpty() && _since == "specified" && _sinceTimestamp != null)
            extCmd += $" --since-time='{new DateTimeOffset(_sinceTimestamp.Value):yyyy-MM-dd'T'HH:mm:ss.fffK}' ";

        //是否显示所有容器
        extCmd += _allContainers ? " --all-containers=true " : $" -c {_containerName}  ";

        _command = $"logs -n {_namespace} {_podName} {extCmd}";
        return this;
    }


    #region Getter Setter

    public string GetCommand()
    {
        return _command;
    }

    public PodLogCommandBuilder SetNamespace(string ns)
    {
        _namespace = ns;
        return this;
    }

    public PodLogCommandBuilder SetPodName(string podName)
    {
        _podName = podName;
        return this;
    }

    public PodLogCommandBuilder SetContainerName(string containerName)
    {
        _containerName = containerName;
        return this;
    }

    public PodLogCommandBuilder SetAllContainers(bool flag)
    {
        _allContainers = flag;
        return this;
    }

    public PodLogCommandBuilder SetShowTimestamp(bool showTimestamp)
    {
        _showTimestamp = showTimestamp;
        return this;
    }

    public PodLogCommandBuilder SetFollow(bool follow)
    {
        _follow = follow;
        return this;
    }

    public PodLogCommandBuilder SetPrefix(bool prefix)
    {
        _prefix = prefix;
        return this;
    }

    public PodLogCommandBuilder SetPrevious(bool previous)
    {
        _previous = previous;
        return this;
    }

    public PodLogCommandBuilder SetIgnoreErrors(bool ignoreErrors)
    {
        _ignoreErrors = ignoreErrors;
        return this;
    }

    public PodLogCommandBuilder SetSinceTimestamp(DateTime? sinceTimestamp)
    {
        _sinceTimestamp = sinceTimestamp;
        return this;
    }

    public PodLogCommandBuilder SetSince(string since)
    {
        _since = since;
        return this;
    }

    public PodLogCommandBuilder SetPodRunningTimeout(string timeout)
    {
        _podRunningTimeout = timeout;
        return this;
    }

    public PodLogCommandBuilder SetTail(string tail)
    {
        _tail = tail;
        return this;
    }

    #endregion
}

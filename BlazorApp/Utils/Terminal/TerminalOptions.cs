#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace BlazorApp.Utils.Terminal;


public class TerminalOptions
{
    /// <summary>Gets or sets the terminal name.</summary>
    public string? Name { get; set; }

    /// <summary>Gets or sets the number of initial rows.</summary>
    public int Rows { get; set; } = 20;

    /// <summary>Gets or sets the number of initial columns.</summary>
    public int Cols { get; set; } = 100;

    /// <summary>
    /// Gets or sets the working directory for the spawned process.
    /// </summary>
    public string Cwd { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>Gets or sets the path to the process to be spawned.(zsh || bash || sh)</summary>
    public string App { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? Path.Combine(System.Environment.SystemDirectory, "cmd.exe")
        : "bash";

    /// <summary>
    /// Gets or sets the command line arguments to the process.
    /// </summary>
    public string[] CommandLine { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets a value indicating whether command line arguments must be quoted.
    /// <c>false</c>, the default, means that the arguments must be quoted and quotes inside escaped then concatenated with spaces.
    /// <c>true</c> means that the arguments must not be quoted and just concatenated with spaces.
    /// </summary>
    public bool VerbatimCommandLine { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether WinPty should be forced as the windows backend even on systems where ConPty is available.
    /// </summary>
    public bool ForceWinPty { get; set; }

    /// <summary>Gets or sets the process' environment variables.</summary>
    public IDictionary<string, string> Environment { get; set; } =
        new Dictionary<string, string>();
}

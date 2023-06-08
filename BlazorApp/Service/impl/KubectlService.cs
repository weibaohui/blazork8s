using System;
using System.Diagnostics;
using System.IO;

namespace BlazorApp.Service.impl;

public class KubectlService : IKubectlService
{
    public string Apply(string yaml)
    {
        if (string.IsNullOrWhiteSpace(yaml))
        {
            return string.Empty;
        }

        //yaml 存为文件
        var    guid = Guid.NewGuid().ToString();
        string path = $"{guid}.yaml";
        Console.WriteLine($"文件名称{path}");
        File.WriteAllText(path, yaml);

        Process process = new Process();
        process.StartInfo.FileName               = "kubectl";          // 设置要执行的 kubectl 命令
        process.StartInfo.Arguments              = $"apply -f {path}"; // 设置命令参数
        process.StartInfo.UseShellExecute        = false;              // 不使用操作系统的 shell
        process.StartInfo.RedirectStandardOutput = true;               // 重定向输出
        process.Start();                                               // 启动进程
        string output = process.StandardOutput.ReadToEnd();            // 读取输出
        process.WaitForExit();                                         // 等待进程退出
        Console.WriteLine(output);
        File.Delete(path);
        return output; // 输出命令输出结果
    }
}

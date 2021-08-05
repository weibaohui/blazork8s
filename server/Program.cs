using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using server.Utils;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            ServiceHelper.Services = host.Services;
            // var watcher = ServiceHelper.Services.GetService<Watcher>();
            // if (watcher != null) watcher.StartWatch();
            host.Run();
        }


        public static void Exec()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName               = "bash";
            cmd.StartInfo.RedirectStandardInput  = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow         = true;
            cmd.StartInfo.UseShellExecute        = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("kubectl version");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
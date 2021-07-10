using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using server.Service;
using server.Service.K8s;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Exec();
            LinkK8s();
            CreateHostBuilder(args).Build().Run();
        }

        private static void LinkK8s()
        {
            var cli = Kubectl.Instance.Client();
            new PodWatcher().StartWatch(cli);
            new NodeWatcher().StartWatch(cli);
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

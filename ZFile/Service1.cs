using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ZFile
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        //下为将 Windows 服务作为控制台应用运行，详见https://learn.microsoft.com/zh-cn/dotnet/framework/windows-services/how-to-debug-windows-service-applications#how-to-run-a-windows-service-as-a-console-application

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            RunCMD.Run(args);    //执行命令


        }



        protected override void OnStop()
        {

        }

        class RunCMD
        {
            public static void Run(string[] args)
            {
                // 设置控制台程序的启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "diskpart.exe";
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;

                // 启动控制台程序
                Process process = new Process();
                process.StartInfo = startInfo;
                process.OutputDataReceived += Process_OutputDataReceived;
                process.Start();
                process.BeginOutputReadLine();
                //process.WaitForExit();
            }

            private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                // 输出控制台信息
                Console.WriteLine(e.Data);
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ZFile
{
    public partial class Service1 : ServiceBase
    {
        private static Process process;
        private int processId;
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
            //读取配置

            GetConfig.GetZConfig();

            // 设置控制台程序的启动信息
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = $"{GetConfig.JDKPath} -Dfile.encoding=utf-8 -jar -Dserver.port={GetConfig.ZFilePort} {GetConfig.ZFilePath}";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            // 启动控制台程序
            Process process = new Process();
            process.StartInfo = startInfo;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            processId = process.Id;
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "processid.txt");
            File.WriteAllText(filePath, processId.ToString());

            Console.WriteLine("PID written to file: {0}", filePath);
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // 输出控制台信息
            Console.WriteLine(e.Data);

        }



        protected override void OnStop()
        {
            Stop_Java();
        }
        private static void Stop_Java()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "processid.txt");
            int pid;

            try
            {
                // 从文件中读取PID
                pid = int.Parse(File.ReadAllText(filePath));
            }
            catch (Exception e)
            {
                Console.WriteLine("无法读取PID：" + e.Message);
                return;
            }

            try
            {
                // 根据PID获取进程对象
                Process process = Process.GetProcessById(pid);

                // 结束进程
                process.Kill();

                Console.WriteLine("进程已结束");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("找不到对应的进程");
            }
            catch (Exception e)
            {
                Console.WriteLine("无法结束进程：" + e.Message);
            }
        }
    }
    static class GetConfig
    {
        public static string ZFilePath = "";
        public static string JDKPath = "";
        public static string ZFilePort = "";
        public static void GetZConfig()
        {
            Type entryType = Assembly.GetEntryAssembly().EntryPoint.DeclaringType;
            if (entryType != null && entryType.BaseType == typeof(ServiceBase))
            {
                // 当前正在运行的是Windows服务
                string configFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ZFile.cfg");
                string[] lines = File.ReadAllLines(configFile);

                foreach (string line in lines)
                {
                    if (line.StartsWith("ZFilePath="))
                    {
                        ZFilePath = line.Split('=')[1];
                    }
                    if (line.StartsWith("JDK8Path="))
                    {
                        JDKPath = line.Split('=')[1];
                    }
                    if (line.StartsWith("ZFilePort="))
                    {
                        ZFilePort = line.Split('=')[1];
                    }
                    break;
                }
            }
            else
            {
                // 当前正在控制台中运行
                string configFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ZFile.cfg");
                string[] lines = File.ReadAllLines(configFile);

                foreach (string line in lines)
                {
                    if (line.StartsWith("ZFilePath="))
                    {
                        ZFilePath = line.Split('=')[1];
                    }
                    else if (line.StartsWith("JDK8Path="))
                    {
                        JDKPath = line.Split('=')[1];
                    }
                    else if (line.StartsWith("ZFilePort="))
                    {
                        ZFilePort = line.Split('=')[1];
                    }
                }
            }

            Console.WriteLine($"ZFilePath = {ZFilePath}");
            Console.WriteLine($"JavaPath = {JDKPath}");
            Console.WriteLine($"RunningPort = {ZFilePort}");
        }
    }
}

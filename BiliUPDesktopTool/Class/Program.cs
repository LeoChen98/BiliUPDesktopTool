using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliUPDesktopTool
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (RunningInstance() != null && Environment.CommandLine.ToLower().IndexOf("-m") == -1)
            {
                System.Windows.Forms.MessageBox.Show("已经有一个实例在运行！");
                Environment.Exit(0);
            }

            //初始化公共变量
            Bas.skin = new Skin();
            Bas.settings = new Settings();

            if (Bas.settings.IsFirstRun)
            {
                //首次运行执行
                LisenceWindow LW = new LisenceWindow();
                if (LW.ShowDialog() != true)
                {
                    Environment.Exit(-2);
                }
                Bas.settings.IsFirstRun = false;
            }

            Bas.account = new Account();
            Bas.biliupdata = new BiliUPData();
            Bas.notifyIcon = new NotifyIconHelper();
            Bas.update = new Update();

            if(Environment.CommandLine.ToLower().IndexOf("-s") == -1)
            {
                Bas.notifyIcon.ShowToolTip("工具主程序已最小化到托盘，调整数据窗口、设置和退出程序请通过图盘图标的菜单。");
            }

            Application app = new Application();
            app.Run(new DesktopWindow());
        }

        /// <summary>
        /// 查找相同进程
        /// </summary>
        /// <returns>相同的进程</returns>
        public static System.Diagnostics.Process RunningInstance()
        {
            System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processes) //查找相同名称的进程
            {
                if (process.Id != current.Id) //忽略当前进程
                {
                    if (process.ProcessName == current.ProcessName)
                    {
                        //确认相同进程的程序运行位置是否一样.
                        if (process.MainModule.FileName == current.MainModule.FileName)
                        { //Return the other process instance.
                            return process;
                        }
                    }
                }
            } //No other instance was found, return null.
            return null;
        }
    }
}

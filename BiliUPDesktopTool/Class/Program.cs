using System;
using System.Threading;
using System.Windows;

namespace BiliUPDesktopTool
{
    internal class Program
    {
        #region Public Methods

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

            if (Bas.settings.IsAutoCheckUpdate) Bas.update.CheckUpdate(false);

            if (Environment.CommandLine.ToLower().IndexOf("-s") == -1)
            {
                Bas.notifyIcon.ShowToolTip("工具主程序已最小化到托盘，调整数据窗口、设置和退出程序请通过图盘图标的菜单。");
            }

            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Thread DesktopWnd_Monitor = new Thread(DesktopWnd_Monitor_Handler);
            DesktopWnd_Monitor.IsBackground = false;
            DesktopWnd_Monitor.Start(app);

            app.Run();
        }


        private static void DesktopWnd_Monitor_Handler(object e)
        {
            Application app = e as Application;

            DesktopWindow dw = null;
            app.Dispatcher.Invoke(() =>
            {
                dw = new DesktopWindow();
                dw.Show();
            });

            while (true)
            {
                try
                {
                    if (dw == null || !dw.IsVisible)
                    {
                        app.Dispatcher.Invoke(() =>
                        {
                            dw = new DesktopWindow();
                            dw.Show();
                        });
                    }
                }
                catch { }
                Thread.Sleep(1000);
            }
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

        #endregion Public Methods
    }
}
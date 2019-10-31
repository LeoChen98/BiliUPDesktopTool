using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;

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

            //用户统计
            Bas.User_Statistics();

            _ = NotifyIconHelper.Instance;

            if (Settings.Instance.IsFirstRun)
            {
                //首次运行执行
                if (WindowsManager.Instance.GetWindow<LisenceWindow>().ShowDialog() != true)
                {
                    Environment.Exit(-2);
                }
                Settings.Instance.IsFirstRun = false;
            }

            //初始化WPF为20帧
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 25 });

            if (Settings.Instance.IsAutoCheckUpdate) Update.Instance.CheckUpdate(false);

            if (Environment.CommandLine.ToLower().IndexOf("-s") == -1)
            {
                WindowsManager.Instance.GetWindow<MainWindow>().Show();
            }

            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Thread DesktopWnd_Monitor = new Thread(DesktopWnd_Monitor_Handler);
            DesktopWnd_Monitor.IsBackground = false;
            DesktopWnd_Monitor.Start(app);

            app.Run();
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

        #region Private Methods

        private static void DesktopWnd_Monitor_Handler(object e)
        {
            Application app = e as Application;

            app.Dispatcher.Invoke(() =>
            {
                WindowsManager.Instance.GetWindow<DesktopWindow>().Show();
            });

            while (true)
            {
                try
                {
                    if ((!WindowsManager.Instance.GetWindow<DesktopWindow>().IsVisible) && Settings.Instance.IsDataViewerDisplay)
                    {
                        app.Dispatcher.Invoke(() =>
                        {
                            WindowsManager.Instance.GetWindow<DesktopWindow>().Show();
                        });
                    }
                    else if (!Settings.Instance.IsDataViewerDisplay)
                    {
                        app.Dispatcher.Invoke(() =>
                        {
                            WindowsManager.Instance.GetWindow<DesktopWindow>().Close();
                        });
                    }
                }
                catch { }
                Thread.Sleep(1000);
            }
        }

        #endregion Private Methods
    }
}
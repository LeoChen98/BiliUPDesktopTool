using System;
using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region Public Methods

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

        /// <summary>
        /// 启动动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (RunningInstance() != null && Environment.CommandLine.ToLower().IndexOf("-m") == -1)
            {
                System.Windows.Forms.MessageBox.Show("已经有一个实例在运行！");
                Environment.Exit(0);
            }

            //初始化公共变量
            Bas.skin = new Skin();
            Bas.settings = new Settings();
            Bas.account = new Account();
            Bas.biliupdata = new BiliUPData();
            Bas.notifyIcon = new NotifyIconHelper();

            if (Bas.settings.IsFirstRun)
            {
                //首次运行执行

                Bas.settings.IsFirstRun = false;
            }
        }

        #endregion Private Methods
    }
}
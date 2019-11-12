using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();

            Statistics_Box.Children.Add(new StatisticsPage());

            MsgBoxPushHelper.PushMsg += MsgBoxPushHelper_PushMsg;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 广播信息
        /// </summary>
        /// <param name="msg">信息内容</param>
        public void NotifyMsg(string msg)
        {
        }

        public void ToTab(int id)
        {
            switch (id)
            {
                case 0:
                    (FindResource("Btn_Home_MouseUp") as Storyboard).Begin();
                    break;

                case 1:
                    (FindResource("Btn_Statistics_MouseUp") as Storyboard).Begin();
                    break;

                case 2:
                    (FindResource("Btn_Nore_MouseUp") as Storyboard).Begin();
                    break;

                default:
                    break;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void Btn_Close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Statistics_Box.Children.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                WinAPIHelper.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
            Close();
        }

        private void Control_Box_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MsgBoxPushHelper_PushMsg(string msg, MsgBoxPushHelper.MsgType type = MsgBoxPushHelper.MsgType.Info)
        {
            if (IsActive && IsVisible)
            {
                msgbox.Show(msg);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MsgBoxPushHelper.PushMsg -= MsgBoxPushHelper_PushMsg;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        #endregion Private Methods
    }
}
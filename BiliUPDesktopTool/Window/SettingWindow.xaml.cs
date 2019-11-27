using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        #region Public Constructors

        public SettingWindow()
        {
            InitializeComponent();

            MsgBoxPushHelper.PushMsg += MsgBoxPushHelper_PushMsg;
        }

        #endregion Public Constructors

        #region Private Methods

        private void BTN_AutoRun_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key != null && key.GetValue("BiliUPDesktopTool") != null)
            {
                key.DeleteValue("BiliUPDesktopTool");
            }
            else
            {
                key.SetValue("BiliUPDesktopTool", "\"" + Process.GetCurrentProcess().MainModule.FileName + "\" -s");
            }
            SetAutoRunShow();
        }

        private void BTN_SetDesktopWndPos_Click(object sender, RoutedEventArgs e)
        {
            WindowsManager.Instance.GetWindow<DesktopWindowSetter>().Show();
        }

        private void MsgBoxPushHelper_PushMsg(string msg, Action command, MsgBoxPushHelper.MsgType type = MsgBoxPushHelper.MsgType.Info)
        {
            if (IsActive && IsVisible)
            {
                msgbox.Show(msg, command, type);
            }
        }

        private void SetAutoRunShow()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key != null && key.GetValue("BiliUPDesktopTool") != null)
            {
                BTN_AutoRun.Content = "取消开机启动";
            }
            else
            {
                BTN_AutoRun.Content = "设置为开机启动";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetAutoRunShow();
        }

        #endregion Private Methods
    }
}
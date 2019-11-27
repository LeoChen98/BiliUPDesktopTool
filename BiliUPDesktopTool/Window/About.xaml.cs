using System;
using System.Diagnostics;
using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        #region Public Constructors

        public About()
        {
            InitializeComponent();

            MsgBoxPushHelper.PushMsg += MsgBoxPushHelper_PushMsg;
        }

        #endregion Public Constructors

        #region Private Methods

        private void BTN_CheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update.Instance.CheckUpdate();
        }

        private void BTN_ShowLisence_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer.exe", "https://www.zhangbudademao.com/BiliUPDesktopTool/Lisence.html");
        }

        private void MsgBoxPushHelper_PushMsg(string msg, Action command, MsgBoxPushHelper.MsgType type = MsgBoxPushHelper.MsgType.Info)
        {
            if (IsActive && IsVisible)
            {
                msgbox.Show(msg, command, type);
            }
        }

        #endregion Private Methods
    }
}
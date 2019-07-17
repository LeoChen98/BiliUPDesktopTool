using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 系统托盘类
    /// </summary>
    internal class NotifyIconHelper
    {
        #region Private Fields

        private About about;

        private NotifyIcon NI;
        private SettingWindow OSetter;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化系统托盘
        /// </summary>
        public NotifyIconHelper()
        {
            NI = new NotifyIcon()
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                Text = "B站up主桌面工具",
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip(),
            };
            NI.ContextMenuStrip.Items.AddRange(GetMenuItems(NI));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 气泡提示
        /// </summary>
        /// <param name="msg">必选，消息</param>
        /// <param name="title">可选，标题</param>
        /// <param name="time">可选，显示时间</param>
        /// <param name="icon">可选，图标</param>
        public void ShowToolTip(string msg, string title = "B站up主桌面工具", int time = 5000, ToolTipIcon icon = ToolTipIcon.Info)
        {
            NI.BalloonTipIcon = icon;
            NI.BalloonTipText = msg;
            NI.BalloonTipTitle = title;
            NI.ShowBalloonTip(time);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// 获得菜单对象
        /// </summary>
        /// <returns></returns>
        private ToolStripItemCollection GetMenuItems(NotifyIcon ni)
        {
            List<ToolStripItem> m = new List<ToolStripItem>();

            ToolStripSeparator MI_Separator1 = new ToolStripSeparator(), MI_Separator2 = new ToolStripSeparator(), MI_Separator3 = new ToolStripSeparator();

            ToolStripMenuItem MI_ShowMainWindow = new ToolStripMenuItem() { Text = "显示主窗口" };
            MI_ShowMainWindow.Click += MI_ShowMainWindow_Click;
            m.Add(MI_ShowMainWindow);

            m.Add(MI_Separator1);

            ToolStripMenuItem MI_DesktopWndPosSetting = new ToolStripMenuItem() { Text = "设置桌面挂件位置" };
            MI_DesktopWndPosSetting.Click += MI_DesktopWndPosSetting_Click;
            m.Add(MI_DesktopWndPosSetting);

            m.Add(MI_Separator2);

            ToolStripMenuItem MI_OverallSetting = new ToolStripMenuItem() { Text = "全局设置" };
            MI_OverallSetting.Click += MI_OverallSetting_Click;
            m.Add(MI_OverallSetting);

            ToolStripMenuItem MI_DataDisplaySetting = new ToolStripMenuItem() { Text = "数据展示设置" };
            MI_DataDisplaySetting.Click += MI_DataDisplaySetting_Click;
            m.Add(MI_DataDisplaySetting);

            ToolStripMenuItem MI_CheckUpdate = new ToolStripMenuItem() { Text = "检查更新" };
            MI_CheckUpdate.Click += MI_CheckUpdate_Click;
            m.Add(MI_CheckUpdate);

            ToolStripMenuItem MI_About = new ToolStripMenuItem() { Text = "关于" };
            MI_About.Click += MI_About_Click;
            m.Add(MI_About);

            m.Add(MI_Separator3);

            ToolStripMenuItem MI_Exit = new ToolStripMenuItem() { Text = "退出" };
            MI_Exit.Click += MI_Exit_Click;
            m.Add(MI_Exit);

            return new ToolStripItemCollection(ni.ContextMenuStrip, m.ToArray());
        }

        private void MI_About_Click(object sender, EventArgs e)
        {
            about = (about == null || about.IsVisible == false) ? new About() : about;
            about.Show();
        }

        private void MI_CheckUpdate_Click(object sender, EventArgs e)
        {
            Bas.update.CheckUpdate();
        }

        private void MI_DataDisplaySetting_Click(object sender, EventArgs e)
        {
            if (Bas.MainWindow == null || !Bas.MainWindow.IsVisible)
            {
                Bas.MainWindow = new MainWindow();
                Bas.MainWindow.Show();
            }
            Bas.MainWindow.ToTab(1);
        }

        private void MI_DesktopWndPosSetting_Click(object sender, EventArgs e)
        {
            if (Bas.desktopwindowsetter == null || !Bas.desktopwindowsetter.IsVisible)
            {
                Bas.desktopwindowsetter = new DesktopWindowSetter();
                Bas.desktopwindowsetter.Show();
            }
            else
            {
                Bas.desktopwindowsetter.Activate();
                Bas.desktopwindowsetter.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void MI_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MI_OverallSetting_Click(object sender, EventArgs e)
        {
            if (OSetter == null || !OSetter.IsVisible)
            {
                OSetter = new SettingWindow();
                OSetter.Show();
            }
            else
            {
                OSetter.Activate();
                OSetter.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void MI_ShowMainWindow_Click(object sender, EventArgs e)
        {
            if (Bas.MainWindow == null || !Bas.MainWindow.IsVisible)
            {
                Bas.MainWindow = new MainWindow();
                Bas.MainWindow.Show();
            }
        }

        #endregion Private Methods
    }
}
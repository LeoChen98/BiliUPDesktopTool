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

        private static NotifyIconHelper instance;
        private NotifyIcon NI;

        #endregion Private Fields

        #region Private Constructors

        /// <summary>
        /// 初始化系统托盘
        /// </summary>
        private NotifyIconHelper()
        {
            NI = new NotifyIcon()
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                Text = "B站up主桌面工具",
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip(),
            };
            NI.ContextMenuStrip.Items.AddRange(GetMenuItems(NI));

            NI.DoubleClick += NI_DoubleClick;
        }

        #endregion Private Constructors

        #region Public Properties

        public static NotifyIconHelper Instance
        {
            get
            {
                if (instance == null) instance = new NotifyIconHelper();
                return instance;
            }
        }

        #endregion Public Properties

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
            MI_ShowMainWindow.Font = new System.Drawing.Font(MI_ShowMainWindow.Font, System.Drawing.FontStyle.Bold);
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
            WindowsManager.Instance.GetWindow<About>().Show();
            ToastHelper.Instance.Notify();
        }

        private void MI_CheckUpdate_Click(object sender, EventArgs e)
        {
            Update.Instance.CheckUpdate();
        }

        private void MI_DataDisplaySetting_Click(object sender, EventArgs e)
        {
            WindowsManager.Instance.GetWindow<MainWindow>().Show();
            WindowsManager.Instance.GetWindow<MainWindow>().ToTab(1);
        }

        private void MI_DesktopWndPosSetting_Click(object sender, EventArgs e)
        {
            if (!WindowsManager.Instance.GetWindow<DesktopWindowSetter>().IsVisible)
            {
                WindowsManager.Instance.GetWindow<DesktopWindowSetter>().Show();
            }
            else
            {
                WindowsManager.Instance.GetWindow<DesktopWindowSetter>().Activate();
                WindowsManager.Instance.GetWindow<DesktopWindowSetter>().WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void MI_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MI_OverallSetting_Click(object sender, EventArgs e)
        {
            if (!WindowsManager.Instance.GetWindow<SettingWindow>().IsVisible)
            {
                WindowsManager.Instance.GetWindow<SettingWindow>().Show();
            }
            else
            {
                WindowsManager.Instance.GetWindow<SettingWindow>().Activate();
                WindowsManager.Instance.GetWindow<SettingWindow>().WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void MI_ShowMainWindow_Click(object sender, EventArgs e)
        {
            WindowsManager.Instance.GetWindow<MainWindow>().Show();
        }

        private void NI_DoubleClick(object sender, EventArgs e)
        {
            WindowsManager.Instance.GetWindow<MainWindow>().Show();
        }

        #endregion Private Methods
    }
}
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

        private NotifyIcon NI;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化系统托盘
        /// </summary>
        public NotifyIconHelper()
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.AddRange(GetMenuItems());

            NI = new NotifyIcon()
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                Text = "B站up主桌面工具",
                Visible = true,
                ContextMenu = menu
            };
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
        /// 获得菜单数组
        /// </summary>
        /// <returns></returns>
        private MenuItem[] GetMenuItems()
        {
            List<MenuItem> r = new List<MenuItem>();

            MenuItem MI_Exit = new MenuItem() { Text = "退出" };
            MI_Exit.Click += MI_Exit_Click;
            r.Add(MI_Exit);

            return r.ToArray();
        }

        private void MI_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion Private Methods
    }
}
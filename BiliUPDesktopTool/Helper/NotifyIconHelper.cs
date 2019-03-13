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
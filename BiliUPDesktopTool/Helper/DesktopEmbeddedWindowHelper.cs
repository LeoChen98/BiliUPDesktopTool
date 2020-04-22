using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 桌面窗体嵌入类
    /// </summary>
    internal class DesktopEmbeddedWindowHelper
    {
        #region Public Methods

        /// <summary>
        /// 将窗体嵌入桌面
        /// </summary>
        /// <param name="hWndC">窗体句柄</param>
        public static void DesktopEmbedWindow(IntPtr hWndC)
        {
            IntPtr pWndP = WinAPIHelper.FindWindow("Progman", null);
            IntPtr pWnd = WinAPIHelper.FindWindowEx(pWndP, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            if (pWnd != IntPtr.Zero)
            {
                WinAPIHelper.SendMessage(pWndP, 0x52c, IntPtr.Zero, IntPtr.Zero);
            }

            IntPtr pWndA = WinAPIHelper.FindWindow("WorkerW", null);
            pWnd = WinAPIHelper.FindWindowEx(pWndA, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            while (pWnd == IntPtr.Zero)
            {
                pWndA = WinAPIHelper.FindWindowEx(IntPtr.Zero, pWndA, "WorkerW", null);
                pWnd = WinAPIHelper.FindWindowEx(pWndA, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            }
            pWndA = WinAPIHelper.FindWindowEx(IntPtr.Zero, pWndA, "WorkerW", null);
            pWnd = WinAPIHelper.FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);

            if (pWndA != IntPtr.Zero)
            {
                WinAPIHelper.SetParent(hWndC, pWndA);
                DisableAltTab(hWndC);
            }
            else System.Windows.Forms.MessageBox.Show("嵌入桌面失败！");
        }

        /// <summary>
        /// 将窗体嵌入桌面
        /// </summary>
        /// <param name="window">WPF窗体实例</param>
        public static void DesktopEmbedWindow(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            IntPtr pWndP = WinAPIHelper.FindWindow("Progman", null);
            IntPtr pWnd = WinAPIHelper.FindWindowEx(pWndP, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            if (pWnd != IntPtr.Zero)
            {
                WinAPIHelper.SendMessage(pWndP, 0x52c, IntPtr.Zero, IntPtr.Zero);
            }

            IntPtr pWndA = WinAPIHelper.FindWindow("WorkerW", null);
            pWnd = WinAPIHelper.FindWindowEx(pWndA, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            while (pWnd == IntPtr.Zero)
            {
                pWndA = WinAPIHelper.FindWindowEx(IntPtr.Zero, pWndA, "WorkerW", null);
                pWnd = WinAPIHelper.FindWindowEx(pWndA, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            }
            pWndA = WinAPIHelper.FindWindowEx(IntPtr.Zero, pWndA, "WorkerW", null);
            pWnd = WinAPIHelper.FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);

            if (pWndA != IntPtr.Zero)
            {
                WinAPIHelper.SetParent(hWnd, pWndA);
                DisableAltTab(hWnd);
            }
            else System.Windows.Forms.MessageBox.Show("嵌入桌面失败！");
        }

        /// <summary>
        /// 将窗体嵌入桌面
        /// </summary>
        /// <param name="window">winform窗体实例</param>
        public static void DesktopEmbedWindow(Form window)
        {
            IntPtr pWndP = WinAPIHelper.FindWindow("Progman", null);
            IntPtr pWnd = WinAPIHelper.FindWindowEx(pWndP, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            if (pWnd != IntPtr.Zero)
            {
                WinAPIHelper.SendMessage(pWndP, 0x52c, IntPtr.Zero, IntPtr.Zero);
            }

            IntPtr pWndA = WinAPIHelper.FindWindow("WorkerW", null);
            pWnd = WinAPIHelper.FindWindowEx(pWndA, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            while (pWnd == IntPtr.Zero)
            {
                pWndA = WinAPIHelper.FindWindowEx(IntPtr.Zero, pWndA, "WorkerW", null);
                pWnd = WinAPIHelper.FindWindowEx(pWndA, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            }
            pWndA = WinAPIHelper.FindWindowEx(IntPtr.Zero, pWndA, "WorkerW", null);
            pWnd = WinAPIHelper.FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);

            if (pWndA != IntPtr.Zero)
            {
                WinAPIHelper.SetParent(window.Handle, pWndA);
                DisableAltTab(window.Handle);
            }
            else System.Windows.Forms.MessageBox.Show("嵌入桌面失败！");
        }

        /// <summary>
        /// 注销Alt+Tab显示
        /// </summary>
        /// <param name="hWnd"></param>
        public static void DisableAltTab(IntPtr hWnd)
        {
            IntPtr WndLong = WinAPIHelper.GetWindowLongPtr(hWnd, -20);
            WinAPIHelper.SetWindowLongPtr(hWnd, -20, new IntPtr(WndLong.ToInt64() | 0x00000080));
        }

        #endregion Public Methods
    }
}
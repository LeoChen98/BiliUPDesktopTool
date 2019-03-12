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

            if (pWndA != IntPtr.Zero) WinAPIHelper.SetParent(hWndC, pWndA);
            else System.Windows.Forms.MessageBox.Show("嵌入桌面失败！");
        }

        /// <summary>
        /// 将窗体嵌入桌面
        /// </summary>
        /// <param name="window">WPF窗体实例</param>
        public static void DesktopEmbedWindow(Window window)
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

            if (pWndA != IntPtr.Zero) WinAPIHelper.SetParent(new WindowInteropHelper(window).Handle, pWndA);
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

            if (pWndA != IntPtr.Zero) WinAPIHelper.SetParent(window.Handle, pWndA);
            else System.Windows.Forms.MessageBox.Show("嵌入桌面失败！");
        }

        #endregion Public Methods
    }
}
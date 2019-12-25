using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ToastCore.Notification;

namespace BiliUPDesktopTool
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("B31C3B29-E506-4603-9F46-CF7106B77116"), ComVisible(true)]
    public class ToastManager : NotificationService
    {
        #region Public Methods

        public override void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId)
        {
            base.OnActivated(arguments, userInput, appUserModelId);
        }

        #endregion Public Methods
    }

    internal class ToastHelper
    {
        #region Private Fields

        private static ToastHelper _helper;
        private ToastManager _manager;

        #endregion Private Fields

        #region Private Constructors

        private ToastHelper()
        {
            _manager = new ToastManager();
            _manager.Init<ToastManager>("B站UP主桌面工具");
            _manager.ClearHistory("B站UP主桌面工具");
            ToastManager.ToastCallback += ToastManager_ToastCallback;
        }

        #endregion Private Constructors

        #region Public Enums

        /// <summary>
        /// 可以打开的窗口枚举
        /// </summary>
        public enum WindowNegation
        {
            /// <summary>
            /// 不打开窗口
            /// </summary>
            None,

            /// <summary>
            /// 主窗口
            /// </summary>
            MainWindow,

            /// <summary>
            /// 主窗口的Statistics_Tab
            /// </summary>
            MainWindow_Statistics,

            /// <summary>
            /// 更新窗口
            /// </summary>
            UpdateWindow
        }

        #endregion Public Enums

        #region Public Properties

        public static ToastHelper Instance
        {
            get
            {
                if (_helper == null) _helper = new ToastHelper();
                return _helper;
            }
        }

        public bool IsEnable
        {
            get
            {
                return !DesktopBridgeHelper.IsWindows7OrLower;
            }
        }

        /// <summary>
        /// 指示点击正文打开的窗口
        /// </summary>
        public WindowNegation WindowBeOpenWhenNeed { get; private set; } = WindowNegation.None;

        #endregion Public Properties

        #region Public Methods

        public void Notify()
        {
            WindowBeOpenWhenNeed = WindowNegation.None;
            if (IsEnable) new Action(() => { _manager.Notify("Hello", "Toast"); }).Invoke();
        }

        public void NotifyMutiLaunch()
        {
            WindowBeOpenWhenNeed = WindowNegation.MainWindow;
            if (IsEnable) new Action(() => { _manager.Notify("B站UP主桌面工具", "已经有一个实例在运行！", new ToastCommands { Content = "知道了", Argument = "MutiLaunchArg" }); }).Invoke();
            else System.Windows.Forms.MessageBox.Show("已经有一个实例在运行！", "B站UP主桌面工具");
        }

        public void NotifyUpdate()
        {
            WindowBeOpenWhenNeed = WindowNegation.UpdateWindow;
            if (IsEnable) new Action(() => { _manager.Notify("B站UP主桌面工具", $"检查到新版本({Update.Instance.NewVersionStr})！\r\n点击查看详情...", "Update", false, new ToastCommands { Content = "更新", Argument = "CallUpdateArg" }, new ToastCommands { Content = "下次再说", Argument = "NOarg" }, new ToastCommands { Content = "忽略此版本", Argument = "IgnoreUpdateArg", AfterActivationBehavior = "pendingUpdate", ActivationType = "Background" }); }).Invoke();
            else NotifyIconHelper.Instance.ShowToolTip($"检查到新版本！({Update.Instance.NewVersionStr})\r\n更新内容：\r\n{Update.Instance.UpdateText}");
        }

        #endregion Public Methods

        #region Private Methods

        private void ToastManager_ToastCallback(string app, string arg, List<KeyValuePair<string, string>> kvs)
        {
            switch (arg)
            {
                case "MutiLaunchArg"://重复启动
                    App.Current.Dispatcher.Invoke(() => { WindowsManager.Instance.GetWindow<MainWindow>().Show(); });
                    break;

                case "CallUpdateArg"://执行更新
                    App.Current.Dispatcher.Invoke(() => { WindowsManager.Instance.GetWindow<UpdateWindow>().Show(); });
                    new Action(() => { Update.Instance.DoUpdate(); }).Invoke();
                    break;

                case "IgnoreUpdateArg"://忽略更新
                    new Action(() => { _manager.Notify("B站UP主桌面工具", "是否确认忽略该版本更新？", "Update", true, new ToastCommands { Content = "是", Argument = "IgnoreUpdateYesArg" }, new ToastCommands { Content = "否", Argument = "IgnoreUpdateNoArg", AfterActivationBehavior = "pendingUpdate", ActivationType = "Background" }); }).Invoke();
                    break;

                case "IgnoreUpdateYesArg"://确认忽略更新
                    Update.Instance.IgnoreThisVersion();
                    break;

                case "IgnoreUpdateNoArg"://反悔忽略更新
                    new Action(() => { _manager.Notify("B站UP主桌面工具", $"检查到新版本({Update.Instance.NewVersionStr})！\r\n点击查看详情...", "Update", true, new ToastCommands { Content = "更新", Argument = "CallUpdateArg" }, new ToastCommands { Content = "下次再说", Argument = "NOarg" }, new ToastCommands { Content = "忽略此版本", Argument = "IgnoreUpdateArg", AfterActivationBehavior = "pendingUpdate", ActivationType = "Background" }); }).Invoke();
                    break;

                case "NOarg"://普通的“否”命令，不执行任何操作
                    break;

                default://用户单击消息，如果命令中存在可打开窗口则打开窗口
                    switch (WindowBeOpenWhenNeed)
                    {
                        case WindowNegation.MainWindow:
                            App.Current.Dispatcher.Invoke(() => { WindowsManager.Instance.GetWindow<MainWindow>().Show(); });
                            break;

                        case WindowNegation.MainWindow_Statistics:
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                WindowsManager.Instance.GetWindow<MainWindow>().Show();
                                WindowsManager.Instance.GetWindow<MainWindow>().ToTab(1);
                            });
                            break;

                        case WindowNegation.UpdateWindow:
                            App.Current.Dispatcher.Invoke(() => { WindowsManager.Instance.GetWindow<UpdateWindow>().Show(); });
                            break;

                        case WindowNegation.None:
                        default:
                            break;
                    }
                    break;
            }
        }

        #endregion Private Methods
    }
}
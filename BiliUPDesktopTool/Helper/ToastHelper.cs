using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
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
        private static ToastHelper _helper;
        private ToastManager _manager;

        public static ToastHelper Instance
        {
            get
            {
                if (_helper == null) _helper = new ToastHelper();
                return _helper;
            }
        }

        private ToastHelper()
        {
            _manager = new ToastManager();
            _manager.Init<ToastManager>("B站UP主桌面工具");
            ToastManager.ToastCallback += ToastManager_ToastCallback;
        }

        #region Public Methods

        public void Notify()
        {
            //new Action(()=> { _manager.Notify("Hello", "Toast", new ToastCommands {Content = "OK", Argument = "OKarg" }, new ToastCommands { Content = "NO", Argument = "NOarg" }); }).Invoke();
            new Action(()=> { _manager.Notify("Hello", "Toast", new ToastCommands {Content = "OK", Argument = "OKarg abc" }, new ToastCommands { Content = "NO", Argument = "NOarg" }); }).Invoke();
        }

        public void NotifyMutiLaunch()
        {
            new Action(() => { _manager.Notify("B站UP主桌面工具", "已经有一个实例在运行！", new ToastCommands { Content = "知道了", Argument = "MutiLaunchArg" }); }).Invoke();
        }

        public bool IsEnable
        {
            get
            {
                return DesktopBridgeHelper.IsRunningAsUwp();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void ToastManager_ToastCallback(string app, string arg, List<KeyValuePair<string, string>> kvs)
        {
            switch (arg)
            {
                case "MutiLaunchArg":
                    App.Current.Dispatcher.Invoke(() => { WindowsManager.Instance.GetWindow<MainWindow>().Show(); });
                    break;

                default:
                    string res = $"appid : {app}  arg : {arg} \n";
                    kvs.ForEach(kv => res += $"key : {kv.Key}  value : {kv.Value} \n");
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        System.Windows.Forms.MessageBox.Show(res);
                    });
                    break;
            }
        }

        

        #endregion Private Methods
    }
}
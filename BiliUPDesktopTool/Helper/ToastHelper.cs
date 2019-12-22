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
        #region Public Methods

        public static void Notify()
        {
            var _manager = new ToastManager();
            _manager.Init<ToastManager>("B站UP主桌面工具");
            ToastManager.ToastCallback += ToastManager_ToastCallback;
            new Action(()=> { _manager.Notify("Hello", "Toast", new ToastCommands { Content = "OK", Argument = "OKarg" }, new ToastCommands { Content = "NO", Argument = "NOarg" }); }).Invoke();
        }

        public static bool IsEnable
        {
            get
            {
                return DesktopBridgeHelper.IsRunningAsUwp();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void ToastManager_ToastCallback(string app, string arg, List<KeyValuePair<string, string>> kvs)
        {
            string res = $"appid : {app}  arg : {arg} \n";
            kvs.ForEach(kv => res += $"key : {kv.Key}  value : {kv.Value} \n");
            App.Current.Dispatcher.Invoke(() =>
            {
                System.Windows.Forms.MessageBox.Show(res);
            });
        }

        

        #endregion Private Methods
    }
}
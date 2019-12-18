using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DesktopWindowSetter.xaml 的交互逻辑
    /// </summary>
    public partial class DesktopWindowSetter : Window
    {
        #region Private Fields

        /// <summary>
        /// 备份设置 [0]:Top;[1]:Left
        /// </summary>
        private double[] _Backup;

        #endregion Private Fields

        #region Public Constructors

        public DesktopWindowSetter()
        {
            InitializeComponent();

            //显示桌面
            ShowDesktop();

            //建立备份
            _Backup = new double[2] { Skin.Instance.DesktopWnd_Top, Skin.Instance.DesktopWnd_Left };
        }

        #endregion Public Constructors

        #region Private Methods

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            Skin.Instance.DesktopWnd_Top = _Backup[0];
            Skin.Instance.DesktopWnd_Left = _Backup[1];
            Close();
        }

        private void Btn_comfirm_Click(object sender, RoutedEventArgs e)
        {
            Skin.Instance.Save();
            Close();
        }

        /// <summary>
        /// 显示桌面
        /// </summary>
        private void ShowDesktop()
        {
            Type oleType = Type.GetTypeFromProgID("Shell.Application");
            object oleObject = Activator.CreateInstance(oleType);
            oleType.InvokeMember("MinimizeAll", BindingFlags.InvokeMethod, null, oleObject, null);

            WindowState = WindowState.Normal;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        #endregion Private Methods
    }
}
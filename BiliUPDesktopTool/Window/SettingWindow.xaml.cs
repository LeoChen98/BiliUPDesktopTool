using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        #region Public Constructors

        public SettingWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            //if (key != null && key.GetValue("BiliUPDesktopTool") != null)
            //{
            //    key.DeleteValue("BiliUPDesktopTool");
            //}
            //else
            //{
            //    key.SetValue("BiliUPDesktopTool", "\"" + Process.GetCurrentProcess().MainModule.FileName + "\"");
            //}
        }

        #endregion Private Methods
    }
}
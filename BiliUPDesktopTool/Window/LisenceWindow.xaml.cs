using System.Diagnostics;
using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// LisenceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LisenceWindow : Window
    {
        #region Public Constructors

        public LisenceWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void BTN_No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BTN_ShowLisence_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer.exe", "https://zhangbudademao.com/BiliUPDesktopTool/Lisence.html");
        }

        private void BTN_Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private new void Show()
        {
        }

        #endregion Private Methods
    }
}
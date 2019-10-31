using System.Diagnostics;
using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        #region Public Constructors

        public About()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void BTN_CheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update.Instance.CheckUpdate();
        }

        private void BTN_ShowLisence_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer.exe", "https://www.zhangbudademao.com/BiliUPDesktopTool/Lisence.html");
        }

        #endregion Private Methods
    }
}
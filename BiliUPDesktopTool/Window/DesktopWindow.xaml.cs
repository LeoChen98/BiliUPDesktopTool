using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DesktopWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DesktopWindow : Window
    {
        #region Public Constructors

        public DesktopWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DesktopEmbeddedWindowHelper.DesktopEmbedWindow(this);
        }

        #endregion Private Methods
    }
}
using System.Windows;
using System.Windows.Data;

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

            BindingInit();
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// 初始化数据绑定
        /// </summary>
        private void BindingInit()
        {
            //绑定窗体Top
            Binding bind_top = new Binding
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_Top")
            };
            SetBinding(TopProperty, bind_top);

            //绑定窗体Left
            Binding bind_left = new Binding
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_Left")
            };
            SetBinding(LeftProperty, bind_left);

            //绑定窗体透明度
            Binding bind_opacity = new Binding
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_Opacity")
            };
            SetBinding(OpacityProperty, bind_opacity);

            //绑定背景
            Binding bind_background = new Binding
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_Bg")
            };
            ViewerPanel.SetBinding(BackgroundProperty, bind_background);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DesktopEmbeddedWindowHelper.DesktopEmbedWindow(this);
        }

        #endregion Private Methods
    }
}
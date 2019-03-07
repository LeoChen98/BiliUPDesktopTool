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

            //建立备份
            _Backup = new double[2] { Bas.skin.DesktopWnd_Top, Bas.skin.DesktopWnd_Left };

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
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            Bas.skin.DesktopWnd_Top = _Backup[0];
            Bas.skin.DesktopWnd_Left = _Backup[1];
            Close();
        }

        private void Btn_comfirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
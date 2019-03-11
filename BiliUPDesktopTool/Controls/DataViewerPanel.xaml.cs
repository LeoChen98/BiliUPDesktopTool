using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DataViewerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DataViewerPanel : UserControl
    {
        #region Public Constructors

        public DataViewerPanel()
        {
            InitializeComponent();

            BindingInit();
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// 初始化绑定
        /// </summary>
        private void BindingInit()
        {
            Binding bind_R_color = new Binding()
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_FontColor")
            };
            BindingOperations.SetBinding(Rx, Shape.StrokeProperty, bind_R_color);
            BindingOperations.SetBinding(Ry, Shape.StrokeProperty, bind_R_color);
        }

        #endregion Private Methods
    }
}
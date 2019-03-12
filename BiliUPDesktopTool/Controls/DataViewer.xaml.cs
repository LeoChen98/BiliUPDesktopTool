using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DataViewer.xaml 的交互逻辑
    /// </summary>
    public partial class DataViewer : UserControl
    {
        #region Public Constructors

        public DataViewer()
        {
            InitializeComponent();

            BindingInit();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// 数据标题
        /// </summary>
        public string Title
        {
            get { return DataTitle.Content.ToString(); }
            set { DataTitle.Content = value; }
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// 初始化绑定
        /// </summary>
        private void BindingInit()
        {
            Binding bind_datatitle_color = new Binding()
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_FontColor")
            };
            DataTitle.SetBinding(ForegroundProperty, bind_datatitle_color);
            Triangle.SetBinding(Shape.FillProperty, bind_datatitle_color);
            Triangle.SetBinding(Shape.StrokeProperty, bind_datatitle_color);
        }

        #endregion Private Methods
    }
}
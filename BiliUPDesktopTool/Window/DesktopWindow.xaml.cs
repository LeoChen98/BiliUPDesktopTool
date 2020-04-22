using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DesktopWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DesktopWindow : Window
    {
        public DesktopWindow()
        {
            InitializeComponent();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DesktopEmbeddedWindowHelper.DesktopEmbedWindow(this);

            System.Drawing.Point size = ScreenHelper.GetFullSize();
            System.Drawing.Point startPoint = ScreenHelper.GetStartPoint();

            
            WinAPIHelper.MoveWindow(new WindowInteropHelper(this).Handle, -startPoint.X*2, -startPoint.Y*2, size.X  ,size.Y, true); //我也不知道为什么起始点要相反数两倍。。。
        }


    }
}

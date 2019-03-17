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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DataDisplaySetter.xaml 的交互逻辑
    /// </summary>
    public partial class DataDisplaySetter : Window
    {
        public DataDisplaySetter()
        {
            InitializeComponent();
        }


        private void Scroll_Overall_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Scroll_Overall.LineLeft();
            }
            else
            {
                Scroll_Overall.LineRight();
            }
        }
    }
}

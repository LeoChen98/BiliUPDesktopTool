using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// LisenceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LisenceWindow : Window
    {
        public LisenceWindow()
        {
            InitializeComponent();
        }

        private void BTN_ShowLisence_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer.exe", "https://zhangbudademao.com/BiliUPDesktopTool/Lisence.html");
        }

        private void BTN_Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BTN_No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private new void Show()
        {
            
        }
    }
}

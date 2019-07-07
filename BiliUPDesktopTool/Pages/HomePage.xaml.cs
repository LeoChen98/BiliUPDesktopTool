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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Lbl_Desc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(TB_Desc.Visibility == Visibility.Hidden)
            {
                TB_Desc.Visibility = Visibility.Visible;
                TB_Desc.Focus();
            }
        }

        private void TB_Desc_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TB_Desc.Visibility = Visibility.Hidden;
        }

        private void TB_Desc_LostFocus(object sender, RoutedEventArgs e)
        {
            TB_Desc.Visibility = Visibility.Hidden;
        }

        private void Lbl_Desc_MouseEnter(object sender, MouseEventArgs e)
        {
            Lbl_Desc.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x4f, 0xbd, 0xea));
        }

        private void Lbl_Desc_MouseLeave(object sender, MouseEventArgs e)
        {
            Lbl_Desc.BorderBrush = null;
        }

        private void TB_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
            }
        }

        private void Btn_Space_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(0x66, 0xb3, 0xb3, 0xb3));
        }

        private void Btn_Space_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_Space_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = null;
        }

        private void Btn_Center_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_Manager_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_Upload_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_UpdateAccount_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_SignOut_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

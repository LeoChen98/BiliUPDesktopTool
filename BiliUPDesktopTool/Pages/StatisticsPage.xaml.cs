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
using System.Windows.Media.Animation;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// StatisticsPage.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsPage : UserControl
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        public static string DataDesc
        {
            get { return Properties.Resources.DataDesc; }
            set { }
        }

        private void Btn_OverallData_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Btn_OverallData.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            Btn_VideoData.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            Btn_ArticleData.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            Btn_OtherSettings.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));

            (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xa1, 0xd6));
            int index = int.Parse((sender as Label).Tag.ToString());

            DoubleAnimation an = new DoubleAnimation()
            {
                From = (double)TabSelectedLine.GetValue(Canvas.LeftProperty),
                To = 80 * index + 10,
                Duration = new Duration(TimeSpan.FromMilliseconds(250))
            };
            TabSelectedLine.BeginAnimation(Canvas.LeftProperty, an);

            if(index == 3 || _TabIndex == 3)
            {
                ThicknessAnimation ant = new ThicknessAnimation()
                {
                    From = SettingBoxWrapper.Margin,
                    To = index == 3 ? new Thickness(-740, 0, 0, 0) : new Thickness(0, 0, 0, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(250))
                };
                SettingBoxWrapper.BeginAnimation(MarginProperty, ant);

                if(index != 3)
                {
                    ant = new ThicknessAnimation()
                    {
                        From = SelectBoxWrapper.Margin,
                        To = new Thickness(-index * 500, 0, -1000+index*500, 0),
                        Duration = new Duration(TimeSpan.FromMilliseconds(250))
                    };
                    SelectBoxWrapper.BeginAnimation(MarginProperty, ant);
                }
            }
            else
            {
                ThicknessAnimation ant = new ThicknessAnimation()
                {
                    From = SelectBoxWrapper.Margin,
                    To = new Thickness(-index * 500, 0, -1000 + index * 500, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(250))
                };
                SelectBoxWrapper.BeginAnimation(MarginProperty, ant);
            }

            _TabIndex = index;

        }
        private int _TabIndex = 0;

        private void Btn_OverallData_MouseEnter(object sender, MouseEventArgs e)
        {
            if((Color)(sender as Label).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x00, 0x00, 0x00))
            {
                (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c));
            }
        }

        private void Btn_OverallData_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Label).Foreground.GetValue(SolidColorBrush.ColorProperty) ==Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c))
            {
                (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            }
        }

        private void TB_RefreshInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}

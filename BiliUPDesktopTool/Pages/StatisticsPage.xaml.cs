using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// StatisticsPage.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsPage : UserControl
    {
        #region Private Fields

        private int _TabIndex = 0;

        #endregion Private Fields

        #region Public Constructors

        public StatisticsPage()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void Btn_OverallData_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Label).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x00, 0x00, 0x00))
            {
                (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c));
            }
        }

        private void Btn_OverallData_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Label).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c))
            {
                (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            }
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

            if (index == 3 || _TabIndex == 3)
            {
                ThicknessAnimation ant = new ThicknessAnimation()
                {
                    From = SettingBoxWrapper.Margin,
                    To = index == 3 ? new Thickness(-740, 0, 0, 0) : new Thickness(0, 0, 0, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(250))
                };
                SettingBoxWrapper.BeginAnimation(MarginProperty, ant);

                if (index != 3)
                {
                    ant = new ThicknessAnimation()
                    {
                        From = SelectBoxWrapper.Margin,
                        To = new Thickness(-index * 500, 0, -1000 + index * 500, 0),
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

        private void TB_RefreshInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        #endregion Private Methods
    }
}
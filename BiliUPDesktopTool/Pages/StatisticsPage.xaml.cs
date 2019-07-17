using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        private DataViewer DV_Selected;

        #endregion Private Fields

        #region Public Constructors

        public StatisticsPage()
        {
            InitializeComponent();

            BindingInit();
        }

        #endregion Public Constructors

        #region Private Methods

        private void BindingInit()
        {
            Binding bind_RealMode = new Binding()
            {
                Source = Bas.settings,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("IsRealTime")
            };
            TBN_RealMode.SetBinding(ToggleButton.StatusProperty, bind_RealMode);

            Binding bind_RefreshInterval = new Binding()
            {
                Source = Bas.settings,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath("DataRefreshInterval"),
                Converter = new IntervalConverter(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            TB_RefreshInterval.SetBinding(TextBox.TextProperty, bind_RefreshInterval);
        }

        private void Btn_ActiveDataViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xc0, 0xff));
        }

        private void Btn_ActiveDataViewer_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xa1, 0xd6));
        }

        private void Btn_ActiveDataViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Bas.settings.IsDataViewerDisplay = true;
            DisActived.Visibility = Visibility.Collapsed;
            Blur.Radius = 0;
        }

        private void Btn_DeActiveDataViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x04, 0x04));
        }

        private void Btn_DeActiveDataViewer_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x39, 0x39));
        }

        private void Btn_DeActiveDataViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Bas.settings.IsDataViewerDisplay = false;
            Btn_Tab_MouseUp(Btn_OverallData, null);
            DisActived.Visibility = Visibility.Visible;
            Blur.Radius = 5;
        }

        private void Btn_SetDataViewerPos_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xc0, 0xff));
        }

        private void Btn_SetDataViewerPos_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xa1, 0xd6));
        }

        private void Btn_SetDataViewerPos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Bas.desktopwindowsetter == null || !Bas.desktopwindowsetter.IsVisible)
            {
                Bas.desktopwindowsetter = new DesktopWindowSetter();
                Bas.desktopwindowsetter.Show();
            }
            else
            {
                Bas.desktopwindowsetter.Activate();
                Bas.desktopwindowsetter.WindowState = WindowState.Normal;
            }
        }

        private void Btn_Tab_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Label).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x00, 0x00, 0x00))
            {
                (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c));
            }
        }

        private void Btn_Tab_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Label).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c))
            {
                (sender as Label).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            }
        }

        private void Btn_Tab_MouseUp(object sender, MouseButtonEventArgs e)
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

        private void DataViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DV_Selected != ((sender as Grid).Children[0] as DataViewer))
                (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(0xcc, 0xe6, 0xe6, 0xe6));
        }

        private void DataViewer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DV_Selected != ((sender as Grid).Children[0] as DataViewer))
                (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void DataViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DV_Selected != null) (DV_Selected.Parent as Grid).Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));

            (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2));

            DV_Selected = ((sender as Grid).Children[0] as DataViewer);

            int id = -1;

            foreach (List<string> item in Bas.settings.DataViewSelected)
            {
                if (item != null && item[0] == DV_Selected.DataMode[0] && item[1] == DV_Selected.DataMode[1])
                {
                    id = Bas.settings.DataViewSelected.IndexOf(item);
                    break;
                }
            }

            ST_Block_LT.Background = id == 0 ? new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2)) : new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
            ST_Block_RT.Background = id == 1 ? new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2)) : new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
            ST_Block_LB.Background = id == 2 ? new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2)) : new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
            ST_Block_RB.Background = id == 3 ? new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2)) : new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void ST_Block_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Border).Background.GetValue(SolidColorBrush.ColorProperty) != Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2))
                (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xcc, 0xe6, 0xe6, 0xe6));
        }

        private void ST_Block_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as Border).Background.GetValue(SolidColorBrush.ColorProperty) != Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2))
                (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void ST_Block_MouseUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (Bas.settings.DataViewSelected[i] == DV_Selected.DataMode)
                {
                    Bas.settings.DataViewSelected[i] = null;
                    break;
                }
            }

            ST_Block_LT.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
            ST_Block_RT.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
            ST_Block_LB.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
            ST_Block_RB.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));

            switch ((sender as Border).Tag)
            {
                case "LT":
                    Bas.settings.DataViewSelected[0] = DV_Selected.DataMode;
                    break;

                case "RT":
                    Bas.settings.DataViewSelected[1] = DV_Selected.DataMode;
                    break;

                case "LB":
                    Bas.settings.DataViewSelected[2] = DV_Selected.DataMode;
                    break;

                case "RB":
                    Bas.settings.DataViewSelected[3] = DV_Selected.DataMode;
                    break;
            }

            Bas.settings.PropertyChangedA(Bas.settings, new PropertyChangedEventArgs("DataViewSelected"));
            Bas.settings.DataViewSelected_Changed_Send();

            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2));
        }

        private void TB_RefreshInterval_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Bas.settings.DataRefreshInterval = string.IsNullOrEmpty((sender as TextBox).Text) ? 0 : int.Parse((sender as TextBox).Text);
        }

        private void TB_RefreshInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Bas.settings.IsDataViewerDisplay)
            {
                Bas.settings.IsDataViewerDisplay = true;
                DisActived.Visibility = Visibility.Collapsed;
                Blur.Radius = 0;
            }
            else
            {
                Bas.settings.IsDataViewerDisplay = false;
                DisActived.Visibility = Visibility.Visible;
                Blur.Radius = 5;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        #endregion Private Methods
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private DataViewer DV_Selected;

        #endregion Private Fields

        #region Public Constructors

        public StatisticsPage()
        {
            InitializeComponent();

            //BindingInit();
        }

        #endregion Public Constructors

        #region Private Methods

        private void BindingInit()
        {
            Binding bind_RealMode = new Binding()
            {
                Source = Settings.Instance,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("IsRealTime")
            };
            TBN_RealMode.SetBinding(ToggleButton.StatusProperty, bind_RealMode);

            Binding bind_RefreshInterval = new Binding()
            {
                Source = Settings.Instance,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath("DataRefreshInterval"),
                Converter = new IntervalConverter()
            };
            TB_RefreshInterval.SetBinding(TextBox.TextProperty, bind_RefreshInterval);
        }

        private void Btn_ActiveDataViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Instance.IsDataViewerDisplay = true;
        }

        private void Btn_DeActiveDataViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Instance.IsDataViewerDisplay = false;
        }

        private void Btn_SetDataViewerPos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!WindowsManager.Instance.GetWindow<DesktopWindowSetter>().IsVisible)
            {
                WindowsManager.Instance.GetWindow<DesktopWindowSetter>().Show();
            }
            else
            {
                WindowsManager.Instance.GetWindow<DesktopWindowSetter>().Activate();
                WindowsManager.Instance.GetWindow<DesktopWindowSetter>().WindowState = WindowState.Normal;
            }
        }

        private void Btn_Tab_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as TextBlock).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x00, 0x00, 0x00))
            {
                (sender as TextBlock).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c));
            }
        }

        private void Btn_Tab_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Color)(sender as TextBlock).Foreground.GetValue(SolidColorBrush.ColorProperty) == Color.FromArgb(0xff, 0x7c, 0x7c, 0x7c))
            {
                (sender as TextBlock).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            }
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

            foreach (List<string> item in Settings.Instance.DataViewSelected)
            {
                if (item != null && item[0] == DV_Selected.DataMode[0] && item[1] == DV_Selected.DataMode[1])
                {
                    id = Settings.Instance.DataViewSelected.IndexOf(item);
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
                if (Settings.Instance.DataViewSelected[i] == DV_Selected.DataMode)
                {
                    Settings.Instance.DataViewSelected[i] = null;
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
                    Settings.Instance.DataViewSelected[0] = DV_Selected.DataMode;
                    break;

                case "RT":
                    Settings.Instance.DataViewSelected[1] = DV_Selected.DataMode;
                    break;

                case "LB":
                    Settings.Instance.DataViewSelected[2] = DV_Selected.DataMode;
                    break;

                case "RB":
                    Settings.Instance.DataViewSelected[3] = DV_Selected.DataMode;
                    break;
            }

            Settings.Instance.PropertyChangedA(Settings.Instance, new PropertyChangedEventArgs("DataViewSelected"));
            Settings.Instance.DataViewSelected_Changed_Send();

            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0xcc, 0xb2, 0xb2, 0xb2));
        }

        private void TB_RefreshInterval_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Settings.Instance.DataRefreshInterval = string.IsNullOrEmpty((sender as TextBox).Text) ? 0 : int.Parse((sender as TextBox).Text) * 1000;
        }

        private void TB_RefreshInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tmp;
            if (int.TryParse(e.Text, out tmp) && tmp > 0)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Instance.IsDataViewerDisplay)
            {
                DisActived.Visibility = Visibility.Collapsed;
                Blur.Radius = 0;
            }
            else
            {
                DisActived.Visibility = Visibility.Visible;
                Blur.Radius = 5;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            DV_Selected = null;

            SettingBox.Children.Clear();
            OverallTab.Children.Clear();
            VideoTab.Children.Clear();
            ArticleTab.Children.Clear();

            BindingOperations.ClearAllBindings(TBN_RealMode);
            BindingOperations.ClearAllBindings(TB_RefreshInterval);
        }

        #endregion Private Methods
    }
}
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        #region Private Fields

        private Timer Refresher;

        #endregion Private Fields

        #region Public Constructors

        public HomePage()
        {
            InitializeComponent();

            BindingInit();

            Refresher = new Timer(new TimerCallback(Refresh));
            Refresher.Change(0, 1800000);
        }

        #endregion Public Constructors

        #region Private Methods

        private void BindingInit()
        {
            Binding binduname = new Binding()
            {
                Source = Bas.account,
                Path = new PropertyPath("UName"),
                Mode = BindingMode.OneWay
            };
            TBk_UserName.SetBinding(TextBlock.TextProperty, binduname);

            Binding bindface = new Binding()
            {
                Source = Bas.account,
                Path = new PropertyPath("Pic"),
                Mode = BindingMode.OneWay
            };
            Img_Face.SetBinding(Image.SourceProperty, bindface);

            Binding binddesc = new Binding()
            {
                Source = Bas.account,
                Path = new PropertyPath("Desc"),
                Mode = BindingMode.OneWay
            };
            TB_Desc.SetBinding(TextBox.TextProperty, binddesc);

            Binding bindlevel = new Binding()
            {
                Source = Bas.account,
                Path = new PropertyPath("Level"),
                Mode = BindingMode.OneWay,
                Converter = new WidthNHeightValue_Times_Converter(),
                ConverterParameter = -36
            };
            Img_level.SetBinding(Canvas.TopProperty, bindlevel);

            Binding bindlevelprocess = new Binding()
            {
                Source = Bas.account,
                Path = new PropertyPath("ExpProgress"),
                Mode = BindingMode.OneWay,
                Converter = new WidthNHeightValue_Times_Converter(),
                ConverterParameter = 398
            };
            Bar_level_top.SetBinding(WidthProperty, bindlevelprocess);

            Binding bindlevelpercentage = new Binding()
            {
                Source = Bas.account,
                Path = new PropertyPath("ExpProgress"),
                Mode = BindingMode.OneWay,
                Converter = new Percentage_Converter()
            };
            Lbl_Level.SetBinding(ContentProperty, bindlevelpercentage);
        }

        private void Btn_Center_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://member.bilibili.com/v2#/home");
        }

        private void Btn_Login_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0x66, 0xb3, 0xb3, 0xb3));
        }

        private void Btn_Login_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void Btn_Login_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Bas.account.Login();
            NeedLoginBox.Visibility = Visibility.Hidden;
            InfoBox.Visibility = Visibility.Visible;
        }

        private void Btn_Manager_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://member.bilibili.com/v2#/upload-manager/article");
        }

        private void Btn_SignOut_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Bas.account.SignOut();
        }

        private void Btn_Space_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(0x66, 0xb3, 0xb3, 0xb3));
        }

        private void Btn_Space_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = null;
        }

        private void Btn_Space_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://space.bilibili.com/" + Bas.account.Uid);
        }

        private void Btn_UpdateAccount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Bas.account.Update();
        }

        private void Btn_Upload_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("功能暂未开放");
        }

        private void Lbl_Desc_MouseEnter(object sender, MouseEventArgs e)
        {
            Lbl_Desc.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x4f, 0xbd, 0xea));
        }

        private void Lbl_Desc_MouseLeave(object sender, MouseEventArgs e)
        {
            Lbl_Desc.BorderBrush = null;
        }

        private void Lbl_Desc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TB_Desc.Visibility == Visibility.Hidden)
            {
                TB_Desc.Visibility = Visibility.Visible;
                TB_Desc.Focus();
            }
        }

        private void Refresh(object state)
        {
            string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/index/operation", Bas.account.Cookies);
            if (!string.IsNullOrEmpty(str))
            {
                JObject obj = JObject.Parse(str);
                if ((int)obj["code"] == 0)
                {
                    foreach (JToken i in obj["data"]["collect_arc"])
                    {
                        if ((int)i["state"] == 1)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                EventList.Children.Add(new EventItem(new EventItem.EventInfo()
                                {
                                    Title = i["content"].ToString(),
                                    Desc = i["remark"].ToString(),
                                    Link = i["link"].ToString(),
                                    Start_Time = i["start_time"].ToString(),
                                    End_Time = i["end_time"].ToString()
                                }));
                            });
                        }
                    }
                }
            }
        }

        private void TB_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
            }
        }

        private void TB_Desc_LostFocus(object sender, RoutedEventArgs e)
        {
            Bas.account.ChangeDesc(TBk_desc.Text);
            TB_Desc.Visibility = Visibility.Hidden;
        }

        private void TB_Desc_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Bas.account.ChangeDesc(TBk_desc.Text);
            TB_Desc.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Bas.account.Islogin)
            {
                InfoBox.Visibility = Visibility.Visible;
                NeedLoginBox.Visibility = Visibility.Hidden;
            }
            else
            {
                InfoBox.Visibility = Visibility.Hidden;
                NeedLoginBox.Visibility = Visibility.Visible;
            }
        }

        #endregion Private Methods
    }
}
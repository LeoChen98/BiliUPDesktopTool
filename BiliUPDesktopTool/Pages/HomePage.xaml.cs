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

            //BindingInit();

            Refresher = new Timer(new TimerCallback(Refresh));
            Refresher.Change(0, 1800000);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// 创作中心命令
        /// </summary>
        public RelayCommand CenterCommand
        {
            get
            {
                return new RelayCommand(() => { Process.Start("https://member.bilibili.com/v2#/home"); });
            }
        }

        /// <summary>
        /// 投稿管理命令
        /// </summary>
        public RelayCommand ManagerCommand
        {
            get
            {
                return new RelayCommand(() => { Process.Start("https://member.bilibili.com/v2#/upload-manager/article"); });
            }
        }

        /// <summary>
        /// 主站空间命令
        /// </summary>
        public RelayCommand SpaceCommand
        {
            get
            {
                return new RelayCommand(() => { Process.Start("https://space.bilibili.com/" + Account.Instance.Uid); });
            }
        }

        /// <summary>
        /// 投稿工具命令
        /// </summary>
        public RelayCommand UploaderCommand
        {
            get
            {
                return new RelayCommand(() => { MsgBoxPushHelper.RaisePushMsg("功能暂未开放"); });
            }
        }

        #endregion Public Properties

        #region Private Methods

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
            Account.Instance.Login();
            NeedLoginBox.Visibility = Visibility.Hidden;
            InfoBox.Visibility = Visibility.Visible;
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
            string str = Bas.GetHTTPBody("https://member.bilibili.com/x/app/h5/activity/videoall?platform=ios", Account.Instance.Cookies);
            if (!string.IsNullOrEmpty(str))
            {
                JObject obj = JObject.Parse(str);
                if ((int)obj["code"] == 0)
                {
                    Dispatcher?.Invoke(() =>
                    {
                        EventList.Children.Clear();
                    });
                    foreach (JToken i in obj["data"])
                    {
                        Dispatcher.Invoke(() =>
                        {
                            EventList.Children.Add(new EventItem(new EventItem.EventInfo()
                            {
                                Title = i["name"].ToString(),
                                Desc = i["protocol"].ToString(),
                                Link = i["act_url"].ToString(),
                                Start_Time = i["stime"] == null ? "-" : Bas.GetTimeStringFromSecondTimestamp((int)i["stime"]),
                                End_Time = "-"
                            }));
                        });
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
            Account.Instance.ChangeDesc(TBk_desc.Text);
            TB_Desc.Visibility = Visibility.Hidden;
        }

        private void TB_Desc_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Account.Instance.ChangeDesc(TBk_desc.Text);
            TB_Desc.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearAllBindings(TBk_UserName);
            BindingOperations.ClearAllBindings(Img_Face);
            BindingOperations.ClearAllBindings(TB_Desc);
            BindingOperations.ClearAllBindings(Img_level);
            BindingOperations.ClearAllBindings(Lbl_Level);

            EventList.Children.Clear();

            Refresher.Dispose();
        }

        #endregion Private Methods
    }
}
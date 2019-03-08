using Newtonsoft.Json.Linq;
using QRCoder;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Private Fields

        private Timer Monitor, Refresher;

        #endregion Private Fields

        #region Public Constructors

        public LoginWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 显示模式窗口
        /// </summary>
        /// <param name="info">提示信息</param>
        /// <returns>操作是否成功</returns>
        public bool? ShowDialog(string info)
        {
            lbl_stauts.Dispatcher.Invoke(delegate () { lbl_stauts.Visibility = Visibility.Visible; lbl_stauts.Content = info; });
            return ShowDialog();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// 获取登陆二维码并显示
        /// </summary>
        private void GetQrcode()
        {
            //获取二维码要包含的url
            string str = Bas.GetHTTPBody("https://passport.bilibili.com/qrcode/getLoginUrl", "", "https://passport.bilibili.com/login");
            JObject obj = JObject.Parse(str);

            if ((int)obj["code"] == 0)
            {
                // 生成二维码的内容
                string strCode = obj["data"]["url"].ToString();
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrcode = new QRCode(qrCodeData);

                //生成二维码位图
                Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 0, 6, false);

                qrcodeBox.Dispatcher.Invoke(delegate ()
                {
                    IntPtr myImagePtr = qrCodeImage.GetHbitmap();     //创建GDI对象，返回指针

                    BitmapSource imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(myImagePtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());  //创建imgSource

                    WinAPIHelper.DeleteObject(myImagePtr);

                    qrcodeBox.Source = imgsource;
                });

                Monitor = new Timer(MonitorCallback, obj["data"]["oauthKey"].ToString(), 1000, 1000);
                Refresher = new Timer(RefresherCallback, null, 180000, Timeout.Infinite);
            }
        }

        /// <summary>
        /// 监视器回调
        /// </summary>
        /// <param name="o">oauthKey</param>
        private void MonitorCallback(object o)
        {
            string oauthKey = o.ToString();

            string str = Bas.PostHTTPBody("https://passport.bilibili.com/qrcode/getLoginInfo", "oauthKey=" + oauthKey + "&gourl=https%3A%2F%2Fwww.bilibili.com%2F", "", "https://passport.bilibili.com/login");
            JObject obj = JObject.Parse(str);

            if (obj.Property("code") != null)
            {
                if ((int)obj["code"] == 0)//登陆成功
                {
                    string Querystring = Regex.Split(obj["data"]["url"].ToString(), "\\?")[1];
                    string[] KeyValuePair = Regex.Split(Querystring, "&");
                    string cookies = "";
                    for (int i = 0; i < KeyValuePair.Length - 1; i++)
                    {
                        cookies += KeyValuePair[i] + "; ";
                    }

                    DateTime expires = DateTime.Now.AddDays(29);

                    Bas.account.Cookies = cookies;
                    Bas.account.Expires = expires;
                    Bas.account.Save();
                    Dispatcher.Invoke(delegate () { Close(); DialogResult = true; });
                }
            }
            else
            {
                switch ((int)obj["data"])
                {
                    case -4://未扫描
                        break;

                    case -5://已扫描
                        lbl_stauts.Dispatcher.Invoke(delegate () { lbl_stauts.Visibility = Visibility.Visible; });
                        break;

                    case 0://登陆成功

                        break;
                }
            }
        }

        /// <summary>
        /// 二维码过期刷新
        /// </summary>
        /// <param name="o">忽略</param>
        private void RefresherCallback(object o)
        {
            GetQrcode();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetQrcode();
        }

        #endregion Private Methods
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 账号数据类
    /// </summary>
    public class Account : SettingsBase<Account.AccountTable>
    {
        #region Public Fields

        /// <summary>
        /// 指示是否登陆
        /// </summary>
        public bool Islogin = false;

        #endregion Public Fields

        #region Private Fields

        //TODO 发布时重新生成.
        /// <summary>
        /// 加密秘钥
        /// </summary>
        private const string encryptKey = "{C6F403E9-53FF-4B75-8182-DC03BBE6944A}";

        /// <summary>
        /// 刷新器
        /// </summary>
        private Timer Refresher;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化账号数据
        /// </summary>
        public Account()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\");

            ST = new AccountTable();
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Account.dma"))
            {
                using (FileStream fs = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Account.dma", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string str = reader.ReadToEnd();
                        str = EncryptHelper.DesDecrypt(str, encryptKey);
                        OutputTable<AccountTable> OT = JsonConvert.DeserializeObject<OutputTable<AccountTable>>(str);
                        ST = OT.settings;
                        Islogin = true;
                    }
                }
            }

            Refresher = new Timer(new TimerCallback(GetInfo));
            Refresher.Change(0, 1800000);
        }

        /// <summary>
        /// 空白初始化函数
        /// </summary>
        /// <param name="flag"></param>
        public Account(EmptyInit flag)
        {
            ST = new AccountTable();

            Refresher = new Timer(new TimerCallback(GetInfo));
            Refresher.Change(0, 1800000);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Cookies字符串
        /// </summary>
        public string Cookies
        {
            get
            {
                if (DateTime.Compare(DateTime.Now, Expires) >= 0)//如果过期
                {
                    Islogin = false;
                    Update();
                }
                return ST.Cookies;
            }
            set
            {
                ST.Cookies = value;
            }
        }

        /// <summary>
        /// csrf_token值
        /// </summary>
        public string Csrf_Token
        {
            get { return ST.Csrf_Token; }
            set { ST.Csrf_Token = value; }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Desc
        {
            get { return ST.Desc; }
            private set
            {
                ST.Desc = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("Desc"));
            }
        }

        /// <summary>
        /// cookies的生存有效期（结束时间）
        /// </summary>
        public DateTime Expires
        {
            get { return ST.Expires; }
            set { ST.Expires = value; }
        }

        /// <summary>
        /// 经验进度
        /// </summary>
        public double ExpProgress
        {
            get { return ST.ExpProgress; }
            private set
            {
                ST.ExpProgress = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("ExpProgress"));
            }
        }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int Level
        {
            get { return ST.Level; }
            private set
            {
                ST.Level = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("Level"));
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        public string Pic
        {
            get { return ST.Pic; }
            set
            {
                ST.Pic = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("Pic"));
            }
        }

        /// <summary>
        /// 用户数字id
        /// </summary>
        public string Uid
        {
            get { return ST.Uid; }
            set
            {
                ST.Uid = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("Uid"));
            }
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UName
        {
            get { return ST.UName; }
            set
            {
                ST.UName = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("UName"));
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 修改简介
        /// </summary>
        /// <param name="NewDesc">新简介文本</param>
        public void ChangeDesc(string NewDesc)
        {
            string str = Bas.PostHTTPBody("https://api.bilibili.com/x/member/web/sign/update", "user_sign=" + NewDesc + "&jsonp=jsonp&csrf=" + Csrf_Token, Cookies);
            if (!string.IsNullOrEmpty(str))
            {
                JObject obj = JObject.Parse(str);
                if ((int)obj["code"] == 0)
                {
                    Bas.MainWindow.NotifyMsg("修改简介成功！");
                    return;
                }
            }
            Bas.MainWindow.NotifyMsg("修改简介失败！" + str);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public void GetInfo(object o = null)
        {
            if (Islogin)
            {
                string str = Bas.GetHTTPBody("https://api.bilibili.com/x/space/myinfo", Cookies);
                if (!string.IsNullOrEmpty(str))
                {
                    JObject obj = JObject.Parse(str);
                    if ((int)obj["code"] == 0)
                    {
                        Pic = obj["data"]["face"].ToString();
                        UName = obj["data"]["name"].ToString();
                        Desc = obj["data"]["sign"].ToString();
                        Level = (int)obj["data"]["level_exp"]["current_level"];
                        if ((double)obj["data"]["level_exp"]["next_exp"] != -1)
                        {
                            ExpProgress = ((double)obj["data"]["level_exp"]["current_exp"] - (double)obj["data"]["level_exp"]["current_min"]) / ((double)obj["data"]["level_exp"]["next_exp"] - (double)obj["data"]["level_exp"]["current_min"]);
                        }
                        else
                        {
                            ExpProgress = 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        public void Login()
        {
            System.Windows.Application.Current?.Dispatcher.Invoke(() =>
            {
                if (Bas.LoginWindow == null) Bas.LoginWindow = new LoginWindow();
                if (!Bas.LoginWindow.IsVisible) Bas.LoginWindow.ShowDialog();
                GetInfo();
            });
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            OutputTable<AccountTable> OT = new OutputTable<AccountTable>(ST);
            string json = JsonConvert.SerializeObject(OT);
            json = EncryptHelper.DesEncrypt(json, encryptKey);
            using (FileStream fs = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Account.dma", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
        }

        /// <summary>
        /// 注销账号
        /// </summary>
        public void SignOut()
        {
            Islogin = false;
            ST = new AccountTable();
            Save();
        }

        /// <summary>
        /// 更新账号
        /// </summary>
        public void Update()
        {
            System.Windows.Application.Current?.Dispatcher.Invoke(() =>
            {
                if (Bas.LoginWindow == null) Bas.LoginWindow = new LoginWindow();
                if (!Bas.LoginWindow.IsVisible) Bas.LoginWindow.ShowDialog();
            });
        }

        #endregion Public Methods

        #region Public Classes

        /// <summary>
        /// 账号数据表
        /// </summary>
        public class AccountTable
        {
            #region Public Fields

            public string Cookies;
            public string Csrf_Token;
            public string Desc;
            public DateTime Expires;
            public double ExpProgress;
            public int Level;
            public string Pic;
            public string Uid;
            public string UName;

            #endregion Public Fields
        }

        public class EmptyInit { };

        #endregion Public Classes
    }
}
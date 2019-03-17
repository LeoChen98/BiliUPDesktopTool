using Newtonsoft.Json;
using System;
using System.IO;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 账号数据类
    /// </summary>
    public class Account : SettingsBase<Account.AccountTable>
    {
        #region Private Fields

        //TODO 发布时重新生成.
        /// <summary>
        /// 加密秘钥
        /// </summary>
        private const string encryptKey = "{C6F403E9-53FF-4B75-8182-DC03BBE6944A}";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化账号数据
        /// </summary>
        public Account()
        {
            ST = new AccountTable();
            if (File.Exists("Account.dma"))
            {
                using (FileStream fs = File.Open("Account.dma", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string str = reader.ReadToEnd();
                        str = EncryptHelper.DesDecrypt(str, encryptKey);
                        OutputTable<AccountTable> OT = JsonConvert.DeserializeObject<OutputTable<AccountTable>>(str);
                        ST = OT.settings;
                    }
                }
            }
            else
            {
                //Bas.account = new Account(new EmptyInit());
                LoginWindow lw = new LoginWindow(this);
                lw.ShowDialog();
            }
        }

        /// <summary>
        /// 空白初始化函数
        /// </summary>
        /// <param name="flag"></param>
        public Account(EmptyInit flag)
        {
            ST = new AccountTable();
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
                    App.Current.Dispatcher.Invoke(() => { LoginWindow LWnd = new LoginWindow(); LWnd.ShowDialog(); });
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
        /// cookies的生存有效期（结束时间）
        /// </summary>
        public DateTime Expires
        {
            get { return ST.Expires; }
            set { ST.Expires = value; }
        }

        /// <summary>
        /// 用户数字id
        /// </summary>
        public string Uid
        {
            get { return ST.Uid; }
            set { ST.Uid = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            OutputTable<AccountTable> OT = new OutputTable<AccountTable>(ST);
            string json = JsonConvert.SerializeObject(OT);
            json = EncryptHelper.DesEncrypt(json, encryptKey);
            using (FileStream fs = File.Open("Account.dma", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
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
            public DateTime Expires;
            public string Uid;

            #endregion Public Fields
        }

        public class EmptyInit { };

        #endregion Public Classes
    }
}
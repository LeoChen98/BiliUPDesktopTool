using Newtonsoft.Json;
using System;
using System.IO;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 账号数据类
    /// </summary>
    internal class Account
    {
        #region Private Fields

        private AccountTable AT;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化账号数据
        /// </summary>
        public Account()
        {
            AT = new AccountTable();

            if (File.Exists("Account.dma"))
            {
                using (FileStream fs = File.Open("Account.dma", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string str = reader.ReadToEnd();
                        str = EncryptHelper.DesDecrypt(str, "{C6F403E9-53FF-4B75-8182-DC03BBE6944A}");
                        OutputTable OT = JsonConvert.DeserializeObject<OutputTable>(str);
                        AT = OT.Account;
                    }
                }
            }
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
                    LoginWindow LWnd = new LoginWindow();
                    if (!(bool)LWnd.ShowDialog("登录失效，请重新登陆。"))
                    {
                        return null;
                    }
                }
                return AT.Cookies;
            }
            set
            {
                AT.Cookies = value;
            }
        }

        /// <summary>
        /// cookies的生存有效期（结束时间）
        /// </summary>
        public DateTime Expires
        {
            get { return AT.Expires; }
            set { AT.Expires = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            OutputTable OT = new OutputTable(AT);
            string json = JsonConvert.SerializeObject(OT);
            json = EncryptHelper.DesEncrypt(json, "{C6F403E9-53FF-4B75-8182-DC03BBE6944A}");
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
            public DateTime Expires;

            #endregion Public Fields
        }

        /// <summary>
        /// 导出数据表
        /// </summary>
        public class OutputTable
        {
            #region Public Fields

            public const int pid = 117;

            public const int version = 1;

            public AccountTable Account;

            #endregion Public Fields

            #region Public Constructors

            public OutputTable(AccountTable AT)
            {
                Account = AT;
            }

            #endregion Public Constructors
        }

        #endregion Public Classes
    }
}
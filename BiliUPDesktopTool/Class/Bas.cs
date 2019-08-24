using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 公共模块
    /// </summary>
    internal class Bas
    {
        #region Public Fields

        /// <summary>
        /// 账号信息实例
        /// </summary>
        public static Account account;

        /// <summary>
        /// up主数据实例
        /// </summary>
        public static BiliUPData biliupdata;

        /// <summary>
        /// 桌面挂件位置设置窗口
        /// </summary>
        public static DesktopWindowSetter desktopwindowsetter;

        /// <summary>
        /// 登录窗体
        /// </summary>
        public static LoginWindow LoginWindow;

        /// <summary>
        /// 主窗口
        /// </summary>
        public static MainWindow MainWindow;

        /// <summary>
        /// 系统托盘
        /// </summary>
        public static NotifyIconHelper notifyIcon;

        /// <summary>
        /// 设置实例
        /// </summary>
        public static Settings settings;

        /// <summary>
        /// 皮肤实例
        /// </summary>
        public static Skin skin;

        /// <summary>
        /// 更新实例
        /// </summary>
        public static Update update;

        #endregion Public Fields

        #region Public Properties

        /// <summary>
        /// 主程序版本号
        /// </summary>
        public static string Version
        {
            get
            {
                return "2.0.0.11 Preview 3";
            }
        }

        /// <summary>
        /// 开源许可
        /// </summary>
        public string Thanks
        {
            get
            {
                return Properties.Resources.Thanks;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 获取文件md5
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>md5</returns>
        public static string GetFileHash(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取指定url的内容
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="Cookies">cookies</param>
        /// <returns>返回内容</returns>
        public static string GetHTTPBody(string url, string Cookies = "", string Referer = "")
        {
            HttpWebRequest req = null;
            HttpWebResponse rep = null;
            StreamReader reader = null;
            string body = "";
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);

                if (!string.IsNullOrEmpty(Cookies))
                {
                    CookieCollection CookiesC = SetCookies(Cookies, url);
                    req.CookieContainer = new CookieContainer(CookiesC.Count)
                    {
                        PerDomainCapacity = CookiesC.Count
                    };
                    req.CookieContainer.Add(CookiesC);
                }

                req.Accept = "*/*";
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                req.Referer = Referer;

                rep = (HttpWebResponse)req.GetResponse();
                reader = new StreamReader(rep.GetResponseStream());
                body = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (reader != null) reader.Close();
                if (rep != null) rep.Close();
                if (req != null) req.Abort();
            }
            return body;
        }

        /// <summary>
        /// POST方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">要post的数据</param>
        /// <param name="Cookies">cookies</param>
        /// <returns>返回内容</returns>
        public static string PostHTTPBody(string url, string data = "", string Cookies = "", string Referer = "")
        {
            HttpWebRequest req = null;
            HttpWebResponse rep = null;
            StreamReader reader = null;
            string body = "";
            byte[] bdata = Encoding.UTF8.GetBytes(data);
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";

                if (!string.IsNullOrEmpty(Cookies))
                {
                    CookieCollection CookiesC = SetCookies(Cookies, url);
                    req.CookieContainer = new CookieContainer(CookiesC.Count)
                    {
                        PerDomainCapacity = CookiesC.Count
                    };
                    req.CookieContainer.Add(CookiesC);
                }

                req.Accept = "*/*";
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                req.Referer = Referer;
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

                Stream writer = req.GetRequestStream();
                writer.Write(bdata, 0, bdata.Length);
                writer.Close();

                rep = (HttpWebResponse)req.GetResponse();
                reader = new StreamReader(rep.GetResponseStream());
                body = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (reader != null) reader.Close();
                if (rep != null) rep.Close();
                if (req != null) req.Abort();
            }
            return body;
        }

        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <param name="cookiestr">Cookies的字符串</param>
        public static CookieCollection SetCookies(string cookiestr, string url)
        {
            try
            {
                CookieCollection public_cookie;
                Uri target = new Uri(url);
                public_cookie = new CookieCollection();
                cookiestr = cookiestr.Replace(",", "%2C");//转义“，”
                string[] cookiestrs = Regex.Split(cookiestr, "; ");
                foreach (string i in cookiestrs)
                {
                    string[] cookie = Regex.Split(i, "=");
                    public_cookie.Add(new Cookie(cookie[0], cookie[1]) { Domain = target.Host });
                }
                return public_cookie;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 解压功能
        /// </summary>
        /// <param name="fileToUnZip">待解压的文件</param>
        /// <param name="zipedFolder">指定解压目标目录</param>
        /// <param name="password">密码</param>
        /// <returns>解压结果</returns>
        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip.Trim()));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');

                        if (fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        using (fs = File.Create(fileName))
                        {
                            int size = 2048;
                            byte[] data = new byte[size];
                            while (true)
                            {
                                size = zipStream.Read(data, 0, data.Length);
                                if (size > 0)
                                    fs.Write(data, 0, size);
                                else
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return result;
        }

        #endregion Public Methods
    }
}
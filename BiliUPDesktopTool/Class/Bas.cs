using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BiliUPDesktopTool
{
    internal class Bas
    {
        #region Public Methods

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

                Stream writer = req.GetRequestStream();
                writer.Write(bdata, 0, bdata.Length);
                writer.Close();

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

        #endregion Public Methods
    }
}
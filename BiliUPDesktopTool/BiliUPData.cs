using Newtonsoft.Json.Linq;

namespace BiliUPDesktopTool
{
    internal class BiliUPData
    {
        #region Public Methods

        public static int GetCharge()
        {
            string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/elec/balance", Properties.Settings.Default.Cookies);
            JObject obj = JObject.Parse(str);
            if ((int)obj["code"] == 0)
            {
                return (int)obj["data"]["wallet"]["sponsorBalance"];
            }
            else
            {
                return -1;
            }
        }

        #endregion Public Methods
    }
}
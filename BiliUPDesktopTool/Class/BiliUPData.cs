using Newtonsoft.Json.Linq;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// B站UP主数据
    /// </summary>
    internal class BiliUPData
    {
        #region Public Methods

        /// <summary>
        /// 获得充电数据
        /// </summary>
        /// <returns>电池数</returns>
        public static int GetCharge()
        {
            string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/elec/balance", Bas.account.Cookies);
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

        /// <summary>
        /// 获得激励计划数据
        /// </summary>
        /// <returns>[0]:前天收入;[1]:本月收入</returns>
        public static int[] GetGrowUp()
        {
            string str = Bas.GetHTTPBody("https://api.bilibili.com/studio/growup/web/up/summary", Bas.account.Cookies);
            JObject obj = JObject.Parse(str);
            if ((int)obj["code"] == 0)
            {
                return new int[2] { (int)obj["data"]["day_income"], (int)obj["data"]["income"] };
            }
            else
            {
                return new int[2] { -1, -1 };
            }
        }

        /// <summary>
        /// 播放量和粉丝数
        /// </summary>
        /// <returns>[0]:新增粉丝;[1]:总粉丝数;[2]:新增播放;[3]:总播放</returns>
        public static int[] PlayNFans()
        {
            string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/index/stat", Bas.account.Cookies);
            JObject obj = JObject.Parse(str);
            if ((int)obj["code"] == 0)
            {
                return new int[4] { (int)obj["data"]["incr_fans"], (int)obj["data"]["total_fans"], (int)obj["data"]["incr_click"], (int)obj["data"]["total_click"] };
            }
            else
            {
                return new int[4] { -1, -1, -1, -1 };
            }
        }

        #endregion Public Methods
    }
}
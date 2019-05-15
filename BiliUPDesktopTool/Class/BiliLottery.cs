using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiliUPDesktopTool
{
    //TODO 重构为VM，跟Lottery窗体绑定
    /// <summary>
    /// B站抽奖
    /// </summary>
    class BiliLottery
    {
        public BiliLottery(string url)
        {
            string[] tmp = url.Split('/');
            switch (tmp[2])
            {
                case "t.bilibili.com":
                    Dynamic.CreateLottery(url);
                    break;
                case "h.bilibili.com":
                    Draw.CreateLottery(url);
                    break;
                case "www.bilibili.com":


                default:
                    break;
            }
        }

        /// <summary>
        /// 抽奖信息
        /// </summary>
        public class LotteryInfo
        {
            /// <summary>
            /// 抽奖文本
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// 抽奖用户名
            /// </summary>
            public string UName { get; set; }
            /// <summary>
            /// 抽奖用户id
            /// </summary>
            public int UId { get; set; }

            /// <summary>
            /// 转发列表
            /// </summary>
            public List<RepostInfo> RepostList { get; set; }

            /// <summary>
            /// 评论列表
            /// </summary>
            public List<CommentInfo> CommentList { get; set; }

            /// <summary>
            /// 点赞列表
            /// </summary>
            public List<LikeInfo> LikeList { get; set; }

            
        }
        /// <summary>
        /// 转发信息
        /// </summary>
        public class RepostInfo
        {

        }
        /// <summary>
        /// 评论信息
        /// </summary>
        public class CommentInfo
        {

        }

        /// <summary>
        /// 点赞信息
        /// </summary>
        public class LikeInfo
        {

        }

        /// <summary>
        /// 动态
        /// </summary>
        public class Dynamic
        {
            public static LotteryInfo CreateLottery(string url)
            {

            }
        }
        /// <summary>
        /// 相簿
        /// </summary>
        public class Draw
        {
            public static LotteryInfo CreateLottery(string url)
            {

            }
        }
        /// <summary>
        /// 视频投稿
        /// </summary>
        public class Video
        {
            public static LotteryInfo CreateLottery(string url)
            {

            }
        }
    }
    
}

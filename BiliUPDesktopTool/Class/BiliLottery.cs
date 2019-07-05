using System.Collections.Generic;

namespace BiliUPDesktopTool
{
    //TODO 重构为VM，跟Lottery窗体绑定
    /// <summary>
    /// B站抽奖
    /// </summary>
    internal class BiliLottery
    {
        #region Public Constructors

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
                    Video.CreateLottery(url);
                    break;

                default:
                    break;
            }
        }

        #endregion Public Constructors

        #region Public Classes

        /// <summary>
        /// 评论信息
        /// </summary>
        public class CommentInfo
        {
        }

        /// <summary>
        /// 相簿
        /// </summary>
        public class Draw
        {
            #region Public Methods

            public static LotteryInfo CreateLottery(string url)
            {
            }

            #endregion Public Methods
        }

        /// <summary>
        /// 动态
        /// </summary>
        public class Dynamic
        {
            #region Public Methods

            public static LotteryInfo CreateLottery(string url)
            {
            }

            #endregion Public Methods
        }

        /// <summary>
        /// 点赞信息
        /// </summary>
        public class LikeInfo
        {
        }

        /// <summary>
        /// 抽奖信息
        /// </summary>
        public class LotteryInfo
        {
            #region Public Properties

            /// <summary>
            /// 评论列表
            /// </summary>
            public List<CommentInfo> CommentList { get; set; }

            /// <summary>
            /// 点赞列表
            /// </summary>
            public List<LikeInfo> LikeList { get; set; }

            /// <summary>
            /// 转发列表
            /// </summary>
            public List<RepostInfo> RepostList { get; set; }

            /// <summary>
            /// 抽奖文本
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 抽奖用户id
            /// </summary>
            public int UId { get; set; }

            /// <summary>
            /// 抽奖用户名
            /// </summary>
            public string UName { get; set; }

            #endregion Public Properties
        }

        /// <summary>
        /// 转发信息
        /// </summary>
        public class RepostInfo
        {
        }

        /// <summary>
        /// 视频投稿
        /// </summary>
        public class Video
        {
            #region Public Methods

            public static LotteryInfo CreateLottery(string url)
            {
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}
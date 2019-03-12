using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Threading;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// B站UP主数据
    /// </summary>
    internal class BiliUPData
    {
        #region Public Fields

        /// <summary>
        /// 专栏数据
        /// </summary>
        public Article article;

        /// <summary>
        /// 视频数据
        /// </summary>
        public Video video;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// 数据刷新计时器
        /// </summary>
        private Timer Refresher;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化up主数据
        /// </summary>
        public BiliUPData()
        {
            article = new Article();
            video = new Video();

            Refresher = new Timer((o) => { Refresh(); }, null, 0, Bas.settings.DataRefreshInterval);
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns>刷新后的实例</returns>
        public BiliUPData Refresh()
        {
            article.Refresh();
            video.Refresh();
            return this;
        }

        #endregion Public Methods

        #region Public Classes

        /// <summary>
        /// 专栏类
        /// </summary>
        public class Article : INotifyPropertyChanged
        {
            #region Private Fields

            private int _coin, _coin_incr, _fav, _fav_incr, _like, _like_incr, _reply, _reply_incr, _share, _share_incr, _view, _view_incr;

            #endregion Private Fields

            #region Public Constructors

            /// <summary>
            /// 初始化专栏数据
            /// </summary>
            public Article()
            {
            }

            #endregion Public Constructors

            #region Public Events

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion Public Events

            #region Public Properties

            /// <summary>
            /// 硬币
            /// </summary>
            public int coin
            {
                get { return _coin; }
                set
                {
                    _coin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin"));
                }
            }

            /// <summary>
            /// 硬币增量
            /// </summary>
            public int coin_incr
            {
                get { return _coin_incr; }
                set
                {
                    _coin_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin_incr"));
                }
            }

            /// <summary>
            /// 收藏
            /// </summary>
            public int fav
            {
                get { return _fav; }
                set
                {
                    _fav = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav"));
                }
            }

            /// <summary>
            /// 收藏增量
            /// </summary>
            public int fav_incr
            {
                get { return _fav_incr; }
                set
                {
                    _fav_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav_incr"));
                }
            }

            /// <summary>
            /// 点赞
            /// </summary>
            public int like
            {
                get { return _like; }
                set
                {
                    _like = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like"));
                }
            }

            /// <summary>
            /// 点赞增量
            /// </summary>
            public int like_incr
            {
                get { return _like_incr; }
                set
                {
                    _like_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like_incr"));
                }
            }

            /// <summary>
            /// 评论
            /// </summary>
            public int reply
            {
                get { return _reply; }
                set
                {
                    _reply = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("reply"));
                }
            }

            /// <summary>
            /// 评论增量
            /// </summary>
            public int reply_incr
            {
                get { return _reply_incr; }
                set
                {
                    _reply_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("reply_incr"));
                }
            }

            /// <summary>
            /// 分享
            /// </summary>
            public int share
            {
                get { return _share; }
                set
                {
                    _share = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share"));
                }
            }

            /// <summary>
            /// 分享增量
            /// </summary>
            public int share_incr
            {
                get { return _share_incr; }
                set
                {
                    _share_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share_incr"));
                }
            }

            /// <summary>
            /// 点击
            /// </summary>
            public int view
            {
                get { return _view; }
                set
                {
                    _view = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("view"));
                }
            }

            /// <summary>
            /// 点击增量
            /// </summary>
            public int view_incr
            {
                get { return _view_incr; }
                set
                {
                    _view_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("view_incr"));
                }
            }

            #endregion Public Properties

            #region Public Methods

            /// <summary>
            /// 刷新数据
            /// </summary>
            /// <returns>刷新数据后的实例</returns>
            public Article Refresh()
            {
                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/h5/data/article", Bas.account.Cookies, "https://member.bilibili.com/studio/gabriel/data-center/overview");
                if (!string.IsNullOrEmpty(str))
                {
                    JObject obj = JObject.Parse(str);
                    if ((int)obj["code"] == 0)
                    {
                        coin = (int)obj["data"]["stat"]["coin"];
                        coin_incr = (int)obj["data"]["stat"]["incr_coin"];
                        fav = (int)obj["data"]["stat"]["fav"];
                        fav_incr = (int)obj["data"]["stat"]["incr_fav"];
                        like = (int)obj["data"]["stat"]["like"];
                        like_incr = (int)obj["data"]["stat"]["incr_like"];
                        reply = (int)obj["data"]["stat"]["reply"];
                        reply_incr = (int)obj["data"]["stat"]["incr_reply"];
                        share = (int)obj["data"]["stat"]["share"];
                        share_incr = (int)obj["data"]["stat"]["incr_share"];
                        view = (int)obj["data"]["stat"]["view"];
                        view_incr = (int)obj["data"]["stat"]["incr_view"];
                    }
                }
                return this;
            }

            #endregion Public Methods
        }

        /// <summary>
        /// 视频类
        /// </summary>
        public class Video : INotifyPropertyChanged
        {
            #region Private Fields

            private int _coin, _coin_incr, _dm, _dm_incr, _fan, _fan_incr, _fav, _fav_incr, _like, _like_incr, _play, _play_incr, _share, _share_incr, _comment, _comment_incr;

            private double _elec, _elec_incr, _growup, _growup_incr;

            #endregion Private Fields

            #region Public Constructors

            /// <summary>
            /// 初始化视频数据
            /// </summary>
            public Video()
            {
            }

            #endregion Public Constructors

            #region Public Events

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion Public Events

            #region Public Properties

            /// <summary>
            /// 硬币
            /// </summary>
            public int coin
            {
                get { return _coin; }
                set
                {
                    _coin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin"));
                }
            }

            /// <summary>
            /// 硬币增量
            /// </summary>
            public int coin_incr
            {
                get { return _coin_incr; }
                set
                {
                    _coin_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin_incr"));
                }
            }

            /// <summary>
            /// 评论
            /// </summary>
            public int comment
            {
                get { return _comment; }
                set
                {
                    _comment = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("comment"));
                }
            }

            /// <summary>
            /// 评论增量
            /// </summary>
            public int comment_incr
            {
                get { return _comment_incr; }
                set
                {
                    _comment_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("comment_incr"));
                }
            }

            /// <summary>
            /// 弹幕
            /// </summary>
            public int dm
            {
                get { return _dm; }
                set
                {
                    _dm = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("dm"));
                }
            }

            /// <summary>
            /// 弹幕增量
            /// </summary>
            public int dm_incr
            {
                get { return _dm_incr; }
                set
                {
                    _dm_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("dm_incr"));
                }
            }

            /// <summary>
            /// 电池
            /// </summary>
            public double elec
            {
                get { return _elec; }
                set
                {
                    _elec = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("elec"));
                }
            }

            /// <summary>
            /// 电池增量
            /// </summary>
            public double elec_incr
            {
                get { return _elec_incr; }
                set
                {
                    _elec_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("elec_incr"));
                }
            }

            /// <summary>
            /// 粉丝
            /// </summary>
            public int fan
            {
                get { return _fan; }
                set
                {
                    _fan = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fan"));
                }
            }

            /// <summary>
            /// 粉丝增量
            /// </summary>
            public int fan_incr
            {
                get { return _fan_incr; }
                set
                {
                    _fan_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fan_incr"));
                }
            }

            /// <summary>
            /// 收藏
            /// </summary>
            public int fav
            {
                get { return _fav; }
                set
                {
                    _fav = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav"));
                }
            }

            /// <summary>
            /// 收藏增量
            /// </summary>
            public int fav_incr
            {
                get { return _fav_incr; }
                set
                {
                    _fav_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav_incr"));
                }
            }

            /// <summary>
            /// 激励计划
            /// </summary>
            public double growup
            {
                get { return _growup; }
                set
                {
                    _growup = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("growup"));
                }
            }

            /// <summary>
            /// 激励计划增量
            /// </summary>
            public double growup_incr
            {
                get { return _growup_incr; }
                set
                {
                    _growup_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("growup_incr"));
                }
            }

            /// <summary>
            /// 点赞
            /// </summary>
            public int like
            {
                get { return _like; }
                set
                {
                    _like = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like"));
                }
            }

            /// <summary>
            /// 点赞增量
            /// </summary>
            public int like_incr
            {
                get { return _like_incr; }
                set
                {
                    _like_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like_incr"));
                }
            }

            /// <summary>
            /// 播放
            /// </summary>
            public int play
            {
                get { return _play; }
                set
                {
                    _play = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("play"));
                }
            }

            /// <summary>
            /// 播放增量
            /// </summary>
            public int play_incr
            {
                get { return _play_incr; }
                set
                {
                    _play_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("play_incr"));
                }
            }

            /// <summary>
            /// 分享
            /// </summary>
            public int share
            {
                get { return _share; }
                set
                {
                    _share = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share"));
                }
            }

            /// <summary>
            /// 分享增量
            /// </summary>
            public int share_incr
            {
                get { return _share_incr; }
                set
                {
                    _share_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share_incr"));
                }
            }

            #endregion Public Properties

            #region Public Methods

            /// <summary>
            /// 刷新数据
            /// </summary>
            /// <returns>刷新后的实例</returns>
            public Video Refresh()
            {
                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/h5/data/overview?type=0", Bas.account.Cookies, "https://member.bilibili.com/studio/gabriel/data-center/overview");
                if (!string.IsNullOrEmpty(str))
                {
                    JObject obj = JObject.Parse(str);
                    if ((int)obj["code"] == 0)
                    {
                        coin = (int)obj["data"]["stat"]["coin"];
                        coin_incr = (int)obj["data"]["stat"]["coin"] - (int)obj["data"]["stat"]["coin_last"];
                        comment = (int)obj["data"]["stat"]["comment"];
                        comment_incr = (int)obj["data"]["stat"]["comment"] - (int)obj["data"]["stat"]["comment_last"];
                        dm = (int)obj["data"]["stat"]["dm"];
                        dm_incr = (int)obj["data"]["stat"]["dm"] - (int)obj["data"]["stat"]["dm_last"];
                        elec_incr = (double)obj["data"]["stat"]["elec"] - (double)obj["data"]["stat"]["elec_last"];
                        fan = (int)obj["data"]["stat"]["fan"];
                        fan_incr = (int)obj["data"]["stat"]["fan"] - (int)obj["data"]["stat"]["fan_last"];
                        fav = (int)obj["data"]["stat"]["fav"];
                        fav_incr = (int)obj["data"]["stat"]["fav"] - (int)obj["data"]["stat"]["fav_last"];
                        like = (int)obj["data"]["stat"]["like"];
                        like_incr = (int)obj["data"]["stat"]["like"] - (int)obj["data"]["stat"]["like_last"];
                        play = (int)obj["data"]["stat"]["play"];
                        play_incr = (int)obj["data"]["stat"]["play"] - (int)obj["data"]["stat"]["play_last"];
                        share = (int)obj["data"]["stat"]["share"];
                        share_incr = (int)obj["data"]["stat"]["share"] - (int)obj["data"]["stat"]["share_last"];
                    }
                }
                double tmp1 = GetCharge();
                if (tmp1 != -1) elec = tmp1;
                double[] tmp = GetGrowUp();
                if (tmp != new double[2] { -1, -1 })
                {
                    growup_incr = tmp[0];
                    growup = tmp[1];
                }
                return this;
            }

            #endregion Public Methods

            #region Private Methods

            /// <summary>
            /// 获得充电数据
            /// </summary>
            /// <returns>电池数</returns>
            private double GetCharge()
            {
                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/elec/balance", Bas.account.Cookies);
                if (!string.IsNullOrEmpty(str))
                {
                    JObject obj = JObject.Parse(str);
                    if ((int)obj["code"] == 0)
                    {
                        return (double)obj["data"]["wallet"]["sponsorBalance"];
                    }
                    else
                    {
                        return -1;
                    }
                }
                else return -1;
            }

            /// <summary>
            /// 获得激励计划数据
            /// </summary>
            /// <returns>[0]:前天收入;[1]:本月收入</returns>
            private double[] GetGrowUp()
            {
                string str = Bas.GetHTTPBody("https://api.bilibili.com/studio/growup/web/up/summary", Bas.account.Cookies);
                if (!string.IsNullOrWhiteSpace(str))
                {
                    JObject obj = JObject.Parse(str);
                    if ((int)obj["code"] == 0)
                    {
                        return new double[2] { (double)obj["data"]["day_income"], (double)obj["data"]["income"] };
                    }
                    else
                    {
                        return new double[2] { -1, -1 };
                    }
                }
                else { return new double[2] { -1, -1 }; }
            }

            #endregion Private Methods
        }

        #endregion Public Classes
    }
}
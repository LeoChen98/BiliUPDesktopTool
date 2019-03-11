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

        #region Protected Methods

        /// <summary>
        /// 格式化数字(大于1万则返回x.x万）
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns></returns>
        protected static string TrimNumStr(string num)
        {
            if (float.Parse(num) >= 10000)
            {
                string tmp = num.Split('.')[0];
                return tmp.Substring(0, tmp.Length - 4) + "." + tmp.Substring(tmp.Length - 4, 1) + "万";
            }
            else
            {
                return num;
            }
        }

        #endregion Protected Methods

        #region Public Classes

        /// <summary>
        /// 专栏类
        /// </summary>
        public class Article : INotifyPropertyChanged
        {
            #region Private Fields

            /// <summary>
            /// 数据内存
            /// </summary>
            private string _coin, _coin_incr, _fav, _fav_incr, _like, _like_incr, _reply, _reply_incr, _share, _share_incr, _view, _view_incr;

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
            public string coin
            {
                get { return _coin; }
                set
                {
                    _coin = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin"));
                }
            }

            /// <summary>
            /// 硬币增量
            /// </summary>
            public string coin_incr
            {
                get { return _coin_incr; }
                set
                {
                    _coin_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin_incr"));
                }
            }

            /// <summary>
            /// 收藏
            /// </summary>
            public string fav
            {
                get { return _fav; }
                set
                {
                    _fav = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav"));
                }
            }

            /// <summary>
            /// 收藏增量
            /// </summary>
            public string fav_incr
            {
                get { return _fav_incr; }
                set
                {
                    _fav_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav_incr"));
                }
            }

            /// <summary>
            /// 点赞
            /// </summary>
            public string like
            {
                get { return _like; }
                set
                {
                    _like = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like"));
                }
            }

            /// <summary>
            /// 点赞增量
            /// </summary>
            public string like_incr
            {
                get { return _like_incr; }
                set
                {
                    _like_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like_incr"));
                }
            }

            /// <summary>
            /// 评论
            /// </summary>
            public string reply
            {
                get { return _reply; }
                set
                {
                    _reply = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("reply"));
                }
            }

            /// <summary>
            /// 评论增量
            /// </summary>
            public string reply_incr
            {
                get { return _reply_incr; }
                set
                {
                    _reply_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("reply_incr"));
                }
            }

            /// <summary>
            /// 分享
            /// </summary>
            public string share
            {
                get { return _share; }
                set
                {
                    _share = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share"));
                }
            }

            /// <summary>
            /// 分享增量
            /// </summary>
            public string share_incr
            {
                get { return _reply_incr; }
                set
                {
                    _reply_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share_incr"));
                }
            }

            /// <summary>
            /// 点击
            /// </summary>
            public string view
            {
                get { return _view; }
                set
                {
                    _view = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("view"));
                }
            }

            /// <summary>
            /// 点击增量
            /// </summary>
            public string view_incr
            {
                get { return _view_incr; }
                set
                {
                    _view_incr = TrimNumStr(value);
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
                        coin = obj["data"]["stat"]["coin"].ToString();
                        coin_incr = obj["data"]["stat"]["incr_coin"].ToString();
                        fav = obj["data"]["stat"]["fav"].ToString();
                        fav_incr = obj["data"]["stat"]["incr_fav"].ToString();
                        like = obj["data"]["stat"]["like"].ToString();
                        like_incr = obj["data"]["stat"]["incr_like"].ToString();
                        reply = obj["data"]["stat"]["reply"].ToString();
                        reply_incr = obj["data"]["stat"]["incr_reply"].ToString();
                        share = obj["data"]["stat"]["share"].ToString();
                        share_incr = obj["data"]["stat"]["incr_share"].ToString();
                        view = obj["data"]["stat"]["view"].ToString();
                        view_incr = obj["data"]["stat"]["incr_view"].ToString();
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

            /// <summary>
            /// 数据内存
            /// </summary>
            private string _coin, _coin_incr, _dm, _dm_incr, _fan, _fan_incr, _fav, _fav_incr, _like, _like_incr, _play, _play_incr, _share, _share_incr, _elec, _elec_incr, _growup, _growup_incr;

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
            public string coin
            {
                get { return _coin; }
                set
                {
                    _coin = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin"));
                }
            }

            /// <summary>
            /// 硬币增量
            /// </summary>
            public string coin_incr
            {
                get { return _coin_incr; }
                set
                {
                    _coin_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coin_incr"));
                }
            }

            /// <summary>
            /// 弹幕
            /// </summary>
            public string dm
            {
                get { return _dm; }
                set
                {
                    _dm = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("dm"));
                }
            }

            /// <summary>
            /// 弹幕增量
            /// </summary>
            public string dm_incr
            {
                get { return _dm_incr; }
                set
                {
                    _dm_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("dm_incr"));
                }
            }

            /// <summary>
            /// 电池
            /// </summary>
            public string elec
            {
                get { return _elec; }
                set
                {
                    _elec = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("elec"));
                }
            }

            /// <summary>
            /// 电池增量
            /// </summary>
            public string elec_incr
            {
                get { return _elec_incr; }
                set
                {
                    _elec_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("elec_incr"));
                }
            }

            /// <summary>
            /// 粉丝
            /// </summary>
            public string fan
            {
                get { return _fan; }
                set
                {
                    _fan = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fan"));
                }
            }

            /// <summary>
            /// 粉丝增量
            /// </summary>
            public string fan_incr
            {
                get { return _fan_incr; }
                set
                {
                    _fan_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fan_incr"));
                }
            }

            /// <summary>
            /// 收藏
            /// </summary>
            public string fav
            {
                get { return _fav; }
                set
                {
                    _fav = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav"));
                }
            }

            /// <summary>
            /// 收藏增量
            /// </summary>
            public string fav_incr
            {
                get { return _fav_incr; }
                set
                {
                    _fav_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("fav_incr"));
                }
            }

            /// <summary>
            /// 激励计划
            /// </summary>
            public string growup
            {
                get { return _growup; }
                set
                {
                    _growup = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("growup"));
                }
            }

            /// <summary>
            /// 激励计划增量
            /// </summary>
            public string growup_incr
            {
                get { return _growup_incr; }
                set
                {
                    _growup_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("growup_incr"));
                }
            }

            /// <summary>
            /// 点赞
            /// </summary>
            public string like
            {
                get { return _like; }
                set
                {
                    _like = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like"));
                }
            }

            /// <summary>
            /// 点赞增量
            /// </summary>
            public string like_incr
            {
                get { return _like_incr; }
                set
                {
                    _like_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("like_incr"));
                }
            }

            /// <summary>
            /// 播放
            /// </summary>
            public string play
            {
                get { return _play; }
                set
                {
                    _play = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("play"));
                }
            }

            /// <summary>
            /// 播放增量
            /// </summary>
            public string play_incr
            {
                get { return _play_incr; }
                set
                {
                    _play_incr = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("play_incr"));
                }
            }

            /// <summary>
            /// 分享
            /// </summary>
            public string share
            {
                get { return _share; }
                set
                {
                    _share = TrimNumStr(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("share"));
                }
            }

            /// <summary>
            /// 分享增量
            /// </summary>
            public string share_incr
            {
                get { return _share_incr; }
                set
                {
                    _share_incr = TrimNumStr(value);
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
                        coin = obj["data"]["stat"]["coin"].ToString();
                        coin_incr = ((int)obj["data"]["stat"]["coin"] - (int)obj["data"]["stat"]["coin_last"]).ToString();
                        dm = obj["data"]["stat"]["dm"].ToString();
                        dm_incr = ((int)obj["data"]["stat"]["dm"] - (int)obj["data"]["stat"]["dm_last"]).ToString();
                        elec_incr = ((float)obj["data"]["stat"]["elec"] - (float)obj["data"]["stat"]["elec_last"]).ToString();
                        fan = obj["data"]["stat"]["fan"].ToString();
                        fan_incr = ((int)obj["data"]["stat"]["fan"] - (int)obj["data"]["stat"]["fan_last"]).ToString();
                        fav = obj["data"]["stat"]["fav"].ToString();
                        fav_incr = ((int)obj["data"]["stat"]["fav"] - (int)obj["data"]["stat"]["fav_last"]).ToString();
                        like = obj["data"]["stat"]["like"].ToString();
                        like_incr = ((int)obj["data"]["stat"]["like"] - (int)obj["data"]["stat"]["like_last"]).ToString();
                        play = obj["data"]["stat"]["play"].ToString();
                        play_incr = ((int)obj["data"]["stat"]["play"] - (int)obj["data"]["stat"]["play_last"]).ToString();
                        share = obj["data"]["stat"]["share"].ToString();
                        share_incr = ((int)obj["data"]["stat"]["share"] - (int)obj["data"]["stat"]["share_last"]).ToString();
                    }
                }

                elec = GetCharge();
                string[] tmp = GetGrowUp();
                growup_incr = tmp[0];
                growup = tmp[1];
                return this;
            }

            #endregion Public Methods

            #region Private Methods

            /// <summary>
            /// 获得充电数据
            /// </summary>
            /// <returns>电池数</returns>
            private string GetCharge()
            {
                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/elec/balance", Bas.account.Cookies);
                JObject obj = JObject.Parse(str);
                if ((int)obj["code"] == 0)
                {
                    return obj["data"]["wallet"]["sponsorBalance"].ToString();
                }
                else
                {
                    return "";
                }
            }

            /// <summary>
            /// 获得激励计划数据
            /// </summary>
            /// <returns>[0]:前天收入;[1]:本月收入</returns>
            private string[] GetGrowUp()
            {
                string str = Bas.GetHTTPBody("https://api.bilibili.com/studio/growup/web/up/summary", Bas.account.Cookies);
                JObject obj = JObject.Parse(str);
                if ((int)obj["code"] == 0)
                {
                    return new string[2] { obj["data"]["day_income"].ToString(), obj["data"]["income"].ToString() };
                }
                else
                {
                    return new string[2] { "", "" };
                }
            }

            #endregion Private Methods
        }

        #endregion Public Classes
    }
}
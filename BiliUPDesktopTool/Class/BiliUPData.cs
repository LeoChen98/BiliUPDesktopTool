using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
        /// 指示是否正在刷新数据。
        /// </summary>
        private bool IsRefreshing = false;

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
        /// 更改刷新间隔
        /// </summary>
        /// <param name="value">新的刷新间隔</param>
        public void ChangeInterval(int value)
        {
            Refresher.Change(0, value);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns>刷新后的实例</returns>
        public BiliUPData Refresh()
        {
            if (Bas.account.Islogin == true && !IsRefreshing)
            {
                IsRefreshing = true;
                article.Refresh();
                video.Refresh();
                IsRefreshing = false;
            }
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
            private int _coin_real, _coin_real_last, _fav_real, _fav_real_last, _like_real, _like_real_last, _reply_real, _reply_real_last, _share_real, _share_real_last, _view_real, _view_real_last;
            private DateTime? LastTime = null;

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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _coin + _coin_real - _coin_real_last;
                    }
                    else
                    {
                        return _coin;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _coin_incr + _coin_real - _coin_real_last;
                    }
                    else
                    {
                        return _coin_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _fav + _fav_real - _fav_real_last;
                    }
                    else
                    {
                        return _fav;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _fav_incr + _fav_real - _fav_real_last;
                    }
                    else
                    {
                        return _fav_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _like + _like_real - _like_real_last;
                    }
                    else
                    {
                        return _like;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _like_incr + _like_real - _like_real_last;
                    }
                    else
                    {
                        return _like_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _reply + _reply_real - _reply_real_last;
                    }
                    else
                    {
                        return _reply;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _reply_incr + _reply_real - _reply_real_last;
                    }
                    else
                    {
                        return _reply_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _share + _share_real - _share_real_last;
                    }
                    else
                    {
                        return _share;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _share_incr + _share_real - _share_real_last;
                    }
                    else
                    {
                        return _share_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _view + _view_real - _view_real_last;
                    }
                    else
                    {
                        return _view;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _view_incr + _view_real - _view_real_last;
                    }
                    else
                    {
                        return _view_incr;
                    }
                }
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
                GetRealTime();

                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/h5/data/article", Bas.account.Cookies, "https://member.bilibili.com/studio/gabriel/data-center/overview");
                if (!string.IsNullOrEmpty(str))
                {
                    try
                    {
                        Article_Data_Template obj = JsonConvert.DeserializeObject<Article_Data_Template>(str);

                        if (obj.code == 0)
                        {
                            coin = obj.data.stat.coin;
                            coin_incr = obj.data.stat.incr_coin;
                            fav = obj.data.stat.fav;
                            fav_incr = obj.data.stat.incr_fav;
                            like = obj.data.stat.like;
                            like_incr = obj.data.stat.incr_like;
                            reply = obj.data.stat.reply;
                            reply_incr = obj.data.stat.incr_reply;
                            share = obj.data.stat.share;
                            share_incr = obj.data.stat.incr_share;
                            view = obj.data.stat.view;
                            view_incr = obj.data.stat.incr_view;
                        }
                    }
                    catch
                    {
                    }
                }

                return this;
            }

            #endregion Public Methods

            #region Private Methods

            /// <summary>
            /// 清除旧数据
            /// </summary>
            private void CleanData()
            {
                _coin_real = 0;
                _fav_real = 0;
                _like_real = 0;
                _reply_real = 0;
                _share_real = 0;
                _view_real = 0;
                if (LastTime == null || DateTime.Compare(((DateTime)LastTime).AddDays(1), DateTime.Now) <= 0)
                {
                    _coin_real_last = 0;
                    _fav_real_last = 0;
                    _like_real_last = 0;
                    _reply_real_last = 0;
                    _share_real_last = 0;
                    _view_real_last = 0;
                }
            }

            /// <summary>
            /// 获取实时数据
            /// </summary>
            private void GetRealTime()
            {
                CleanData();  //清除旧数据

                bool IsChangeLast = false;
                int pn = 1;
                Article_Real_Data_Template obj = new Article_Real_Data_Template();
                do
                {
                    string str = Bas.GetHTTPBody("https://api.bilibili.com/x/article/creative/article/list?group=0&sort=&pn=" + pn, Bas.account.Cookies, "https://member.bilibili.com/v2");
                    if (!string.IsNullOrEmpty(str))
                    {
                        try
                        {
                            obj = JsonConvert.DeserializeObject<Article_Real_Data_Template>(str);
                            if (obj.code == 0)
                            {
                                foreach (Article_Real_Data_Template.Data_Template.Article_Template i in obj.artlist.articles)
                                {
                                    _coin_real += i.stats.coin;
                                    _fav_real += i.stats.favorite;
                                    _like_real += i.stats.like;
                                    _reply_real += i.stats.reply;
                                    _share_real += i.stats.share;
                                    _view_real += i.stats.view;

                                    if (LastTime == null || DateTime.Compare(((DateTime)LastTime).AddDays(1), DateTime.Now) <= 0)
                                    {
                                        _coin_real_last += i.stats.coin;
                                        _fav_real_last += i.stats.favorite;
                                        _like_real_last += i.stats.like;
                                        _reply_real_last += i.stats.reply;
                                        _share_real_last += i.stats.share;
                                        _view_real_last += i.stats.view;

                                        IsChangeLast = true;
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                } while (++pn * obj.artlist.page.ps <= obj.artlist.page.total);

                if (IsChangeLast)
                    LastTime = DateTime.Compare(DateTime.Now.Date.AddHours(12), DateTime.Now) <= 0 ? DateTime.Now.Date.AddHours(12) : DateTime.Now.Date.AddDays(-1).AddHours(12);//设定为当天12点数据
            }

            #endregion Private Methods

            #region Private Classes

            /// <summary>
            /// 专栏总览数据模板
            /// </summary>
            private class Article_Data_Template
            {
                #region Public Fields

                public int code = -1;
                public Data_Template data = new Data_Template();

                #endregion Public Fields

                #region Public Classes

                public class Data_Template
                {
                    #region Public Fields

                    public Stat_Template stat;

                    #endregion Public Fields

                    #region Public Classes

                    public class Stat_Template
                    {
                        #region Public Fields

                        public int coin, fav, like, reply, share, view;
                        public int incr_coin, incr_fav, incr_like, incr_reply, incr_share, incr_view;

                        #endregion Public Fields
                    }

                    #endregion Public Classes
                }

                #endregion Public Classes
            }

            /// <summary>
            /// 专栏实时数据模板
            /// </summary>
            private class Article_Real_Data_Template
            {
                #region Public Fields

                public Data_Template artlist = new Data_Template();
                public int code;

                #endregion Public Fields

                #region Public Classes

                public class Data_Template
                {
                    #region Public Fields

                    public Article_Template[] articles;
                    public Page_Template page = new Page_Template() { pn = 0, ps = 10, total = 0 };

                    #endregion Public Fields

                    #region Public Classes

                    public class Article_Template
                    {
                        #region Public Fields

                        public Stats_Template stats;

                        #endregion Public Fields

                        #region Public Classes

                        public class Stats_Template
                        {
                            #region Public Fields

                            public int view, favorite, like, reply, share, coin, dynamic;

                            #endregion Public Fields
                        }

                        #endregion Public Classes
                    }

                    public class Page_Template
                    {
                        #region Public Fields

                        public int pn, ps, total;

                        #endregion Public Fields
                    }

                    #endregion Public Classes
                }

                #endregion Public Classes
            }

            #endregion Private Classes
        }

        /// <summary>
        /// 视频类
        /// </summary>
        public class Video : INotifyPropertyChanged
        {
            #region Private Fields

            private int _coin, _coin_incr, _dm, _dm_incr, _fan, _fan_incr, _fav, _fav_incr, _like, _like_incr, _play, _play_incr, _share, _share_incr, _comment, _comment_incr;
            private int _coin_real, _coin_real_last, _dm_real, _dm_real_last, _fav_real, _fav_real_last, _like_real, _like_real_last, _play_real, _play_real_last, _share_real, _share_real_last, _comment_real, _comment_real_last;
            private double _elec, _elec_last, _elec_incr, _elec_total, _growup, _growup_incr;
            private DateTime? LastTime = null;

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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _coin + _coin_real - _coin_real_last;
                    }
                    else
                    {
                        return _coin;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _coin_incr + _coin_real - _coin_real_last;
                    }
                    else
                    {
                        return _coin_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _comment + _comment_real - _comment_real_last;
                    }
                    else
                    {
                        return _comment;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _comment_incr + _comment_real - _comment_real_last;
                    }
                    else
                    {
                        return _comment_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _dm + _dm_real - _dm_real_last;
                    }
                    else
                    {
                        return _dm;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _dm_incr + _dm_real - _dm_real_last;
                    }
                    else
                    {
                        return _dm_incr;
                    }
                }
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
                get
                {
                    return _elec - _elec_last + _elec_incr;
                }
                set
                {
                    _elec_incr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("elec_incr"));
                }
            }

            /// <summary>
            /// 电池总量
            /// </summary>
            public double elec_total
            {
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _elec_total + _elec - _elec_last;
                    }
                    else
                    {
                        return _elec_total;
                    }
                }
                set
                {
                    _elec_total = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("elec_total"));
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _fav + _fav_real - _fav_real_last;
                    }
                    else
                    {
                        return _fav;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _fav_incr + _fav_real - _fav_real_last;
                    }
                    else
                    {
                        return _fav_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _like + _like_real - _like_real_last;
                    }
                    else
                    {
                        return _like;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _like_incr + _like_real - _like_real_last;
                    }
                    else
                    {
                        return _like_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _play + _play_real - _play_real_last;
                    }
                    else
                    {
                        return _play;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _play_incr + _play_real - _play_real_last;
                    }
                    else
                    {
                        return _play_incr;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _share + _share_real - _share_real_last;
                    }
                    else
                    {
                        return _share;
                    }
                }
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
                get
                {
                    if (Bas.settings.IsRealTime)
                    {
                        return _share_incr + _share_real - _share_real_last;
                    }
                    else
                    {
                        return _share_incr;
                    }
                }
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
                GetRealTime();

                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/h5/data/overview?type=0", Bas.account.Cookies, "https://member.bilibili.com/studio/gabriel/data-center/overview");
                if (!string.IsNullOrEmpty(str))
                {
                    try
                    {
                        Video_Data_Template obj = JsonConvert.DeserializeObject<Video_Data_Template>(str);
                        if (obj.code == 0)
                        {
                            coin = obj.data.stat.coin;
                            coin_incr = obj.data.stat.coin - obj.data.stat.coin_last;
                            comment = obj.data.stat.comment;
                            comment_incr = obj.data.stat.comment - obj.data.stat.comment_last;
                            dm = obj.data.stat.dm;
                            dm_incr = obj.data.stat.dm - obj.data.stat.dm_last;
                            fav = obj.data.stat.fav;
                            fav_incr = obj.data.stat.fav - obj.data.stat.fav_last;
                            like = obj.data.stat.like;
                            like_incr = obj.data.stat.like - obj.data.stat.like_last;
                            play = obj.data.stat.play;
                            play_incr = obj.data.stat.play - obj.data.stat.play_last;
                            share = obj.data.stat.share;
                            share_incr = obj.data.stat.share - obj.data.stat.share_last;

                            fan = obj.data.stat.fan;
                            fan_incr = obj.data.stat.fan - obj.data.stat.fan_last;

                            double tmp1 = GetCharge();
                            elec = tmp1 == -1 ? obj.data.stat.elec : tmp1;
                            elec_incr = obj.data.stat.elec - obj.data.stat.elec_last;
                            elec_total = obj.data.stat.elec;
                        }
                    }
                    catch
                    {
                    }
                }

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
            /// 清除旧数据
            /// </summary>
            private void CleanData()
            {
                _coin_real = 0;
                _dm_real = 0;
                _fav_real = 0;
                _like_real = 0;
                _play_real = 0;
                _share_real = 0;
                _comment_real = 0;
                if (LastTime == null || DateTime.Compare(((DateTime)LastTime).AddDays(1), DateTime.Now) <= 0)
                {
                    _coin_real_last = 0;
                    _dm_real_last = 0;
                    _fav_real_last = 0;
                    _like_real_last = 0;
                    _play_real_last = 0;
                    _share_real_last = 0;
                    _comment_real_last = 0;
                    _elec_last = GetCharge();
                }
            }

            /// <summary>
            /// 获得充电数据
            /// </summary>
            /// <returns>电池数</returns>
            private double GetCharge()
            {
                string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/elec/balance", Bas.account.Cookies);
                if (!string.IsNullOrEmpty(str))
                {
                    try
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
                    catch
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
                    try
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
                    catch
                    {
                        return new double[2] { -1, -1 };
                    }
                }
                else { return new double[2] { -1, -1 }; }
            }

            /// <summary>
            /// 获取实时数据
            /// </summary>
            private void GetRealTime()
            {
                CleanData();    //清除旧数据

                bool IsChangeLast = false;
                int pn = 1;
                Video_Real_Data_Template obj = new Video_Real_Data_Template();

                do
                {
                    string str = Bas.GetHTTPBody("https://member.bilibili.com/x/web/archives?status=is_pubing%2Cpubed%2Cnot_pubed&pn=" + pn + "&ps=10&coop=1", Bas.account.Cookies, "https://member.bilibili.com/v2");

                    if (!string.IsNullOrEmpty(str))
                    {
                        try
                        {
                            obj = JsonConvert.DeserializeObject<Video_Real_Data_Template>(str);

                            if (obj.code == 0)
                            {
                                foreach (Video_Real_Data_Template.Data_Template.Video_Template i in obj.data.arc_audits)
                                {
                                    _coin_real += i.stat.coin;
                                    _dm_real += i.stat.danmaku;
                                    _fav_real += i.stat.favorite;
                                    _like_real += i.stat.like;
                                    _play_real += i.stat.view;
                                    _share_real += i.stat.share;
                                    _comment_real += i.stat.reply;

                                    if (LastTime == null || DateTime.Compare(((DateTime)LastTime).AddDays(1), DateTime.Now) <= 0)
                                    {
                                        _coin_real_last += i.stat.coin;
                                        _dm_real_last += i.stat.danmaku;
                                        _fav_real_last += i.stat.favorite;
                                        _like_real_last += i.stat.like;
                                        _play_real_last += i.stat.view;
                                        _share_real_last += i.stat.share;
                                        _comment_real_last += i.stat.reply;

                                        IsChangeLast = true;
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                } while (++pn * obj.data.page.ps <= obj.data.page.count);

                if (IsChangeLast)
                    LastTime = DateTime.Compare(DateTime.Now.Date.AddHours(12), DateTime.Now) <= 0 ? DateTime.Now.Date.AddHours(12) : DateTime.Now.Date.AddDays(-1).AddHours(12);//设定为当天12点数据
            }

            #endregion Private Methods

            #region Private Classes

            /// <summary>
            /// 视频总览数据模板
            /// </summary>
            private class Video_Data_Template
            {
                #region Public Fields

                public int code;
                public Data_Template data = new Data_Template();

                #endregion Public Fields

                #region Public Classes

                public class Data_Template
                {
                    #region Public Fields

                    public Stat_Template stat = new Stat_Template();

                    #endregion Public Fields

                    #region Public Classes

                    public class Stat_Template
                    {
                        #region Public Fields

                        public int coin, comment, dm, fav, like, play, share, fan;
                        public int coin_last, comment_last, dm_last, fav_last, like_last, play_last, share_last, fan_last;
                        public double elec, elec_last;

                        #endregion Public Fields
                    }

                    #endregion Public Classes
                }

                #endregion Public Classes
            }

            /// <summary>
            /// 视频实时数据模板
            /// </summary>
            private class Video_Real_Data_Template
            {
                #region Public Fields

                public int code;
                public Data_Template data = new Data_Template();

                #endregion Public Fields

                #region Public Classes

                public class Data_Template
                {
                    #region Public Fields

                    public Video_Template[] arc_audits;
                    public Page_Template page = new Page_Template() { pn = 0, ps = 10, count = 0 };

                    #endregion Public Fields

                    #region Public Classes

                    public class Page_Template
                    {
                        #region Public Fields

                        public int pn, ps, count;

                        #endregion Public Fields
                    }

                    public class Video_Template
                    {
                        #region Public Fields

                        public Stat_Template stat;

                        #endregion Public Fields

                        #region Public Classes

                        public class Stat_Template
                        {
                            #region Public Fields

                            public int view, danmaku, reply, favorite, coin, share, like;

                            #endregion Public Fields
                        }

                        #endregion Public Classes
                    }

                    #endregion Public Classes
                }

                #endregion Public Classes
            }

            #endregion Private Classes
        }

        #endregion Public Classes
    }
}
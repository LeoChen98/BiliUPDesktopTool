using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 常规设置类
    /// </summary>
    public class Settings : SettingsBase<Settings.SettingsTable>
    {
        #region Private Fields

        /// <summary>
        /// 设置配置文件保存位置
        /// </summary>
        private const string savepath = "Settings.dms";

        private static Settings instance;

        #endregion Private Fields

        #region Private Constructors

        /// <summary>
        /// 初始化设置类
        /// </summary>
        private Settings() : base(new SettingsTable(), savepath)
        {
            if (DataViewSelected == null)
            {
                DataViewSelected = new List<List<string>>() { new List<string> { "video", "play", "play_incr" }, new List<string> { "video", "fan", "fan_incr" }, new List<string> { "video", "growup", "growup_incr" }, new List<string> { "video", "elec", "elec_incr" } };
            }
        }

        #endregion Private Constructors

        #region Public Events

        public event EventHandler DataViewSelected_Changed;

        #endregion Public Events

        #region Public Properties

        public static Settings Instance
        {
            get
            {
                if (instance == null) instance = new Settings();
                return instance;
            }
        }

        /// <summary>
        /// 数据刷新间隔（单位：毫秒，默认值：60000（1分钟））
        /// </summary>
        public int DataRefreshInterval
        {
            get
            {
                return ST.DataRefreshInterval;
            }
            set
            {
                if (value <= 0) value = 60000;
                ST.DataRefreshInterval = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("DataRefreshInterval"));
                Save();
            }
        }

        /// <summary>
        /// 数据展示选择的项目
        /// </summary>
        public List<List<string>> DataViewSelected
        {
            get { return ST.DataViewSelected; }
            set
            {
                ST.DataViewSelected = value;
                Save();
            }
        }

        /// <summary>
        /// 指示是否自动检查更新
        /// </summary>
        public bool IsAutoCheckUpdate
        {
            get { return ST.IsAutoCheckUpdate; }
            set
            {
                ST.IsAutoCheckUpdate = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("IsAutoCheckUpdate"));
                Save();
            }
        }

        public bool IsDataViewerDisplay
        {
            get { return ST.IsDataViewerDisplay; }
            set
            {
                ST.IsDataViewerDisplay = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("IsDataViewerDisplay"));
                Save();
            }
        }

        /// <summary>
        /// 指示是否第一次启动
        /// </summary>
        public bool IsFirstRun
        {
            get
            {
                return ST.IsFirstRun;
            }
            set
            {
                ST.IsFirstRun = value;
                Save();
            }
        }

        /// <summary>
        /// 指示可选实时数据是否启动实时
        /// </summary>
        public bool IsRealTime
        {
            get { return ST.IsRealTime; }
            set
            {
                ST.IsRealTime = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("IsRealTime"));
                Save();
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 调起列表变更事件
        /// </summary>
        public void DataViewSelected_Changed_Send()
        {
            DataViewSelected_Changed(this, new EventArgs());
            Save();
        }

        #endregion Public Methods

        #region Public Classes

        /// <summary>
        /// 数据表
        /// </summary>
        public class SettingsTable
        {
            #region Public Fields

            public int DataRefreshInterval = 60000;

            public List<List<string>> DataViewSelected;

            public bool IsAutoCheckUpdate = true;
            public bool IsDataViewerDisplay = true;
            public bool IsFirstRun = true;

            public bool IsRealTime = false;

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
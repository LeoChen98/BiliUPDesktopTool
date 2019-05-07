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

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化设置类
        /// </summary>
        public Settings() : base(new SettingsTable(), savepath)
        {
            if (DataViewSelected == null)
            {
                DataViewSelected = new List<string[]>() { new string[3] { "video", "play", "play_incr" }, new string[3] { "video", "fan", "fan_incr" }, new string[3] { "video", "growup", "growup_incr" }, new string[3] { "video", "elec", "elec_incr" } };
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler DataViewSelected_Changed;

        #endregion Public Events

        #region Public Properties

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
        public List<string[]> DataViewSelected
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

            public List<string[]> DataViewSelected;

            public bool IsAutoCheckUpdate = true;
            public bool IsFirstRun = true;

            public bool IsRealTime = false;

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
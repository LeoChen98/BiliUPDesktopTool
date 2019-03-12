using System.Collections.Generic;

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
        public Settings() : base(new SettingsTable(), savepath) { }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// 数据刷新间隔（单位：毫秒，默认值：60000（1分钟））
        /// </summary>
        public int DataRefreshInterval
        {
            get { return ST.DataRefreshInterval; }
            set { ST.DataRefreshInterval = value; }
        }

        /// <summary>
        /// 数据展示选择的项目
        /// </summary>
        public List<string[]> DataViewSelected
        {
            get { return ST.DataViewSelected; }
            set { ST.DataViewSelected = value; }
        }

        #endregion Public Properties

        #region Public Classes

        /// <summary>
        /// 数据表
        /// </summary>
        public class SettingsTable
        {
            #region Public Fields

            public int DataRefreshInterval = 60000;

            public List<string[]> DataViewSelected = new List<string[]>() { new string[3] { "video", "play", "play_incr" }, new string[3] { "video", "fan", "fan_incr" }, new string[3] { "video", "growup", "growup_incr" }, new string[3] { "video", "elec", "elec_incr" } };

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
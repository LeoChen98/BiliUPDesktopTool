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

        #endregion Public Properties

        #region Public Classes

        /// <summary>
        /// 数据表
        /// </summary>
        public class SettingsTable
        {
            #region Public Fields

            public int DataRefreshInterval = 60000;

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
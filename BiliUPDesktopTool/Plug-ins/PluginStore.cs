using System;

namespace BiliUPDesktopTool
{
    internal class PluginStore
    {
        #region Public Methods

        public static PluginInfo_Online[] GetList()
        {
            return null;
        }

        #endregion Public Methods

        #region Public Classes

        public class PluginInfo_Online
        {
            public string name;
            public string description;
            public int version;
            public string version_str;
            public DateTime updatetime;
            public string url;
            public string hash;
        }

        #endregion Public Classes
    }
}
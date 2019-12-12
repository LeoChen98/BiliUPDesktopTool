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
            #region Public Fields

            public string description;
            public string hash;
            public string name;
            public DateTime updatetime;
            public string url;
            public int version;
            public string version_str;

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
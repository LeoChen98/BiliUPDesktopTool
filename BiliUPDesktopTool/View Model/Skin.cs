using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Media;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 皮肤配置
    /// </summary>
    internal class Skin : INotifyPropertyChanged
    {
        #region Private Fields

        private SkinTable ST;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        public Skin()
        {
            ST = new SkinTable();
            if (File.Exists("\\Skin.dms"))
            {
                using (FileStream fs = File.OpenRead("\\Skin.dms"))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string str = reader.ReadToEnd();
                        OutputTable OT = JsonConvert.DeserializeObject<OutputTable>(str);
                        ST = OT.settings;
                    }
                }
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// 桌面窗体的背景颜色
        /// </summary>
        public Brush DesktopWnd_Bg
        {
            get { return ST.DesktopWnd_Bg; }
            set
            {
                ST.DesktopWnd_Bg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Bg"));
            }
        }

        /// <summary>
        /// 桌面窗体的Left属性
        /// </summary>
        public double DesktopWnd_Left
        {
            get { return ST.DesktopWnd_Left; }
            set
            {
                ST.DesktopWnd_Left = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Left"));
            }
        }

        /// <summary>
        /// 桌面窗体的透明度属性
        /// </summary>
        public double DesktopWnd_Opacity
        {
            get { return ST.DesktopWnd_Opacity; }
            set
            {
                ST.DesktopWnd_Opacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Opacity"));
            }
        }

        /// <summary>
        /// 桌面窗体的Top属性
        /// </summary>
        public double DesktopWnd_Top
        {
            get { return ST.DesktopWnd_Top; }
            set
            {
                ST.DesktopWnd_Top = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Top"));
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 保存皮肤配置文件
        /// </summary>
        public void Save()
        {
            OutputTable OT = new OutputTable(ST);
            string json = JsonConvert.SerializeObject(OT);
            using (FileStream fs = File.Open("Skin.dms", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
        }

        #endregion Public Methods

        #region Private Classes

        /// <summary>
        /// 设置导出类
        /// </summary>
        private class OutputTable
        {
            #region Public Fields

            public const int pid = 117;
            public const int version = 1;
            public SkinTable settings;

            #endregion Public Fields

            #region Public Constructors

            public OutputTable(SkinTable ST)
            {
                settings = ST;
            }

            #endregion Public Constructors
        }

        /// <summary>
        /// 皮肤设置值类
        /// </summary>
        private class SkinTable
        {
            #region Public Fields

            /// <summary>
            /// 桌面窗体的背景颜色
            /// </summary>
            [JsonIgnore]
            public Brush DesktopWnd_Bg = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));

            /// <summary>
            /// 桌面窗体的Left属性
            /// </summary>
            public double DesktopWnd_Left = 300;

            /// <summary>
            /// 桌面窗体的透明度
            /// </summary>
            public double DesktopWnd_Opacity = 1;

            /// <summary>
            /// 桌面窗体的Top属性
            /// </summary>
            public double DesktopWnd_Top = 300;

            #endregion Public Fields

            #region Public Properties

            /// <summary>
            /// 桌面窗体的背景颜色字符串
            /// </summary>
            public string DesktopWnd_Background
            {
                get
                {
                    return ((SolidColorBrush)DesktopWnd_Bg).Color.ToString();
                }
                set
                {
                    DesktopWnd_Bg = new SolidColorBrush(GetColor(value));
                }
            }

            #endregion Public Properties

            #region Private Methods

            /// <summary>
            /// 将sRGB字符串转换为Color实例
            /// </summary>
            /// <param name="ColorCode">sRBG字符串</param>
            /// <returns>Color实例</returns>
            private Color GetColor(string ColorCode)
            {
                return Color.FromArgb(byte.Parse(ColorCode.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                    byte.Parse(ColorCode.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                    byte.Parse(ColorCode.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                    byte.Parse(ColorCode.Substring(1, 2), NumberStyles.AllowHexSpecifier));
            }

            #endregion Private Methods
        }

        #endregion Private Classes
    }
}
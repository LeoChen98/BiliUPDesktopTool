using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 皮肤配置
    /// </summary>
    public class Skin : SettingsBase<Skin.SkinTable>
    {
        #region Private Fields

        /// <summary>
        /// 皮肤配置文件保存位置
        /// </summary>
        private const string savepath = "Skin.dms";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        public Skin() : base(new SkinTable(), savepath) { }

        #endregion Public Constructors

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
                PropertyChangedA(this, new PropertyChangedEventArgs("DesktopWnd_Bg"));
            }
        }

        /// <summary>
        /// 桌面窗体的字体颜色
        /// </summary>
        public Brush DesktopWnd_FontColor
        {
            get { return ST.DesktopWnd_FC; }
            set
            {
                ST.DesktopWnd_FC = value;
                PropertyChangedA(this, new PropertyChangedEventArgs("DesktopWnd_FontColor"));
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
                PropertyChangedA(this, new PropertyChangedEventArgs("DesktopWnd_Left"));
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
                PropertyChangedA(this, new PropertyChangedEventArgs("DesktopWnd_Opacity"));
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
                PropertyChangedA(this, new PropertyChangedEventArgs("DesktopWnd_Top"));
            }
        }

        #endregion Public Properties

        #region Public Classes

        /// <summary>
        /// 皮肤设置值类
        /// </summary>
        public class SkinTable
        {
            #region Public Fields

            /// <summary>
            /// 桌面窗体的背景颜色
            /// </summary>
            [JsonIgnore]
            public Brush DesktopWnd_Bg = new SolidColorBrush(Color.FromArgb(0x2D, 0x0, 0x0, 0x0));

            /// <summary>
            /// 桌面窗体的字体颜色
            /// </summary>
            [JsonIgnore]
            public Brush DesktopWnd_FC = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));

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

            /// <summary>
            /// 桌面窗体的字体颜色字符串
            /// </summary>
            public string DesktopWnd_FontColor
            {
                get
                {
                    return ((SolidColorBrush)DesktopWnd_FC).Color.ToString();
                }
                set
                {
                    DesktopWnd_FC = new SolidColorBrush(GetColor(value));
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

        #endregion Public Classes
    }
}
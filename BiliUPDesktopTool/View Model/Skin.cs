using Newtonsoft.Json.Linq;
using System.ComponentModel;
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

        private Brush _DesktopWnd_Bg = new SolidColorBrush(Color.FromArgb(225, 225, 225, 225));
        private double _DesktopWnd_Left = 300;
        private double _DesktopWnd_Opacity = 1;
        private double _DesktopWnd_Top = 300;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        public Skin()
        {
            if (File.Exists("\\Skin.dms"))
            {
                using (FileStream fs = File.OpenRead("\\Skin.dms"))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string str = reader.ReadToEnd();
                        JObject obj = JObject.Parse(str);

                        //具体代码
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
            get { return _DesktopWnd_Bg; }
            set
            {
                _DesktopWnd_Bg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Bg"));
            }
        }

        /// <summary>
        /// 桌面窗体的Left属性
        /// </summary>
        public double DesktopWnd_Left
        {
            get { return _DesktopWnd_Left; }
            set
            {
                _DesktopWnd_Left = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Left"));
            }
        }

        /// <summary>
        /// 桌面窗体的透明度属性
        /// </summary>
        public double DesktopWnd_Opacity
        {
            get { return _DesktopWnd_Opacity; }
            set
            {
                _DesktopWnd_Opacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopWnd_Opacity"));
            }
        }

        /// <summary>
        /// 桌面窗体的Top属性
        /// </summary>
        public double DesktopWnd_Top
        {
            get { return _DesktopWnd_Top; }
            set
            {
                _DesktopWnd_Top = value;
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
        }

        #endregion Public Methods
    }
}
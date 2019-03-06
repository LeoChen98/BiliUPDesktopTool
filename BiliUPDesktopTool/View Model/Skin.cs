using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.IO;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 皮肤配置
    /// </summary>
    internal class Skin : INotifyPropertyChanged
    {
        #region Private Fields

        private double _DesktopWnd_Left;
        private double _DesktopWnd_Opacity = 1;
        private double _DesktopWnd_Top;

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
    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DataViewerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DataViewerPanel : UserControl
    {
        #region Public Fields

        // Using a DependencyProperty as the backing store for DataModes. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty DataModesProperty =
            DependencyProperty.Register("DataModes", typeof(List<string[]>), typeof(UserControl), new PropertyMetadata(null, new PropertyChangedCallback(ModesChanged)));

        #endregion Public Fields

        #region Public Constructors

        public DataViewerPanel()
        {
            InitializeComponent();

            Bas.settings.DataViewSelected_Changed += ModesChanged;

            //初始化数据
            ChangeView();
        }

        #endregion Public Constructors

        #region Public Properties

        public List<string[]> DataModes
        {
            get { return (List<string[]>)GetValue(DataModesProperty); }
            set { SetValue(DataModesProperty, value); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 更新展示
        /// </summary>
        public void ChangeView()
        {
            if (Bas.biliupdata != null)
            {
                DataViewer[] viewers = new DataViewer[4] { ViewerLT, ViewerRT, ViewerLB, ViewerRB };
                for (int i = 0; i < viewers.Length; i++)
                {
                    viewers[i].Title = GetDataTile(Bas.settings.DataViewSelected[i]);
                    viewers[i].DataMode = Bas.settings.DataViewSelected[i];
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void ModesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataViewerPanel s = d as DataViewerPanel;
            s.ChangeView();
        }

        /// <summary>
        /// 初始化绑定
        /// </summary>
        private void BindingInit()
        {
            Binding bind_R_color = new Binding()
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_FontColor")
            };
            SetBinding(ForegroundProperty, bind_R_color);
        }

        /// <summary>
        /// 获取数据标题
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string GetDataTile(List<string> v)
        {
            if (v == null) return "";
            string title = "";
            switch (v[0])
            {
                case "video":
                    title += "视频";
                    break;

                case "article":
                    title += "专栏";
                    break;

                default:
                    break;
            }

            switch (v[1])
            {
                case "coin":
                    title += "硬币";
                    break;

                case "fav":
                    title += "收藏";
                    break;

                case "like":
                    title += "点赞";
                    break;

                case "reply":
                case "comment":
                    title += "评论";
                    break;

                case "share":
                    title += "分享";
                    break;

                case "view":
                    title += "点击";
                    break;

                case "dm":
                    title += "弹幕";
                    break;

                case "elec":
                    title = "电池";
                    break;

                case "fan":
                    title = "粉丝";
                    break;

                case "growup":
                    title = "创作激励";
                    break;

                case "play":
                    title += "播放";
                    break;

                default:
                    break;
            }
            return title;
        }

        private void ModesChanged(object sender, EventArgs e)
        {
            ChangeView();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Parent.GetType() == typeof(DesktopWindow))
            {
                BindingInit();
            }
        }

        #endregion Private Methods
    }
}
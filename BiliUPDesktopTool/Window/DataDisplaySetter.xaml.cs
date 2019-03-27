using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DataDisplaySetter.xaml 的交互逻辑
    /// </summary>
    public partial class DataDisplaySetter : Window, INotifyPropertyChanged
    {
        #region Private Fields

        /// <summary>
        /// 当前选择的DataViewer
        /// </summary>
        private DataViewer DV_Selected;

        #endregion Private Fields

        #region Public Constructors

        public DataDisplaySetter()
        {
            InitializeComponent();

            BindingInit();
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public Visibility Notice_Article_Visbility
        {
            get
            {
                if (Scroll_Article != null && Scroll_Article.Visibility == Visibility.Visible && Scroll_Article.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            //set { SetValue(Notice_Article_VisbilityProperty, value); }
        }

        public Visibility Notice_Overall_Visbility
        {
            get
            {
                if (Scroll_Overall != null && Scroll_Overall.Visibility == Visibility.Visible && Scroll_Overall.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            //set { SetValue(Notice_Overall_VisbilityProperty, value); }
        }

        public Visibility Notice_Video_Visbility
        {
            get
            {
                if (Scroll_Video != null && Scroll_Video.Visibility == Visibility.Visible && Scroll_Video.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            //set { SetValue(Notice_Video_VisbilityProperty, value); }
        }

        #endregion Public Properties

        #region Private Methods

        private void BindingInit()
        {
            Binding bind_RealMode = new Binding()
            {
                Source = Bas.settings,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("IsRealTime")
            };
            TBN_RealMode.SetBinding(ToggleButton.StatusProperty, bind_RealMode);
        }

        private void DataViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DV_Selected != null) DV_Selected.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff)); ;
            DataViewer s = sender as DataViewer;
            s.Background = new SolidColorBrush(Color.FromArgb(0x66, 0xff, 0xff, 0xff));
            DV_Selected = sender as DataViewer;

            lbl_DataTitle.Content = GetDataTile(s.DataMode);
            JObject obj = JObject.Parse(Properties.Resources.DataDesc);
            TBK_DataDesc.Text = obj[s.DataMode[0]][s.DataMode[1]].ToString();

            ST_Block1.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));
            ST_Block2.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));
            ST_Block3.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));
            ST_Block4.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));

            int id = -1;

            foreach (string[] item in Bas.settings.DataViewSelected)
            {
                if (item != null && item[0] == s.DataMode[0] && item[1] == s.DataMode[1])
                {
                    id = Bas.settings.DataViewSelected.IndexOf(item);
                    break;
                }
            }

            switch (id)
            {
                case 0:
                    ST_Block1.Background = new SolidColorBrush(Color.FromArgb(0x66, 0xff, 0xff, 0xff));
                    break;

                case 1:
                    ST_Block2.Background = new SolidColorBrush(Color.FromArgb(0x66, 0xff, 0xff, 0xff));
                    break;

                case 2:
                    ST_Block3.Background = new SolidColorBrush(Color.FromArgb(0x66, 0xff, 0xff, 0xff));
                    break;

                case 3:
                    ST_Block4.Background = new SolidColorBrush(Color.FromArgb(0x66, 0xff, 0xff, 0xff));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 获取数据标题
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string GetDataTile(string[] v)
        {
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

        private void Scroll_Article_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Scroll_Article.LineLeft();
            }
            else
            {
                Scroll_Article.LineRight();
            }
        }

        private void Scroll_Overall_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Scroll_Overall.LineLeft();
            }
            else
            {
                Scroll_Overall.LineRight();
            }
        }

        private void Scroll_Video_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Scroll_Video.LineLeft();
            }
            else
            {
                Scroll_Video.LineRight();
            }
        }

        private void ST_Block1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Label s = sender as System.Windows.Controls.Label;

            ST_Block1.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));
            ST_Block2.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));
            ST_Block3.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));
            ST_Block4.Background = new SolidColorBrush(Color.FromArgb(0x01, 0xff, 0xff, 0xff));

            int id = -1;

            foreach (string[] item in Bas.settings.DataViewSelected)
            {
                if (item != null && item[0] == DV_Selected.DataMode[0] && item[1] == DV_Selected.DataMode[1])
                {
                    id = Bas.settings.DataViewSelected.IndexOf(item);
                    break;
                }
            }

            if (id != -1) Bas.settings.DataViewSelected[id] = null;

            switch (s.Content)
            {
                case "左上":
                    Bas.settings.DataViewSelected[0] = DV_Selected.DataMode;
                    break;

                case "右上":
                    Bas.settings.DataViewSelected[1] = DV_Selected.DataMode;
                    break;

                case "左下":
                    Bas.settings.DataViewSelected[2] = DV_Selected.DataMode;
                    break;

                case "右下":
                    Bas.settings.DataViewSelected[3] = DV_Selected.DataMode;
                    break;

                default:
                    break;
            }
            Bas.settings.PropertyChangedA(Bas.settings, new PropertyChangedEventArgs("DataViewSelected"));
            Bas.settings.DataViewSelected_Changed_Send();

            s.Background = new SolidColorBrush(Color.FromArgb(0x66, 0xff, 0xff, 0xff));
        }

        private void Tab_Article_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Scroll_Overall.Visibility = Visibility.Hidden;
            Scroll_Video.Visibility = Visibility.Hidden;
            Scroll_Article.Visibility = Visibility.Visible;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Overall_Visbility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Video_Visbility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Article_Visbility"));
            Bg_Article.Stroke = new SolidColorBrush(Color.FromArgb(0x66, 0x42, 0xb7, 0xdd));
            Bg_Article.Fill = new SolidColorBrush(Color.FromArgb(0x66, 0x42, 0xb7, 0xdd));
            Bg_Overall.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Overall.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Video.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Video.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
        }

        private void Tab_Overall_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Scroll_Overall.Visibility = Visibility.Visible;
            Scroll_Video.Visibility = Visibility.Hidden;
            Scroll_Article.Visibility = Visibility.Hidden;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Overall_Visbility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Video_Visbility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Article_Visbility"));
            Bg_Overall.Stroke = new SolidColorBrush(Color.FromArgb(0x66, 0x42, 0xb7, 0xdd));
            Bg_Overall.Fill = new SolidColorBrush(Color.FromArgb(0x66, 0x42, 0xb7, 0xdd));
            Bg_Article.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Article.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Video.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Video.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
        }

        private void Tab_Video_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Scroll_Overall.Visibility = Visibility.Hidden;
            Scroll_Video.Visibility = Visibility.Visible;
            Scroll_Article.Visibility = Visibility.Hidden;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Overall_Visbility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Video_Visbility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notice_Article_Visbility"));
            Bg_Video.Stroke = new SolidColorBrush(Color.FromArgb(0x66, 0x42, 0xb7, 0xdd));
            Bg_Video.Fill = new SolidColorBrush(Color.FromArgb(0x66, 0x42, 0xb7, 0xdd));
            Bg_Overall.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Overall.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Article.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
            Bg_Article.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x42, 0xb7, 0xdd));
        }

        #endregion Private Methods
    }
}
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
        //public static readonly DependencyProperty Notice_Article_VisbilityProperty =
        //    DependencyProperty.Register("Notice_Article_Visbility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Hidden));

        //public static readonly DependencyProperty Notice_Overall_VisbilityProperty =
        //            DependencyProperty.Register("Notice_Overall_Visbility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Hidden));

        //public static readonly DependencyProperty Notice_Video_VisbilityProperty =
        //    DependencyProperty.Register("Notice_Video_Visbility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Hidden));

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
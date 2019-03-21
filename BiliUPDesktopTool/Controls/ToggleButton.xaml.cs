using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// ToggleButton.xaml 的交互逻辑
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        #region Public Fields

        // Using a DependencyProperty as the backing store for Status. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(bool), typeof(UserControl), new PropertyMetadata(false));

        #endregion Public Fields

        #region Public Constructors

        public ToggleButton()
        {
            InitializeComponent();

            StatusChanged += ToggleButton_StatusChanged;
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void StatusChanged_Handler(object sender, object e);

        #endregion Public Delegates

        #region Public Events

        public event StatusChanged_Handler StatusChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// 按键状态
        /// </summary>
        public bool Status
        {
            get { return (bool)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); StatusChanged(this, value); }
        }

        #endregion Public Properties

        #region Private Methods

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Status)
            {
                Status = false;
            }
            else
            {
                Status = true;
            }
        }

        private void ToggleButton_StatusChanged(object sender, object e)
        {
            if ((bool)e)
            {
                ThicknessAnimation an = new ThicknessAnimation();
                an.From = new Thickness(0, 0, 36, 0);
                an.To = new Thickness(36, 0, 0, 0);
                an.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                slider.BeginAnimation(MarginProperty, an);
                Background = new SolidColorBrush(Color.FromArgb(0xff, 0x21, 0xc7, 0x3b));
            }
            else
            {
                ThicknessAnimation an = new ThicknessAnimation();
                an.From = new Thickness(36, 0, 0, 0);
                an.To = new Thickness(0, 0, 36, 0);
                an.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                slider.BeginAnimation(MarginProperty, an);
                Background = new SolidColorBrush(Color.FromArgb(0xff, 0x96, 0x96, 0x96));
            }
        }

        #endregion Private Methods
    }
}
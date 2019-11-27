using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                ((Storyboard)FindResource("Status_On")).Begin();
            }
            else
            {
                ((Storyboard)FindResource("Status_Off")).Begin();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Status)
            {
                ((Storyboard)FindResource("Status_On")).Begin();
            }
            else
            {
                ((Storyboard)FindResource("Status_Off")).Begin();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StatusChanged -= ToggleButton_StatusChanged;
        }

        #endregion Private Methods
    }
}
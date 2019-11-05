using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        #region Public Fields

        public static readonly DependencyProperty FinishedProperty =
            DependencyProperty.Register("Finished", typeof(bool), typeof(Window), new PropertyMetadata(false, new PropertyChangedCallback((o, e) => { if ((bool)e.NewValue == true) (o as UpdateWindow).Close(); })));

        #endregion Public Fields

        #region Public Constructors

        public UpdateWindow()
        {
            InitializeComponent();

            BindingInit();

            MsgBoxPushHelper.PushMsg += MsgBoxPushHelper_PushMsg;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Finished
        {
            get { return (bool)GetValue(FinishedProperty); }
            set { SetValue(FinishedProperty, value); }
        }

        #endregion Public Properties

        #region Private Methods

        private void BindingInit()
        {
            Binding bind_IsFinished = new Binding()
            {
                Source = Update.Instance,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath("IsFinished")
            };
            SetBinding(FinishedProperty, bind_IsFinished);

            Binding bind_UpdateText = new Binding()
            {
                Source = Update.Instance,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath("UpdateText")
            };
            TB_UpdateText.SetBinding(TextBox.TextProperty, bind_UpdateText);

            Binding bind_Status = new Binding()
            {
                Source = Update.Instance,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath("Status")
            };
            LBL_Status.SetBinding(ContentProperty, bind_Status);
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTN_Update_Click(object sender, RoutedEventArgs e)
        {
            BTN_Cancel.IsEnabled = false;
            BTN_Update.IsEnabled = false;
            Update.Instance.DoUpdate();
        }

        private void MsgBoxPushHelper_PushMsg(string msg, MsgBoxPushHelper.MsgType type = MsgBoxPushHelper.MsgType.Info)
        {
            if (IsActive && IsVisible)
            {
                msgbox.Show(msg);
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion Private Methods
    }
}
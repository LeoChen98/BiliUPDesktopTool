using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// MsgBox.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBox : UserControl, INotifyPropertyChanged
    {
        #region Public Constructors

        public MsgBox()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public RelayCommand command { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Show(string msg, Action command = null, MsgBoxPushHelper.MsgType type = MsgBoxPushHelper.MsgType.Info)
        {
            ShowBox.Content = msg;
            this.command = command == null ? null : new RelayCommand(command);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("command"));

            FormattedText formattedText = new
               FormattedText(msg, System.Globalization.CultureInfo.CurrentCulture,
               System.Windows.FlowDirection.LeftToRight,
               new Typeface(ShowBox.FontFamily, ShowBox.FontStyle,
               ShowBox.FontWeight, ShowBox.FontStretch),
               ShowBox.FontSize, ShowBox.Foreground, null);

            Width = formattedText.Width + 40;
            Height = formattedText.Height + 20;

            (FindResource("Show") as Storyboard).Begin();
        }

        #endregion Public Methods
    }
}
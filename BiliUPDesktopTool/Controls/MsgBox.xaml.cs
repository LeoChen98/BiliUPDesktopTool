using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// MsgBox.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBox : UserControl
    {
        public MsgBox()
        {
            InitializeComponent();
        }


        public void Show(string msg)
        {
            Tbk_ShowBox.Text = msg;

            FormattedText formattedText = new
               FormattedText(msg, System.Globalization.CultureInfo.CurrentCulture,
               System.Windows.FlowDirection.LeftToRight,
               new Typeface(Tbk_ShowBox.FontFamily, Tbk_ShowBox.FontStyle,
               Tbk_ShowBox.FontWeight, Tbk_ShowBox.FontStretch),
               Tbk_ShowBox.FontSize, Tbk_ShowBox.Foreground, null);

            Width = formattedText.Width +40;
            Height = formattedText.Height + 20;

            (FindResource("Show") as Storyboard).Begin();
        }

        private void userControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}

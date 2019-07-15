using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// EventItem.xaml 的交互逻辑
    /// </summary>
    public partial class EventItem : UserControl
    {
        public EventItem()
        {
            InitializeComponent();
        }
        private EventInfo Info;
        public EventItem(EventInfo info)
        {
            InitializeComponent();

            Info = info;

            
        }

        public class EventInfo
        {
            public string Title, Desc, Start_Time, End_Time, Link;
        }

        bool IsMoreClicked = false;

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMoreClicked)
            {
                DoubleAnimation an = new DoubleAnimation();
                an.From = 180;
                an.To = 0;
                an.Duration = new Duration(TimeSpan.FromMilliseconds(250));
                trans.BeginAnimation(RotateTransform.AngleProperty, an);

                an.From = 122 + TBk_Desc.ActualHeight;
                an.To = 50;
                an.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                BeginAnimation(HeightProperty, an);

                IsMoreClicked = false;
            }
            else
            {

                DoubleAnimation an = new DoubleAnimation();
                an.From = 0;
                an.To = 180;
                an.Duration = new Duration(TimeSpan.FromMilliseconds(250));
                trans.BeginAnimation(RotateTransform.AngleProperty, an);

                an.From = 50;
                an.To = 122 + TBk_Desc.ActualHeight;
                an.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                BeginAnimation(HeightProperty, an);

                IsMoreClicked = true;
            }
        }

        private void Btn_EventPage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Info.Link);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TBk_EventTitle.Text = Info.Title;
            Lbl_Desc.Content = Info.Desc;
            Lbl_StartTime.Content = Info.Start_Time;
            Lbl_EndTime.Content = Info.End_Time;
        }
    }
}

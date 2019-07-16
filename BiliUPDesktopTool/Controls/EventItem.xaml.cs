using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// EventItem.xaml 的交互逻辑
    /// </summary>
    public partial class EventItem : UserControl
    {
        #region Private Fields

        private EventInfo Info;

        private bool IsMoreClicked = false;

        #endregion Private Fields

        #region Public Constructors

        public EventItem()
        {
            InitializeComponent();
        }

        public EventItem(EventInfo info)
        {
            InitializeComponent();

            Info = info;
        }

        #endregion Public Constructors

        #region Private Methods

        private void Btn_EventPage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Info.Link);
        }

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TBk_EventTitle.Text = Info.Title;
            Lbl_Desc.Content = Info.Desc;
            Lbl_StartTime.Content = Info.Start_Time;
            Lbl_EndTime.Content = Info.End_Time;
        }

        #endregion Private Methods

        #region Public Classes

        public class EventInfo
        {
            #region Public Fields

            public string Title, Desc, Start_Time, End_Time, Link;

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
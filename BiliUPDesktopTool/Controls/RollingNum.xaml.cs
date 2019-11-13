using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// RollingNum.xaml 的交互逻辑
    /// </summary>
    public partial class RollingNum : UserControl
    {
        #region Private Fields

        private const double CharacterHeight = 50;

        #endregion Private Fields

        #region Public Constructors

        public RollingNum()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 改变数字
        /// </summary>
        /// <param name="num">数字</param>
        public void ChangeNum(int num)
        {
            ThicknessAnimation an = new ThicknessAnimation
            {
                From = nums.Margin,
                Duration = new Duration(TimeSpan.FromMilliseconds(500))
            };

            switch (num)
            {
                case 9:
                    an.To = new Thickness(0,0,0,-6);
                    break;
                case 8:
                    an.To = new Thickness(0, 0, 0, -56);
                    break;
                case 7:
                    an.To = new Thickness(0, 0, 0, -106);
                    break;
                case 6:
                    an.To = new Thickness(0, 0, 0, -156);
                    break;
                case 5:
                    an.To = new Thickness(0, 0, 0, -206);
                    break;
                case 4:
                    an.To = new Thickness(0, 0, 0, -256);
                    break;
                case 3:
                    an.To = new Thickness(0, 0, 0, -306);
                    break;
                case 2:
                    an.To = new Thickness(0, 0, 0, -356);
                    break;
                case 1:
                    an.To = new Thickness(0, 0, 0, -406);
                    break;
                case 0:
                    an.To = new Thickness(0, 0, 0, -456);
                    break;
                case -1:
                    an.To = new Thickness(0, 0, 0, -515);
                    break;
                case -2:
                    an.To = new Thickness(0, 0, 0, -565);
                    break;
                case -3:
                    an.To = new Thickness(0, 0, 0, -600);
                    break;

                default:
                    break;
            }

            nums.BeginAnimation(MarginProperty, an);
        }

        #endregion Public Methods

        #region Private Methods

        private void BindingInit()
        {
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion Private Methods
    }
}
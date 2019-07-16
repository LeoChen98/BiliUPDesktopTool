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
            ThicknessAnimation an = new ThicknessAnimation();
            an.From = nums.Margin;
            //an.To = new Thickness(nums.Margin.Left, -Math.Abs((num % 10) * CharacterHeight), nums.Margin.Right, -450 + (num % 10) * CharacterHeight);
            an.To = new Thickness(nums.Margin.Left, nums.Margin.Top, nums.Margin.Right, -450 + (num % 10) * CharacterHeight);
            an.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            nums.BeginAnimation(MarginProperty, an);
        }

        #endregion Public Methods

        #region Private Methods

        private void BindingInit()
        {
            Binding bind_fontcolor = new Binding()
            {
                Source = Bas.skin,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_FontColor")
            };
            SetBinding(ForegroundProperty, bind_fontcolor);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Parent.GetValue(NameProperty).ToString() != "ViewPanel")
            {
                BindingInit();
            }
        }

        #endregion Private Methods
    }
}
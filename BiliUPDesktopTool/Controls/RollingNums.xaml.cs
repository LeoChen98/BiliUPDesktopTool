using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// RollingNums.xaml 的交互逻辑
    /// </summary>
    public partial class RollingNums : UserControl
    {
        #region Public Fields

        /// <summary>
        /// 数据依赖属性
        /// </summary>
        public static readonly DependencyProperty numProperty =
            DependencyProperty.Register("num", typeof(double), typeof(RollingNums), new PropertyMetadata((double)0, new PropertyChangedCallback(ChangeNum)));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// 指示当前是否正数
        /// </summary>
        private bool IsPostive = true;

        #endregion Private Fields

        #region Public Constructors

        public RollingNums()
        {
            InitializeComponent();

            ChangeNum(0);//初始化值
        }

        #endregion Public Constructors

        #region Public Delegates

        /// <summary>
        /// 正负变更委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PostiveAndNegativeChangedHandler(object sender, PostiveAndNegativeChangedEventArgs e);

        #endregion Public Delegates

        #region Public Events

        /// <summary>
        /// 正负变更事件
        /// </summary>
        public event PostiveAndNegativeChangedHandler PostiveAndNegativeChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// 数据
        /// </summary>
        public double num
        {
            get { return (double)GetValue(numProperty); }
            set
            {
                SetValue(numProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 改变数字（自动回调）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ChangeNum(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                RollingNums r = (RollingNums)sender;
                double v = Convert.ToDouble(e.NewValue);
                r.ChangeNum(v);
            }
        }

        /// <summary>
        /// 改变数字
        /// </summary>
        /// <param name="num">数字</param>
        public void ChangeNum(double num)
        {
            if (num >= 0 && IsPostive != true)
            {
                PostiveAndNegativeChanged?.Invoke(this, new PostiveAndNegativeChangedEventArgs { OldValue = IsPostive, NewValue = true });
                IsPostive = true;
            }
            else
            {
                PostiveAndNegativeChanged?.Invoke(this, new PostiveAndNegativeChangedEventArgs { OldValue = IsPostive, NewValue = false });
                IsPostive = false;
            }
            if (num != -1)
            {
                num = Math.Abs(num);
                string numstr = num.ToString();
                if (numstr.IndexOf("E+") >= 0)
                {
                    string[] numstmp = Regex.Split(numstr, "E+");
                    numstr = numstmp[0];
                    int x = 1;
                    if (numstmp[0].IndexOf(".") >= 0)
                    {
                        numstr = numstr.Replace(".", "");
                        x = numstmp[0].Length - numstmp[0].IndexOf(".");
                    }
                    for (int i = x - 1; i < int.Parse(numstmp[1]); i++)
                    {
                        numstr += "0";
                    }
                }
                int[] numids = new int[6];
                if (num >= 10000)
                {
                    if (num >= 100000000)
                    {
                        string tmp1 = numstr.Split('.')[0];
                        string tmp2 = tmp1.Substring(0, tmp1.Length - 8);//整数部分
                        string tmp3 = tmp1.Substring(tmp2.Length, 1);//小数部分

                        numids[5] = -2;
                        numids[4] = int.Parse(tmp1.Substring(tmp2.Length + 1, 1)) >= 5 ? int.Parse(tmp3) + 1 : int.Parse(tmp3);

                        if (numids[4] >= 10)
                        {
                            numids[4] -= 10;
                            tmp2 = (int.Parse(tmp2) + 1).ToString();
                        }

                        for (int i = 1; i <= 4; i++)
                        {
                            if (tmp2.Length - i >= 0)
                            {
                                numids[4 - i] = int.Parse(tmp2.Substring(tmp2.Length - i, 1));
                            }
                            else
                            {
                                if (4 - i == 3)
                                {
                                    numids[3] = 0;
                                }
                                else
                                {
                                    numids[4 - i] = -3;
                                }
                            }
                        }
                    }
                    else
                    {
                        string tmp1 = numstr.Split('.')[0];
                        string tmp2 = tmp1.Substring(0, tmp1.Length - 4);//整数部分
                        string tmp3 = tmp1.Substring(tmp2.Length, 1);//小数部分

                        numids[5] = -1;
                        numids[4] = int.Parse(tmp1.Substring(tmp2.Length + 1, 1)) >= 5 ? int.Parse(tmp3) + 1 : int.Parse(tmp3);

                        if (numids[4] >= 10)
                        {
                            numids[4] -= 10;
                            tmp2 = (int.Parse(tmp2) + 1).ToString();
                        }

                        for (int i = 1; i <= 4; i++)
                        {
                            if (tmp2.Length - i >= 0)
                            {
                                numids[4 - i] = int.Parse(tmp2.Substring(tmp2.Length - i, 1));
                            }
                            else
                            {
                                numids[4 - i] = -3;
                            }
                        }
                    }
                }
                else
                {
                    string[] tmp1 = numstr.Split('.');
                    for (int i = 1; i <= 4; i++)
                    {
                        if (tmp1[0].Length - i >= 0)
                        {
                            numids[4 - i] = int.Parse(tmp1[0].Substring(tmp1[0].Length - i, 1));
                        }
                        else
                        {
                            numids[4 - i] = -3;
                        }
                    }
                    if (tmp1.Length > 1)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (tmp1[1].Length - i > 0)
                            {
                                numids[4 + i] = int.Parse(tmp1[1].Substring(i, 1));
                            }
                            else
                            {
                                numids[4 + i] = 0;
                            }
                        }
                    }
                }
                N4.ChangeNum(numids[0]);
                N3.ChangeNum(numids[1]);
                N2.ChangeNum(numids[2]);
                N1.ChangeNum(numids[3]);
                if (numids[4] == 0 && numids[5] == 0)
                {
                    numpoint.Visibility = Visibility.Hidden;
                    P1.Visibility = Visibility.Hidden;
                    P2.Visibility = Visibility.Hidden;
                    ViewPanel.Margin = new Thickness(30, 0, -28, 0);
                }
                else
                {
                    numpoint.Visibility = Visibility.Visible;
                    P1.Visibility = Visibility.Visible;
                    P2.Visibility = Visibility.Visible;
                    ViewPanel.Margin = new Thickness(1, 0, 1, 0);
                    P1.ChangeNum(numids[4]);
                    P2.ChangeNum(numids[5]);
                }
            }
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
            if (Parent.GetValue(NameProperty).ToString() != "DV_Holder")
            {
                BindingInit();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearAllBindings(this);
        }

        #endregion Private Methods

        #region Public Classes

        /// <summary>
        /// 正负变更传参
        /// </summary>
        public class PostiveAndNegativeChangedEventArgs
        {
            #region Public Fields

            public bool NewValue;
            public bool OldValue;

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
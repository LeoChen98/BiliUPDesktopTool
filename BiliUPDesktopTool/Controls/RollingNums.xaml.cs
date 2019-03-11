using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// RollingNums.xaml 的交互逻辑
    /// </summary>
    public partial class RollingNums : UserControl
    {
        #region Public Constructors

        public RollingNums()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Methods

        public void ChangeNum(double num)
        {
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
                    numids[4] = int.Parse(tmp3);

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
                else
                {
                    string tmp1 = numstr.Split('.')[0];
                    string tmp2 = tmp1.Substring(0, tmp1.Length - 4);//整数部分
                    string tmp3 = tmp1.Substring(tmp2.Length, 1);//小数部分

                    numids[5] = -1;
                    numids[4] = int.Parse(tmp3);

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
            P1.ChangeNum(numids[4]);
            P2.ChangeNum(numids[5]);
        }

        #endregion Public Methods
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Forms;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 布尔值-可视转换器  parameter指示Visible对应的布尔值，以Hidden为相反值
    /// </summary>
    public class Bool2Visbility : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool)value;
            bool p = bool.Parse(parameter.ToString());
            if (v == p) return Visibility.Visible;
            else return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility v = (Visibility)value;
            bool p = bool.Parse(parameter.ToString());
            if ((v == Visibility.Visible) == p) return true;
            else return false;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 笔刷-颜色转换器
    /// </summary>
    public class Brush2ColorValue_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(SolidColorBrush))
            {
                return ((SolidColorBrush)value).Color;
            }
            else
            {
                return Colors.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 数据描述转换器
    /// </summary>
    public class DataDesc_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] p = parameter.ToString().Split('-');
            JObject obj = JObject.Parse(value.ToString());
            return obj[p[0]][p[1]].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 间隔转换器
    /// </summary>
    public class IntervalConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value / 1000).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value)) value = "60";
            value = Regex.Replace((string)value, @"[^0-9]+", "");
            return int.Parse((string)value) * 1000;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 布尔值-可视转换器  parameter指示Visible对应的布尔值，以Hidden为相反值
    /// </summary>
    public class IsNullConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("This converter is only used for convert to bool.");
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 百分数转换器
    /// </summary>
    public class Percentage_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round(double.Parse(value.ToString()) * 100).ToString() + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string v = value.ToString().Replace("%", "");
            double dv = double.Parse(v);
            return dv * 0.01;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 简介ToolTip文本转换器
    /// </summary>
    public class Tooltip_UserInfo_Desc_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "(单击以编辑)" + value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace("(单击以编辑)", "");
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 宽高值转换器
    /// </summary>
    public class WidthNHeightValue_Plus_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = double.IsNaN(double.Parse(parameter.ToString())) ? 0 : double.Parse(value.ToString());
            double p = double.IsNaN(double.Parse(parameter.ToString())) ? 0 : double.Parse(parameter.ToString());
            return v + p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = value == null ? 0 : (double)value;
            double p = parameter == null ? 0 : (double)parameter;
            return v - p;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// 宽高值乘法转换器
    /// </summary>
    public class WidthNHeightValue_Times_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = double.IsNaN(double.Parse(parameter.ToString())) ? 0 : double.Parse(value.ToString());
            double p = double.IsNaN(double.Parse(parameter.ToString())) ? 0 : double.Parse(parameter.ToString());
            return v * p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = value == null ? 0 : (double)value;
            double p = parameter == null ? 0 : (double)parameter;
            return v / p;
        }

        #endregion Public Methods
    }


    /// <summary>
    /// 桌面窗口位置值转换器
    /// </summary>
    public class DesktopWnd_Pos_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Screen.AllScreens.Length > 1)
            {
                double tmp = 0;
                switch (parameter.ToString().ToLower())
                {
                    case "left":
                        foreach (Screen i in Screen.AllScreens)
                            if (i.WorkingArea.X < tmp) tmp = i.WorkingArea.X;
                        return (double)value - tmp;
                    case "top":
                        foreach (Screen i in Screen.AllScreens)
                            if (i.WorkingArea.Y < tmp) tmp = i.WorkingArea.Y;
                        return (double)value - tmp;
                    default:
                        throw new ArgumentException("非法parameter!");
                }
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("This converter is only used for convert to bool.");
        }

        #endregion Public Methods
    }
}
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace BiliUPDesktopTool
{
    public class Bool2Visbility : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool)value;
            if (v == true) return Visibility.Visible;
            else return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility v = (Visibility)value;
            if (v == Visibility.Visible) return true;
            else return false;
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
    /// 简介ToolTip文本转换器
    /// </summary>
    public class Tooltip_UserInfo_Desc_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "(单击以编辑)" + value.ToString() ;
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
    /// 百分数转换器
    /// </summary>
    public class Percentage_Converter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round(double.Parse(value.ToString()) *100 ).ToString() + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string v = value.ToString().Replace("%","");
            double dv = double.Parse(v);
            return (double)value * 0.01;
        }

        #endregion Public Methods
    }

    public class NoticeVisbilityConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}
using Microsoft.Win32;
using System;

namespace AutoRunSetter
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-setAutoRun":
                        if (args.Length > 1 && args[1] == "-c")
                        {//取消开机启动
                            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                            if (key != null && key.GetValue("BiliUPDesktopTool") != null)
                            {
                                key.DeleteValue("BiliUPDesktopTool");
                            }
                        }
                        else
                        {//设置开机启动
                            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                            if (key != null && key.GetValue("BiliUPDesktopTool") == null)
                            {
                                key.SetValue("BiliUPDesktopTool", "\"" + Environment.CurrentDirectory + "\\BiliUPDesktopTool.exe\"");
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion Private Methods
    }
}
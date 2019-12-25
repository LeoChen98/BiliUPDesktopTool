using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// Code from https://github.com/qmatteoq/DesktopBridgeHelpers/edit/master/DesktopBridge.Helpers/Helpers.cs
    /// </summary>
    internal class DesktopBridgeHelper
    {
        #region Private Fields

        private const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

        private static bool? _isRunningAsUwp;

        #endregion Private Fields

        #region Public Properties

        public static bool IsWindows7OrLower
        {
            get
            {
                int versionMajor = Environment.OSVersion.Version.Major;
                int versionMinor = Environment.OSVersion.Version.Minor;
                double version = versionMajor + (double)versionMinor / 10;
                return version <= 6.1;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static bool IsRunningAsUwp()
        {
            if (_isRunningAsUwp == null)
            {
                if (IsWindows7OrLower)
                {
                    _isRunningAsUwp = false;
                }
                else
                {
                    int length = 0;
                    StringBuilder sb = new StringBuilder(0);
                    int result = GetCurrentPackageFullName(ref length, sb);

                    sb = new StringBuilder(length);
                    result = GetCurrentPackageFullName(ref length, sb);

                    _isRunningAsUwp = result != APPMODEL_ERROR_NO_PACKAGE;
                }
            }

            return _isRunningAsUwp.Value;
        }

        #endregion Public Methods

        #region Private Methods

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);

        #endregion Private Methods
    }
}
using System.Management;

namespace BiliUPDesktopTool
{
    internal class MachineInfoHelper
    {
        #region Public Methods

        public static string GetCPUInfo()
        {
            using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
            {
                using (ManagementObjectCollection moc = cimobject.GetInstances())
                {
                    string strCpuID = null;
                    foreach (ManagementObject mo in moc)
                    {
                        strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                        break;
                    }
                    return strCpuID;
                }
            }
        }

        #endregion Public Methods
    }
}
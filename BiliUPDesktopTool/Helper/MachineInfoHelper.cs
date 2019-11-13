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

        public static string GetMainDriveId()
        {
            using (ManagementClass mc = new ManagementClass("Win32_PhysicalMedia"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    string strID = null;
                    foreach (ManagementObject mo in moc)
                    {
                        strID = mo.Properties["SerialNumber"].Value.ToString();
                        break;
                    }
                    return strID;
                }
            }
        }

        #endregion Public Methods
    }
}
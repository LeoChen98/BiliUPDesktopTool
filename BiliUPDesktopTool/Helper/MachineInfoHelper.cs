using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace BiliUPDesktopTool
{
    internal class MachineInfoHelper
    {
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
            ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = mc.GetInstances();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace BiliUPDesktopTool
{
    class MachineInfoHelper
    {
        public static string GetCPUInfo()
        {
            using(ManagementClass cimobject = new ManagementClass("Win32_Processor"))
            {
                using(ManagementObjectCollection moc = cimobject.GetInstances())
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

    }
}

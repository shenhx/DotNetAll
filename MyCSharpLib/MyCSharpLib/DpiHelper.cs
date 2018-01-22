using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DpiHelper
    {
        public Tuple<float,float> GetDpiByManagementClass()
        {
            using(ManagementClass mc = new ManagementClass("Win32_DesktopMonitor"))
            {
                using(ManagementObjectCollection moc = mc.GetInstances())
                {
                    int PixelsPerXLogicalInch = 0;
                    int PixelsPerYLogicalInch = 0;
                    foreach (ManagementObject each in moc)
                    {
                        PixelsPerXLogicalInch = int.Parse(each.Properties["PixelsPerXLogicalInch"].Value.ToString());
                        PixelsPerYLogicalInch = int.Parse(each.Properties["PixelsPerYLogicalInch"].Value.ToString());
                    }
                    return new Tuple<float, float>(PixelsPerXLogicalInch, PixelsPerYLogicalInch);
                }
            }
        }

        public Tuple<float,float> GetDpiByDrawing()
        {
            using(Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = g.DpiX;
                float dpiY = g.DpiY;
                return new Tuple<float, float>(dpiX,dpiY);
            }
        }
    }
}

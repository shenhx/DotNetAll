using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZabraPrinting
{
    public class GraphDeviceInfo
    {
        public int ColorDepth { get; set; }

        public int DpiX { get; set; }

        public int DpiY { get; set; }

        public float PageWidth { get; set; }

        public float PageHeight { get; set; }

        public float MarginTop { get; set; }
    }
}

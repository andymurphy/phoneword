using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Phoneword
{
    public static class SharedResources
    {
        // Do these two work the same. Perhaps the 2nd one cannot be changed, only accessed - no setter.
        public static Color ButtonBkColour = Color.FromArgb(0xff, 0xa5, 0); // Hexadecimal notation -> (255, 165, 0) in decimal. Red/green
        public static Color ButtonTextColour
        {
            get { return Color.White;  }
        }
    }
}

using RGBDelight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.ViewModels
{
    class MainVM
    {
        private LED _RGB;
        public MainVM()
        {
            _RGB = new LED(0, 0, 0);
        }

        public void ChangeLedColour(byte r, byte g, byte b)
        {
            _RGB.RGB = new Tuple<byte, byte, byte>(r, g, b);
        }
    }
}

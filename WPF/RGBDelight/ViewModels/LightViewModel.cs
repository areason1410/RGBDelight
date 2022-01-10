using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBDelight.Models;

namespace RGBDelight.ViewModels
{
    class LightViewModel
    {
        #region Properties
        
        #endregion
        // methods ON, OFF, ChangeColour
        #region Methods

        public void ChangeColour(LED light, byte r, byte g, byte b)
        {
            light.RGB = new Tuple<byte, byte, byte>(r, g, b);
        }

        public void On(LED light)
        {

        }

        public void Off(LED light)
        {

        }
        #endregion


    }
}

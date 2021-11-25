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
        private HouseViewModel _houseVM;
        private RoomViewModel _roomVM;
        private LightViewModel _lightVM;
        public MainVM()
        {
            _houseVM = new HouseViewModel();
            _roomVM = new RoomViewModel();
            _lightVM = new LightViewModel();
        }

        public HouseViewModel House()
        {
            return _houseVM;
        }

        public RoomViewModel Room()
        {
            return _roomVM;
        }

        public LightViewModel Light()
        {
            return _lightVM;
        }
    }
}

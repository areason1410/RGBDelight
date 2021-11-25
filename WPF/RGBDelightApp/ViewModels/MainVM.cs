using RGBDelight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.ViewModels
{
    public class MainVM
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

        public HouseViewModel HouseVM()
        {
            return _houseVM;
        }

        public RoomViewModel RoomVM()
        {
            return _roomVM;
        }

        public LightViewModel LightVM()
        {
            return _lightVM;
        }
    }
}

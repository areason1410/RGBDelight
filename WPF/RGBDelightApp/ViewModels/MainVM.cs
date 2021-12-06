using RGBDelight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.ViewModels
{
    public class MainVM
    {
        private House _house;
        public MainVM()
        {
            _house = new House();
        }

        public ObservableCollection<Room> Rooms()
        {
            return _house.Rooms;
        }

        public Room GetRoom(Room room)
        {
            if(_house.Rooms.Contains(room))
            {
                try
                {
                    return room;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public bool AddRoom(Room room)
        {
            try
            {
                _house.Rooms.Add(room);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool AddLight(Room room, LED light)
        {
            try
            {
                room.Lights.Add(light);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool RemoveRoom(Room room)
        {
            if(_house.Rooms.Contains(room))
            {
                try
                {
                    if(_house.Rooms.Remove(room))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveLight(Room room, LED light)
        {
            if (room.Lights.Contains(light))
            {
                try
                {
                    if (room.Lights.Remove(light))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            else
            {
                return false;
            }
        }
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
        // private HouseViewModel _houseVM;
        // private RoomViewModel _roomVM;
        // private LightViewModel _lightVM;
        // public MainVM()
        // {
        //     _houseVM = new HouseViewModel();
        //     _roomVM = new RoomViewModel();
        //     _lightVM = new LightViewModel();
        // }

        // public HouseViewModel HouseVM()
        // {
        //     return _houseVM;
        // }

        // public RoomViewModel RoomVM()
        // {
        //     return _roomVM;
        // }

        // public LightViewModel LightVM()
        // {
        //     return _lightVM;
        // }

        // public void AddRoom(Room room)
        // {
        //     try
        //     {
        //         _houseVM.AddRoom(room);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public void 
    }
}

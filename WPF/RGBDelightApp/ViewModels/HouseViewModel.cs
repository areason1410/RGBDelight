using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBDelight.Models;

namespace RGBDelight.ViewModels
{
    public class HouseViewModel
    {

        #region Properties
        private House _house = new House();
        private RoomViewModel _roomVM = new RoomViewModel();
        #endregion
        #region Methods

        public ObservableCollection<Room> Rooms()
        {
            return _house.Rooms;
        }
        
        public Room Room(Room room)
        {
            int index = Rooms().IndexOf(room);
            if(index == -1) return Rooms()[0];
            return Rooms()[index];
        }
        public void AddRoom(Room Room)
        {
            try
            {
                _house.Rooms.Add(Room);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveRoom(Room Room)
        {
            if(_house.Rooms.Contains(Room))
            {
                try
                {
                    _house.Rooms.Remove(Room);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        public void AddLight(Room room, LED light)
        {
            try
            {
                 int index = Rooms().IndexOf(room);
                 Rooms()[index].Lights.Add(light);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        // public void AddLight(Room room, LED light)
        // {
        //     try
        //     {
        //         _roomVM.AddLight(room, light);
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // }
        #endregion
    }
}

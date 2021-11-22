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
        private House house = new House();
        #endregion
        #region Methods

        public ObservableCollection<Room> Rooms()
        {
            return house.Rooms;
        }
        public void AddRoom(Room Room)
        {
            try
            {
                house.Rooms.Add(Room);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveRoom(Room Room)
        {
            if(house.Rooms.Contains(Room))
            {
                try
                {
                    house.Rooms.Remove(Room);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion
    }
}

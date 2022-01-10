using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBDelight.Models;

namespace RGBDelight.ViewModels
{
    class HouseViewModel
    {
        public string testString = "This is a text";
        #region Methods
        public void AddRoom(House House, Room Room)
        {
            try
            {
                House.Rooms.Add(Room);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveRoom(House House, Room Room)
        {
            if(House.Rooms.Contains(Room))
            {
                try
                {
                    House.Rooms.Remove(Room);
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

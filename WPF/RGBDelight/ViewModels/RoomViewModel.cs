using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBDelight.Models;

namespace RGBDelight.ViewModels
{
    class RoomViewModel
    {
        #region Methods

        public void AddLight(Room Room, string LightName)
        {
            try
            {
                Room.Lights.Add(new LED(0, 0, 0));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeRoomName(Room Room, string name)
        {
            try
            {
                Room.RoomName = name;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveLight(Room Room, LED Light)
        {
            if (Room.Lights.Contains(Light))
            {
                try
                {
                    Room.Lights.Remove(Light);
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

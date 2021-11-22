using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBDelight.Models;

namespace RGBDelight.ViewModels
{
    public class RoomViewModel
    {
        #region Methods

        public void AddLight(Room Room, LED Light)
        {
            try
            {
                Room.Lights.Add(Light);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ObservableCollection<LED> Lights(Room room)
        {
            return room.Lights;
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

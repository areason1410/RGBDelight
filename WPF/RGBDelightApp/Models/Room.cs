using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.Models
{
    public class Room : INPC
    {
        #region Properties
        private ObservableCollection<LED> _lights;

        public ObservableCollection<LED> Lights
        {
            get { return _lights; }
            set 
            {
                _lights = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        private string _roomName;
        public string RoomName
        {
            get { return _roomName; }
            set 
            {
                _roomName = value;
                OnPropertyChanged(nameof(Room));

            }
        }

        private Guid _id;
        public Guid ID
        {
            get { return _id; }
            set 
            { 
                _id = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        #endregion
        #region Constructors
        public Room(string RoomName)
        {
            _lights = new ObservableCollection<LED>();
            _roomName = RoomName;
            _id = new Guid();
        }
        #endregion
    }
}

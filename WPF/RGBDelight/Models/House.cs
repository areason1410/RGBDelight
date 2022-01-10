using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RGBDelight.Models
{
    class House : INPC
    {
        private ObservableCollection<Room> _rooms;

        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set 
            { 
                _rooms = value;
                OnPropertyChanged(nameof(House));

            }
        }


        public House(ObservableCollection<Room> Rooms)
        {
            _rooms = Rooms;
        }

    }
}

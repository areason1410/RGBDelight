﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RGBDelight.Models
{
    public class House : INPC
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

        private string _roomName;

        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }

        public House()
        {
            _rooms = new ObservableCollection<Room>();
        }

    }
}

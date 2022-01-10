using System;
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

        private ObservableCollection<Scene> _scenes;

        public ObservableCollection<Scene> Scenes
        {
            get { return _scenes; }
            set
            {
                _scenes = value;
                OnPropertyChanged(nameof(House));

            }
        }
        public House()
        {
            _rooms = new ObservableCollection<Room>();
            _scenes = new ObservableCollection<Scene>();
        }

    }
}

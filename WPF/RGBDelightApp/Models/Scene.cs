using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.Models
{
    public class Scene : INPC
    {
        #region Properties
        private ObservableCollection<LED> _lights;

        public ObservableCollection<LED> Lights
        {
            get { return _lights; }
            set 
            {
                _lights = value;
                OnPropertyChanged(nameof(Scene));
            }
        }

        private string _sceneName;
        public string SceneName
        {
            get { return _sceneName; }
            set 
            {
                _sceneName = value;
                OnPropertyChanged(nameof(Scene));

            }
        }

        private Guid _id;
        public Guid ID
        {
            get { return _id; }
            set 
            { 
                _id = value;
                OnPropertyChanged(nameof(Scene));
            }
        }

        #endregion
        #region Constructors
        public Scene(string sceneName)
        {
            _lights = new ObservableCollection<LED>();
            _sceneName = sceneName;
            _id = new Guid();
        }
        #endregion
    }
}

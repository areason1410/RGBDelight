using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.Models
{
    public class Scene : Room
    {
        #region Properties
       
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

        #endregion
        #region Constructors
        public Scene(string sceneName) : base(sceneName)
        {
            _lights = new ObservableCollection<LED>();
            _sceneName = sceneName;
            _id = new Guid();
        }
        #endregion
    }
}

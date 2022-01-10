using RGBDelight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBDelight.ViewModels
{
    public static class MainVM
    {
        private static House _house;
        static MainVM()
        {
            _house = new House();
        }

        public static ObservableCollection<Room> Rooms()
        {
            return _house.Rooms;
        }

        public static ObservableCollection<Scene> Scenes()
        {
            return _house.Scenes;
        }

        public static Room GetRoom(Room room)
        {
            if(_house.Rooms.Contains(room))
            {
                try
                {
                    return room;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public static Scene GetScenes(Scene scene)
        {
            if (_house.Scenes.Contains(scene))
            {
                try
                {
                    return scene;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool AddRoom(Room room)
        {
            try
            {
                _house.Rooms.Add(room);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static bool AddScene(Scene scene)
        {
            try
            {
                _house.Scenes.Add(scene);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static bool AddLight(object room, LED light)
        {
            try
            {
                (room as Room).Lights.Add(light);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static bool RemoveRoom(Room room)
        {
            if(_house.Rooms.Contains(room))
            {
                try
                {
                    if(_house.Rooms.Remove(room))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveScene(Scene scene)
        {
            if (_house.Scenes.Contains(scene))
            {
                try
                {
                    if (_house.Scenes.Remove(scene))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveLight(Room room, LED light)
        {
            if (room.Lights.Contains(light))
            {
                try
                {
                    if (room.Lights.Remove(light))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ApplyScene(Scene scene, Room room)
        {
            if(_house.Scenes.Contains(scene) && _house.Rooms.Contains(room))
            {
                try
                {
                    for (int i = 0; i < room.Lights.Count; i++)
                    {
                        try
                        {
                            room.Lights[i].RGB = scene.Lights[i].RGB;
                            room.Lights[i].Brightness = scene.Lights[i].Brightness;

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            return true;
        }

        public static void ChangeColour(LED light, byte r, byte g, byte b)
        {
            light.RGB = new Tuple<byte, byte, byte>(r, g, b);
        }

        public static void On(LED light)
        {

        }

        public static void Off(LED light)
        {

        }
        // private HouseViewModel _houseVM;
        // private RoomViewModel _roomVM;
        // private LightViewModel _lightVM;
        // public MainVM()
        // {
        //     _houseVM = new HouseViewModel();
        //     _roomVM = new RoomViewModel();
        //     _lightVM = new LightViewModel();
        // }

        // public HouseViewModel HouseVM()
        // {
        //     return _houseVM;
        // }

        // public RoomViewModel RoomVM()
        // {
        //     return _roomVM;
        // }

        // public LightViewModel LightVM()
        // {
        //     return _lightVM;
        // }

        // public void AddRoom(Room room)
        // {
        //     try
        //     {
        //         _houseVM.AddRoom(room);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public void 
    }
}

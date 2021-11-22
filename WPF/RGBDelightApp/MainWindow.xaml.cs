using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RGBDelight.ViewModels;
using RGBDelight.Models;
using System.Diagnostics;

namespace RGBDelightApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> myCars { get; set; }
        public HouseViewModel house;
        public RoomViewModel roomVM;
        public MainWindow()
        {
            InitializeComponent();
            myCars = new List<string>();

            house = new HouseViewModel();
            roomVM = new RoomViewModel();
            Room room = new Room("ROOOM");
            LED light = new LED(0, 0, 0);
            roomVM.AddLight(room, light);
            house.AddRoom(room);

            gridRooms.ItemsSource = house.Rooms();
            gridRooms.DisplayMemberPath = "RoomName";
            gridLights.ItemsSource = roomVM.Lights(room);

            DataContext = house.Rooms();


            //myCars.Add("broom");
            //myCars.Add("broom");
            //myCars.Add("broom");
            //myCars.Add("broom");
            //DataContext = this;
            
            
            //gridRooms.DataContext
                
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = new Room($"Room {house.Rooms().Count + 1}");
            house.AddRoom(room);
        }
    }
}

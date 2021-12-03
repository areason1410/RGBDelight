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
        public MainVM mainVM;
        public HouseViewModel houseVM;


        private enum GridDefinitions
        {
            Row,
            Column
        }

        #region Evidence
        private void AddColumnDefinition(int width)
        {
            ColumnDefinition column = new ColumnDefinition();
            GridLength size = new GridLength(width, GridUnitType.Star);
            column.Width = size;
            View.ColumnDefinitions.Add(column);
        }

        private void AddRowDefinition(int Height)
        {
            RowDefinition row = new RowDefinition();
            GridLength size = new GridLength(Height, GridUnitType.Star);
            row.Height = size;
            View.RowDefinitions.Add(row);
        }


        #endregion

        /// <summary>
        /// Create a Row or Column definition and automatically add it to the grid
        /// </summary>
        /// <param name="definition">GridDefinitions Enum type so Row or Column</param>
        /// <param name="val">Width or Height of the Column or Row (star values)</param>
        /// <param name="count">How many copies of the definition to be made</param>
        private void AddGridDefinition(GridDefinitions definition, int val, int count = 1)
        {
            GridLength size = new GridLength(val, GridUnitType.Star);

            switch (definition)
            {
                case GridDefinitions.Row:
                    for(int i = 0; i < count; i++)
                    {
                        RowDefinition row = new RowDefinition();
                        row.Height = size;
                        View.RowDefinitions.Add(row);
                    }
                    break;
                case GridDefinitions.Column:
                    for(int i = 0; i < count; i++)
                    {
                        ColumnDefinition column = new ColumnDefinition();
                        column.Width = size;
                        View.ColumnDefinitions.Add(column);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates a DataTemplate finish this lol
        /// </summary>
        /// <returns></returns>
        private DataTemplate RoomsListViewDataTemplate()
        {
            DataTemplate listViewDataTemplate = new DataTemplate();
            listViewDataTemplate.DataType = typeof(Room);

            FrameworkElementFactory listViewElementFactory = new FrameworkElementFactory(typeof(StackPanel));
            listViewElementFactory.Name = "listViewElementFactory";
            listViewElementFactory.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);

            FrameworkElementFactory roomElementFactory = new FrameworkElementFactory(typeof(TextBlock));
            roomElementFactory.SetBinding(TextBlock.TextProperty, new Binding("RoomName"));
            roomElementFactory.SetValue(TextBlock.ToolTipProperty, "asdf");
            listViewElementFactory.AppendChild(roomElementFactory);

            listViewDataTemplate.VisualTree = listViewElementFactory;

            return listViewDataTemplate;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTemplate LightsListBoxDataTemplate()
        {
            DataTemplate gridViewDataTemplate = new DataTemplate();
            gridViewDataTemplate.DataType = typeof(Room);

            FrameworkElementFactory gridViewElementFactory = new FrameworkElementFactory(typeof(ListBox));
            gridViewElementFactory.Name = "listViewElementFactory";
            //gridViewElementFactory.SetValue(GridView.);

            FrameworkElementFactory lightElementFactory = new FrameworkElementFactory(typeof(TextBlock));
            lightElementFactory.SetBinding(TextBlock.TextProperty, new Binding("ID"));
            lightElementFactory.SetValue(TextBlock.ToolTipProperty, "asdf");
            gridViewElementFactory.AppendChild(lightElementFactory);

            gridViewDataTemplate.VisualTree = gridViewElementFactory;

            return gridViewDataTemplate;

        }


        public MainWindow()
        {
            InitializeComponent();
            //mainVM = new MainVM();

            houseVM = new HouseViewModel();
            //mainVM.RoomVM.roomList.Add
            Room room1 = new Room("Room 1");
            Room room2 = new Room("Room 2");

            houseVM.AddRoom(room1);
            houseVM.AddRoom(room2);
            //houseVM.Rooms().Add(room1);
            //houseVM.AddRoom(room1);
            //Room room2 = new Room("Room 2");
            //houseVM.Rooms().Add(room2);
            LED light1 = new LED(0, 0, 0);
            LED light2 = new LED(0, 0, 0);

            houseVM.AddLight(room1, light1);
            houseVM.AddLight(room1, light2);
            //houseVM.rooms.ElementAt(0).Lights.Add(light1);
            //mainVM.RoomVM().AddLight(room1, light1);

            AddGridDefinition(GridDefinitions.Column, 1, 2);
            AddGridDefinition(GridDefinitions.Row, 1);
            AddGridDefinition(GridDefinitions.Row, 5);
            AddGridDefinition(GridDefinitions.Row, 1);


            TextBlock roomText = new TextBlock();
            //roomText.Name = "RoomText";
            roomText.Text = "Room";
            roomText.FontSize = 24;
            roomText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(roomText, 1);
            Grid.SetRow(roomText, 1);
          
            Button addRoomButton = new Button();
            addRoomButton.Content = "+ Add Room";
            addRoomButton.HorizontalAlignment = HorizontalAlignment.Right;

            DockPanel topDock = new DockPanel();
            Grid.SetColumnSpan(topDock, 2);
            topDock.Children.Add(roomText);
            topDock.Children.Add(addRoomButton);

            
            ListView roomList = new ListView();
            roomList.ItemTemplate = RoomsListViewDataTemplate();
            roomList.ItemsSource = houseVM.Rooms();
            Grid.SetRow(roomList, 1);

            ListBox lightList = new ListBox();
            lightList.ItemTemplate = LightsListBoxDataTemplate();
            lightList.ItemsSource = houseVM.Room(roomList.SelectedItem as Room).Lights;
            Debug.WriteLine($"{roomList.SelectedItem} yea");
            Grid.SetColumn(lightList, 1);
            Grid.SetRow(lightList, 1);


            // View.Children.Add(test);
            View.Children.Add(topDock);
            View.Children.Add(roomList);
            View.Children.Add(lightList);
            View.ShowGridLines = true;


            //View.ColumnDefinitions.

            DataContext = this;
            
        }

       
    }
}

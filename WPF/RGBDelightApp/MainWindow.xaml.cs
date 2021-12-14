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
using RGBDelight.Views;
using System.Diagnostics;
namespace RGBDelightApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM mainVM;
        private utils utils;


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
        private ItemsPanelTemplate RoomListViewItemsPanelTemplate()
        {
            ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate();
            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(WrapPanel));
            frameworkElementFactory.SetValue(WrapPanel.MaxWidthProperty, (double)200);


            itemsPanelTemplate.VisualTree = frameworkElementFactory;
            return itemsPanelTemplate;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTemplate LightsListBoxDataTemplate()
        {
            DataTemplate gridViewDataTemplate = new DataTemplate();
            gridViewDataTemplate.DataType = typeof(LED);

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

        ListBox lightList = new ListBox();


        private void roomSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = e.Source as ListView;
            //list.SelectedItem
            //lightList.ItemsSource = (list.SelectedItem as Room).Lights;
            LightsView lightsView = new LightsView(mainVM, list.SelectedItem as Room);
            lightsView.ShowDialog();
            
        }

       

        private void addRoomButtonClicked(object sender, RoutedEventArgs e)
        {
            AddRoomPopup popup = new AddRoomPopup();
            popup.ShowDialog();
            mainVM.AddRoom(new Room(AddRoomPopup.roomName));
        }
        private void addLightButtonClicked(object sender, RoutedEventArgs e)
        {

        }


        public MainWindow()
        {
            InitializeComponent();
            //mainVM = new MainVM();

            mainVM = new MainVM();
            utils = new utils();
            //mainVM.RoomVM.roomList.Add
            Room room1 = new Room("Room 1");
            Room room2 = new Room("Room 2");

            mainVM.AddRoom(room1);
            mainVM.AddRoom(room2);

            LED light1 = new LED(23, 45, 122);
            LED light2 = new LED(11, 1, 1);


            mainVM.AddLight(room1, light1);
            mainVM.AddLight(room1, light2);

            utils.AddGridDefinition(View, GridDefinitions.Column, 1);
            utils.AddGridDefinition(View, GridDefinitions.Row, 1);
            utils.AddGridDefinition(View, GridDefinitions.Row, 5);
            utils.AddGridDefinition(View, GridDefinitions.Row, 1);


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
            addRoomButton.Click += addRoomButtonClicked;

            DockPanel topDock = new DockPanel();
            Grid.SetColumnSpan(topDock, 1);
            topDock.Children.Add(roomText);
            topDock.Children.Add(addRoomButton);

            
            ListView roomList = new ListView();
            roomList.ItemTemplate = RoomsListViewDataTemplate();
            roomList.ItemsSource = mainVM.Rooms();
            roomList.ItemsPanel = RoomListViewItemsPanelTemplate();
            Grid.SetRow(roomList, 1);
            roomList.SelectionChanged += new SelectionChangedEventHandler(roomSelectionChanged);


            //lightList.ItemTemplate = LightsListBoxDataTemplate();
            //if (roomList.SelectedItem != null)
            //{
            //    lightList.ItemsSource = (roomList.SelectedItem as Room).Lights;
            //}

            //Grid.SetColumn(lightList, 1);
            //Grid.SetRow(lightList, 1);


            // View.Children.Add(test);
            View.Children.Add(topDock);
            View.Children.Add(roomList);
            //View.Children.Add(lightList);
            View.ShowGridLines = true;


            //View.ColumnDefinitions.

            DataContext = this;
            
        }
    }
}

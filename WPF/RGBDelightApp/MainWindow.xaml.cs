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
using Newtonsoft.Json;


namespace RGBDelight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM mainVM;
        private Grid View;
        private bool loggedIn = true; //set to false in production
        private readonly string currentUsername;


        /// <summary>
        /// Creates a DataTemplate finish this lol
        /// https://stackoverflow.com/questions/17828417/centering-text-vertically-and-horizontally-in-textblock-and-passwordbox-in-windo
        /// </summary>
        /// <returns></returns>
        private DataTemplate RoomsListViewDataTemplate()
        {
            DataTemplate listViewDataTemplate = new DataTemplate();
            listViewDataTemplate.DataType = typeof(Room);

            FrameworkElementFactory roomElementFactory = new FrameworkElementFactory(typeof(Label));
            roomElementFactory.SetBinding(Label.ContentProperty, new Binding("RoomName"));
            roomElementFactory.SetValue(TextBlock.ToolTipProperty, "asdf");
            roomElementFactory.SetValue(TextBlock.MinWidthProperty, Constants.screenWidth / 7.5);
            roomElementFactory.SetValue(TextBlock.MinHeightProperty, Constants.screenHeight / 10);

           // roomElementFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
            //roomElementFactory.SetValue(VerticalContentAlignment, VerticalContentAlignment.Center);

            roomElementFactory.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
            roomElementFactory.SetValue(HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            roomElementFactory.SetValue(Label.BackgroundProperty, Brushes.Gray);
            roomElementFactory.SetValue(Label.ForegroundProperty, Brushes.White);

            listViewDataTemplate.VisualTree = roomElementFactory;

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
            frameworkElementFactory.SetValue(WrapPanel.MaxWidthProperty, Constants.screenWidth);
            //frameworkElementFactory.SetValue(TextBlock.MarginProperty, new Thickness(50));

            itemsPanelTemplate.VisualTree = frameworkElementFactory;
            return itemsPanelTemplate;

        }
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.style.setters?view=windowsdesktop-6.0#System_Windows_Style_Setters
        /// </summary>
        /// <returns></returns>
        private Style RoomListItemContainerStyle()
        {
            Style style = new Style();
            style.TargetType = typeof(ListViewItem);
            style.Setters.Add(new Setter(ListView.MarginProperty, new Thickness((Constants.screenWidth / 75))));

            return style;
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
            if (list.SelectedIndex != -1)
            {
                LightsView lightsView = new LightsView(mainVM, list.SelectedItem as Room);
                lightsView.ShowDialog();
            }
            list.SelectedIndex = -1;
            
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

        private void drawRoomsPage()
        {
            TextBlock pageText = new TextBlock();
            pageText.Text = "Rooms";
            pageText.Margin = Constants.TitleTextMargin;
            pageText.FontSize = 24;
            pageText.VerticalAlignment = VerticalAlignment.Center;
            pageText.Foreground = Utils.GetBrush(Colours.White);
            Grid.SetColumn(pageText, 1);
            Grid.SetRow(pageText, 1);

            Button addRoomButton = new Button();
            addRoomButton.Content = "+ Add Room";
            addRoomButton.HorizontalAlignment = HorizontalAlignment.Right;
            addRoomButton.Click += addRoomButtonClicked;

            DockPanel topDock = new DockPanel();
            Grid.SetColumnSpan(topDock, 1);
            topDock.Children.Add(pageText);
            topDock.Children.Add(addRoomButton);
            Grid.SetColumnSpan(topDock, 3);

            ListView roomList = new ListView();
            roomList.ItemTemplate = RoomsListViewDataTemplate();
            roomList.ItemsSource = mainVM.Rooms();
            roomList.ItemsPanel = RoomListViewItemsPanelTemplate();
            roomList.ItemContainerStyle = RoomListItemContainerStyle();
            roomList.Background = Utils.GetBrush(Colours.BackgroundDefault);
            Grid.SetRow(roomList, 1);
            roomList.SelectionChanged += new SelectionChangedEventHandler(roomSelectionChanged);
            Grid.SetColumnSpan(roomList, 3);

            View.Children.Add(topDock);
            View.Children.Add(roomList);
        }



        private void drawScenesPage()
        {
            TextBlock pageText = new TextBlock();
            pageText.Text = "Scenes";
            pageText.Margin = Constants.TitleTextMargin;
            pageText.FontSize = 24;
            pageText.VerticalAlignment = VerticalAlignment.Center;
            pageText.Foreground = Utils.GetBrush(Colours.White);
            Grid.SetColumn(pageText, 1);
            Grid.SetRow(pageText, 1);

            Button addRoomButton = new Button();
            addRoomButton.Content = "+ Add Room";
            addRoomButton.HorizontalAlignment = HorizontalAlignment.Right;
            addRoomButton.Click += addRoomButtonClicked;

            DockPanel topDock = new DockPanel();
            Grid.SetColumnSpan(topDock, 1);
            topDock.Children.Add(pageText);
            topDock.Children.Add(addRoomButton);
            Grid.SetColumnSpan(topDock, 3);

            ListView roomList = new ListView();
            roomList.ItemTemplate = RoomsListViewDataTemplate();
            roomList.ItemsSource = mainVM.Rooms();
            roomList.ItemsPanel = RoomListViewItemsPanelTemplate();
            roomList.ItemContainerStyle = RoomListItemContainerStyle();
            roomList.Background = Utils.GetBrush(Colours.BackgroundDefault);
            Grid.SetRow(roomList, 1);
            roomList.SelectionChanged += new SelectionChangedEventHandler(roomSelectionChanged);
            Grid.SetColumnSpan(roomList, 3);

            View.Children.Add(topDock);
            View.Children.Add(roomList);
        }


        private void roomsPageButtonClicked(object sender, RoutedEventArgs e)
        {
            View.Children.Clear();
            drawRoomsPage();
            drawBottomBar();
        }
        private void scenesPageButtonClicked(object sender, RoutedEventArgs e)
        {
            View.Children.Clear();
            drawScenesPage();
            drawBottomBar();
        }

        private void drawBottomBar()
        {

            //string[] content = new string[3] { "Scenes", "Main", "Settings" };

            //for(int i = 0; i < 3; i++)
            //{
            //    Button button = new Button();
            //    Grid.SetColumn(button, i);
            //    Grid.SetRow(button, 3);
            //    button.Content = content[i];


            //    View.Children.Add(button);
            //}

            Button scenesPage = new Button();
            Grid.SetRow(scenesPage, 3);
            scenesPage.Content = "Scenes";
            scenesPage.Click += scenesPageButtonClicked;
            //scenesPage.
            //Grid.SetRow(scenesPage, 3);

            Button roomsPage = new Button();
            Grid.SetRow(roomsPage, 3);
            Grid.SetColumn(roomsPage, 1);
            roomsPage.Content = "Main";
            roomsPage.Click += roomsPageButtonClicked;


            Button settingsPage = new Button();
            Grid.SetRow(settingsPage, 3);
            Grid.SetColumn(settingsPage, 2);
            settingsPage.Content = "Settings";


            View.Children.Add(scenesPage);
            View.Children.Add(roomsPage);
            View.Children.Add(settingsPage);
        }

        public MainWindow()
        {
            if(loggedIn == false)
            {
                LoginView login = new LoginView();
                login.ShowDialog();
                currentUsername = LoginView.currentUsername;
                loggedIn = true;
            }

            InitializeComponent();

            mainVM = new MainVM();
            View = new Grid();

            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.Background = Utils.GetBrush(Colours.BackgroundDark);

            Room room1 = new Room("Room 1");
            Room room2 = new Room("Room 2");

            mainVM.AddRoom(room1);
            mainVM.AddRoom(room2);

            LED light1 = new LED(23, 45, 122);
            LED light2 = new LED(11, 1, 1);

            mainVM.AddLight(room1, light1);
            mainVM.AddLight(room1, light2);

            Utils.AddGridDefinition(View, GridDefinitions.Column, 1, 3);
            Utils.AddGridDefinition(View, GridDefinitions.Row, 1);
            Utils.AddGridDefinition(View, GridDefinitions.Row, 5);
            Utils.AddGridDefinition(View, GridDefinitions.Row, 1);

            drawRoomsPage();

            drawBottomBar();;

            View.ShowGridLines = true;

            this.Content = View;
            DataContext = this;
            
        }
    }
}

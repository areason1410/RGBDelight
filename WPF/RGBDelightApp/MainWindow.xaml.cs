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
        private PageType currentPage;

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
            style.Setters.Add(new Setter(ListView.MarginProperty, new Thickness(Constants.screenWidth / 75)));

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
            currentPage = PageType.Rooms;
            TextBlock pageText = new TextBlock();
            pageText.Text = "Rooms";
            pageText.Margin = Constants.TitleTextMargin;
            pageText.FontSize = 24;
            pageText.FontWeight = FontWeights.Bold;
            pageText.VerticalAlignment = VerticalAlignment.Center;
            pageText.Foreground = Utils.GetBrush(Colours.White);
            Grid.SetColumn(pageText, 1);
            Grid.SetRow(pageText, 1);


            Border addRoomBorder = new Border();

            Button addRoomButton = new Button();
            addRoomButton.Content = "+ Add Room";
            //addRoomButton.MinWidth = System.Windows.SystemParameters.WorkArea.Width / 10;
            //addRoomButton.HorizontalAlignment = HorizontalAlignment.Right;
            addRoomButton.Foreground = Utils.GetBrush(Colours.White);
            addRoomButton.Background = Utils.GetBrush(Colours.Blue);
            //addRoomButton.Margin = Constants.AddButtonMargin;
            addRoomButton.BorderBrush = Utils.GetBrush(Colours.Blue);
            addRoomButton.Click += addRoomButtonClicked;

            addRoomBorder.Child = addRoomButton;
            addRoomBorder.CornerRadius = new CornerRadius(5);
            addRoomBorder.BorderBrush = Utils.GetBrush(Colours.Blue);
            addRoomBorder.BorderThickness = new Thickness(3);
            addRoomBorder.HorizontalAlignment = HorizontalAlignment.Right;
            addRoomBorder.MinWidth = System.Windows.SystemParameters.WorkArea.Width / 10;
            addRoomBorder.Margin = Constants.AddButtonMargin;




            DockPanel topDock = new DockPanel();
            Grid.SetColumnSpan(topDock, 1);
            topDock.Children.Add(pageText);
            topDock.Children.Add(addRoomBorder);
            Grid.SetColumnSpan(topDock, 3);

            ListView roomList = new ListView();
            roomList.ItemTemplate = RoomsListViewDataTemplate();
            roomList.ItemsSource = mainVM.Rooms();
            roomList.ItemsPanel = RoomListViewItemsPanelTemplate();
            roomList.ItemContainerStyle = RoomListItemContainerStyle();
            roomList.Background = Utils.GetBrush(Colours.BackgroundDefault);
            roomList.BorderBrush = Utils.GetBrush(Colours.BackgroundDefault);
            roomList.SelectionChanged += new SelectionChangedEventHandler(roomSelectionChanged);
            Grid.SetRow(roomList, 1);
            Grid.SetColumnSpan(roomList, 3);

            View.Children.Add(topDock);
            View.Children.Add(roomList);
        }



        private void drawScenesPage()
        {
            currentPage = PageType.Scenes;
            TextBlock pageText = new TextBlock();
            pageText.Text = "Scenes";
            pageText.Margin = Constants.TitleTextMargin;
            pageText.FontSize = 24;
            pageText.FontWeight = FontWeights.Bold;
            pageText.VerticalAlignment = VerticalAlignment.Center;
            pageText.Foreground = Utils.GetBrush(Colours.White);
            Grid.SetColumn(pageText, 1);
            Grid.SetRow(pageText, 1);


            Border addSceneBorder = new Border();

            Button addSceneButton = new Button();
            addSceneButton.Content = "+ Add Scene";
            //aSceneomButton.MinWidth = System.Windows.SystemParameters.WorkArea.Width / 10;
            //aSceneomButton.HorizontalAlignment = HorizontalAlignment.Right;
            addSceneButton.Foreground = Utils.GetBrush(Colours.White);
            addSceneButton.Background = Utils.GetBrush(Colours.Blue);
            //aSceneomButton.Margin = Constants.AddButtonMargin;
            addSceneButton.BorderBrush = Utils.GetBrush(Colours.Blue);
            //addSceneButton.Click += addSceneButtonClicked;

            addSceneBorder.Child = addSceneButton;
            addSceneBorder.CornerRadius = new CornerRadius(5);
            addSceneBorder.BorderBrush = Utils.GetBrush(Colours.Blue);
            addSceneBorder.BorderThickness = new Thickness(3);
            addSceneBorder.HorizontalAlignment = HorizontalAlignment.Right;
            addSceneBorder.MinWidth = System.Windows.SystemParameters.WorkArea.Width / 10;
            addSceneBorder.Margin = Constants.AddButtonMargin;

            DockPanel topDock = new DockPanel();
            Grid.SetColumnSpan(topDock, 1);
            topDock.Children.Add(pageText);
            topDock.Children.Add(addSceneBorder);
            Grid.SetColumnSpan(topDock, 3);

            ListView scenesList = new ListView();
            scenesList.ItemTemplate = RoomsListViewDataTemplate();
            scenesList.ItemsSource = mainVM.Scenes();
            scenesList.ItemsPanel = RoomListViewItemsPanelTemplate();
            scenesList.ItemContainerStyle = RoomListItemContainerStyle();
            scenesList.Background = Utils.GetBrush(Colours.BackgroundDefault);
            scenesList.BorderBrush = Utils.GetBrush(Colours.BackgroundDefault);
            Grid.SetRow(scenesList, 1);
            scenesList.SelectionChanged += new SelectionChangedEventHandler(roomSelectionChanged);
            Grid.SetColumnSpan(scenesList, 3);

            View.Children.Add(topDock);
            View.Children.Add(scenesList);
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

            StackPanel scenesPageStackPanel = new StackPanel();
            scenesPageStackPanel.Orientation = Orientation.Vertical;
            scenesPageStackPanel.Margin = new Thickness(10);

            Image scenePng = Utils.loadImage("C:\\Users\\Aaron\\Source\\Repos\\RGBDelight\\WPF\\RGBDelightApp\\Images\\donut_large_white_24dp.png", UriKind.Absolute);
            scenePng.Width = Constants.iconWidth;
            scenePng.Height = Constants.iconHeight;
            scenesPageStackPanel.Children.Add(scenePng);

            Label scenesPageLabel = new Label();
            scenesPageLabel.Content = "Scenes";
            scenesPageLabel.FontSize = Constants.bottomBarFontSize;
            scenesPageLabel.Foreground = Utils.GetBrush(Colours.White);
            scenesPageStackPanel.Children.Add(scenesPageLabel);


            Button scenesPage = new Button();
            Grid.SetRow(scenesPage, 3);

            scenesPage.Background = Utils.GetBrush(Colours.BackgroundDark);
            scenesPage.BorderBrush = Utils.GetBrush(Colours.BackgroundDark);
            scenesPage.Foreground = Utils.GetBrush(Colours.White);

            scenesPage.Content = scenesPageStackPanel;
            scenesPage.Click += scenesPageButtonClicked;


            Button roomsPage = new Button();

            StackPanel roomsPageStackPanel = new StackPanel();
            roomsPageStackPanel.Orientation = Orientation.Vertical;
            roomsPageStackPanel.Margin = new Thickness(10);


            Image homePng = Utils.loadImage("C:\\Users\\Aaron\\Source\\Repos\\RGBDelight\\WPF\\RGBDelightApp\\Images\\home_white_24dp.png", UriKind.Absolute);
            homePng.Width = Constants.iconWidth;
            homePng.Height = Constants.iconHeight;
            roomsPageStackPanel.Children.Add(homePng);

            Label roomsPageLabel = new Label();
            roomsPageLabel.Content = "Rooms";
            roomsPageLabel.FontSize = Constants.bottomBarFontSize;
            roomsPageLabel.Foreground = Utils.GetBrush(Colours.White);
            roomsPageStackPanel.Children.Add(roomsPageLabel);

            


            roomsPage.Content = roomsPageStackPanel;
            roomsPage.Background = Utils.GetBrush(Colours.BackgroundDark);
            roomsPage.BorderBrush = Utils.GetBrush(Colours.BackgroundDark);
            roomsPage.Click += roomsPageButtonClicked;
            //roomsPage.MouseEnter += roomsPageButtonMouseOver;
            Grid.SetRow(roomsPage, 3);
            Grid.SetColumn(roomsPage, 1);



            Button settingsPage = new Button();


            StackPanel settingsPageStackPanel = new StackPanel();
            roomsPageStackPanel.Orientation = Orientation.Vertical;
            roomsPageStackPanel.Margin = new Thickness(10);

            Image settingsPng = Utils.loadImage("C:\\Users\\Aaron\\Source\\Repos\\RGBDelight\\WPF\\RGBDelightApp\\Images\\settings_white_24dp.png", UriKind.Absolute);
            settingsPng.Width = Constants.iconWidth;
            settingsPng.Height = Constants.iconHeight;
            settingsPageStackPanel.Children.Add(settingsPng);

            Label settingsPageLabel = new Label();
            settingsPageLabel.Content = "Settings";
            settingsPageLabel.FontSize = Constants.bottomBarFontSize;
            settingsPageLabel.Foreground = Utils.GetBrush(Colours.White);
            settingsPageStackPanel.Children.Add(settingsPageLabel);


            settingsPage.Content = settingsPageStackPanel;
            settingsPage.Background = Utils.GetBrush(Colours.BackgroundDark);
            settingsPage.BorderBrush = Utils.GetBrush(Colours.BackgroundDark);
            settingsPage.Click += roomsPageButtonClicked;
            Grid.SetRow(settingsPage, 3);
            Grid.SetColumn(settingsPage, 2);

            if (currentPage == PageType.Rooms)
            {
                roomsPageLabel.Foreground = Utils.GetBrush(Colours.Blue);
                roomsPageLabel.FontSize = roomsPageLabel.FontSize + 2;
                homePng = Utils.loadImage("C:\\Users\\Aaron\\Source\\Repos\\RGBDelight\\WPF\\RGBDelightApp\\Images\\home_blue_24dp.png", UriKind.Absolute);
                homePng.Width = Constants.iconWidth + 2;
                homePng.Height = Constants.iconHeight + 2;
                roomsPageStackPanel.Children.Clear();
                roomsPageStackPanel.Children.Add(homePng);
                roomsPageStackPanel.Children.Add(roomsPageLabel);
                //roomsPageLabel.FontWeight = FontWeights.Bold;
            }
            else if(currentPage == PageType.Scenes)
            {
                scenesPageLabel.Foreground = Utils.GetBrush(Colours.Blue);
                scenesPageLabel.FontSize = roomsPageLabel.FontSize + 2;
                scenePng = Utils.loadImage("C:\\Users\\Aaron\\Source\\Repos\\RGBDelight\\WPF\\RGBDelightApp\\Images\\donut_large_blue_24dp.png", UriKind.Absolute);
                scenePng.Width = Constants.iconWidth+2;
                scenePng.Height = Constants.iconHeight+2;
                scenesPageStackPanel.Children.Clear();
                scenesPageStackPanel.Children.Add(scenePng);
                scenesPageStackPanel.Children.Add(scenesPageLabel);
            }


            View.Children.Add(scenesPage);
            View.Children.Add(roomsPage);
            View.Children.Add(settingsPage);
        }


        private void dummy()
        {
            Room room1 = new Room("Room 1");
            Room room2 = new Room("Room 2");

            mainVM.AddRoom(room1);
            mainVM.AddRoom(room2);

            LED light1 = new LED(23, 45, 122);
            LED light2 = new LED(11, 1, 1);

            mainVM.AddLight(room1, light1);
            mainVM.AddLight(room1, light2);

            Scene scene1 = new Scene("Scene 1");
            Scene scene2 = new Scene("Scene 2");

            mainVM.AddScene(scene1);
            mainVM.AddScene(scene2);

            LED light3 = new LED(23, 45, 122);
            LED light4 = new LED(11, 1, 1);

            light3.Brightness = 45;
            mainVM.AddLight(scene1, light3);
            mainVM.AddLight(scene1, light4);


            mainVM.ApplyScene(scene1, room1);
        }

        public MainWindow()
        {
            if(loggedIn == false)
            {
                currentPage = PageType.Login;
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

            dummy();


            Utils.AddGridDefinition(View, GridDefinitions.Column, 1, 3);
            Utils.AddGridDefinition(View, GridDefinitions.Row, 1);
            Utils.AddGridDefinition(View, GridDefinitions.Row, 5);
            Utils.AddGridDefinition(View, GridDefinitions.Row, 1);


            drawRoomsPage();

            drawBottomBar();;


            //View.ShowGridLines = true;

            this.Content = View;
            DataContext = this;
            
        }
    }
}

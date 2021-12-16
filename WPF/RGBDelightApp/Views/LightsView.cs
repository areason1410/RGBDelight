using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RGBDelight.Views
{
    public class LightsView : Window
    {
        public Grid RootGrid { get; private set; }
        private TextBox TextBox_Test;
        private Room room;
        private MainVM mainVM;

        private void backButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addLightButtonClicked(object sender, RoutedEventArgs e)
        {
            AddRoomPopup popup = new AddRoomPopup();
            popup.ShowDialog();
            mainVM.AddLight(room, new LED(0,0,0));
        }




        /// <summary>
        /// Creates a DataTemplate finish this lol
        /// https://stackoverflow.com/questions/17828417/centering-text-vertically-and-horizontally-in-textblock-and-passwordbox-in-windo
        /// </summary>
        /// <returns></returns>
        private DataTemplate LightListViewDataTemplate()
        {
            DataTemplate listViewDataTemplate = new DataTemplate();
            listViewDataTemplate.DataType = typeof(Room);

            FrameworkElementFactory roomElementFactory = new FrameworkElementFactory(typeof(Label));
            roomElementFactory.SetBinding(Label.ContentProperty, new Binding("RoomName"));
            roomElementFactory.SetValue(TextBlock.ToolTipProperty, "asdf");
            //roomElementFactory.SetValue(TextBlock.MinWidthProperty, screenWidth / 7.5);
            //roomElementFactory.SetValue(TextBlock.MinHeightProperty, screenHeight / 10);

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
        private ItemsPanelTemplate LightListViewItemsPanelTemplate()
        {
            ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate();
            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(WrapPanel));
            //frameworkElementFactory.SetValue(WrapPanel.MaxWidthProperty, screenWidth);
            //frameworkElementFactory.SetValue(TextBlock.MarginProperty, new Thickness(50));

            itemsPanelTemplate.VisualTree = frameworkElementFactory;
            return itemsPanelTemplate;

        }
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.style.setters?view=windowsdesktop-6.0#System_Windows_Style_Setters
        /// </summary>
        /// <returns></returns>
        private Style LightListItemContainerStyle()
        {
            Style style = new Style();
            style.TargetType = typeof(ListViewItem);
            //style.Setters.Add(new Setter(ListView.MarginProperty, new Thickness((screenWidth / 75))));

            return style;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTemplate LightsListViewDataTemplate()
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

        ListView lightList = new ListView();


        private void lightSelectionChanged(object sender, SelectionChangedEventArgs e)
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


        public LightsView(MainVM vm, Room selectedRoom)
        {
            mainVM = vm;
            room = selectedRoom;

            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.Background = Utils.GetBrush(Colours.BackgroundDark);

            this.RootGrid = new Grid();
            //{ HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };


            Utils.AddGridDefinition(RootGrid, GridDefinitions.Column, 1);
            Utils.AddGridDefinition(RootGrid, GridDefinitions.Row, 1);
            Utils.AddGridDefinition(RootGrid, GridDefinitions.Row, 5);
            Utils.AddGridDefinition(RootGrid, GridDefinitions.Row, 1);


            TextBlock roomText = new TextBlock();
            if(room != null) roomText.Text = room.RoomName;
            roomText.FontSize = 24;
            roomText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(roomText, 1);
            Grid.SetRow(roomText, 1);

            Button backButton = new Button();
            backButton.MinWidth = System.Windows.SystemParameters.WorkArea.Width / 10;
            backButton.Content = "<-";
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.Click += backButtonClicked;

            Button addLightButton = new Button();
            addLightButton.Content = "+ Add Light";
            addLightButton.MinWidth = System.Windows.SystemParameters.WorkArea.Width / 10;
            addLightButton.HorizontalAlignment = HorizontalAlignment.Right;
            addLightButton.Click += addLightButtonClicked;


            DockPanel topDock = new DockPanel();
            topDock.Children.Add(backButton);
            topDock.Children.Add(roomText);
            topDock.Children.Add(addLightButton);
            Grid.SetColumnSpan(topDock, 1);

            lightList.ItemTemplate = LightsListViewDataTemplate();
            lightList.ItemsSource = room.Lights;
            lightList.ItemsPanel = LightListViewItemsPanelTemplate();
            lightList.ItemContainerStyle = LightListItemContainerStyle();
            lightList.Background = Utils.GetBrush(Colours.BackgroundDefault);
            Grid.SetRow(lightList, 1);



            // Create a sqare grid with 20 pixel borders 

            // this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            // this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            // this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            // this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });

            // this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
            // this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });
            // this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });

            // // Create a new Textbox and place it in the middle of the root grid
            // TextBox_Test = new TextBox()
            // { Text = "ABC", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Top };

            // Grid.SetColumn(TextBox_Test, 1);
            // Grid.SetRow(TextBox_Test, 1);
            // this.RootGrid.Children.Add(TextBox_Test);

            // Grid GridForButtons = new Grid()
            // { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };

            // Button Button_Close = new Button() { Content = room.RoomName };
            // Button_Close.Click += Button_Create_Room;

            // // Add the button to the grid which has one cell by default
            // Grid.SetColumn(Button_Close, 0);
            // Grid.SetRow(Button_Close, 0);
            // GridForButtons.Children.Add(Button_Close);

            // // add the button grid to the RootGrid
            // Grid.SetRow(GridForButtons, 2);
            // Grid.SetColumn(GridForButtons, 1);
            // this.RootGrid.Children.Add(GridForButtons);

            // // Add the RootGrid to the content of the window
            // this.Content = this.RootGrid;

            // // fit the window size to the size of the RootGrid
            //this.SizeToContent = SizeToContent.WidthAndHeight;
            RootGrid.Children.Add(topDock);
            RootGrid.Children.Add(lightList);
            RootGrid.ShowGridLines = true;
            this.Content = RootGrid;
        }

    }
}
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
using RGBDelight.Models;
namespace RGBDelight.Views
{
    public class AddLightPopup : Window
    {
        public Grid RootGrid { get; private set; }
        public static string lightName;
        private TextBox TextBox_Test;
        public AddLightPopup()
        {
            this.WindowStyle = WindowStyle.ThreeDBorderWindow;

            this.RootGrid = new Grid()
            { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };

            // Create a sqare grid with 20 pixel borders 
            this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            this.RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });

            this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
            this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });
            this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });

            // Create a new Textbox and place it in the middle of the root grid
            TextBox_Test = new TextBox()
            { Text = "ABC", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Top };

            Grid.SetColumn(TextBox_Test, 1);
            Grid.SetRow(TextBox_Test, 1);
            this.RootGrid.Children.Add(TextBox_Test);

            Grid GridForButtons = new Grid()
            { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };

            Button Button_Close = new Button() { Content = "Add Light" };
            Button_Close.Click += Button_Create_Room;

            // Add the button to the grid which has one cell by default
            Grid.SetColumn(Button_Close, 0);
            Grid.SetRow(Button_Close, 0);
            GridForButtons.Children.Add(Button_Close);

            // add the button grid to the RootGrid
            Grid.SetRow(GridForButtons, 2);
            Grid.SetColumn(GridForButtons, 1);
            this.RootGrid.Children.Add(GridForButtons);

            // Add the RootGrid to the content of the window
            this.Content = this.RootGrid;

            // fit the window size to the size of the RootGrid
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void Button_Create_Room(object sender, RoutedEventArgs e)
        {
            lightName = (TextBox_Test.Text != "" && TextBox_Test != null) ? lightName = TextBox_Test.Text : lightName = "New Room";
            this.Close();
        }
    }
}
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
    public class LoginView : Window
    {
        public Grid RootGrid { get; private set; }
        private TextBox username;
        private PasswordBox password;
        public static string currentUsername;

        private void LoginButtonPressed(object sender, RoutedEventArgs e)
        {
            AccountData data = new AccountData(username.Text, password.Password);
            string resp = Utils.PostRequest("http://localhost:3000/database/verifyUser", data);
            if (resp == "good Password")
            {
                currentUsername = username.Text;
                this.Close();
            }
        }

        public LoginView()
        {
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.Background = Utils.GetBrush(Colours.BackgroundDark);

            this.RootGrid = new Grid();
            //{ HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };


            Utils.AddGridDefinition(RootGrid, GridDefinitions.Column, 1, 3);
            Utils.AddGridDefinition(RootGrid, GridDefinitions.Row, 1);
            Utils.AddGridDefinition(RootGrid, GridDefinitions.Row, 5);
            Utils.AddGridDefinition(RootGrid, GridDefinitions.Row, 1);


            StackPanel mainStack = new StackPanel();
            Grid.SetColumn(mainStack, 1);
            Grid.SetRow(mainStack, 1);
            mainStack.Orientation = Orientation.Vertical;
            mainStack.VerticalAlignment = VerticalAlignment.Center;

            username = new TextBox();
            username.KeyDown += Username_KeyDown;

            password = new PasswordBox();
            password.KeyDown += Password_KeyDown;


            Button loginButton = new Button();
            loginButton.Height = (double)100;
            loginButton.Content = "Login";
            loginButton.Click += LoginButtonPressed;

            mainStack.Children.Add(username);
            mainStack.Children.Add(password);
            mainStack.Children.Add(loginButton);


            RootGrid.Children.Add(mainStack);
            //RootGrid.ShowGridLines = true;
            this.Content = RootGrid;
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButtonPressed(null, null);
            }
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoginButtonPressed(null, null);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Net;
using Newtonsoft.Json;

namespace RGBDelight
{
    public enum GridDefinitions
    {
        Row,
        Column
    }

    public struct Colours
    {
        public const string BackgroundDefault = "#222222";
        public const string BackgroundDark = "#1D1D1D";
        public const string White = "#F6F7FB";
    }

    public struct Constants
    {
        public static double screenWidth = System.Windows.SystemParameters.WorkArea.Width;
        public static double screenHeight = System.Windows.SystemParameters.WorkArea.Height;
        public static Thickness TitleTextMargin = new Thickness(20, 0, 0, 0);
        public static Thickness LightsViewMargin = new Thickness(0, 30, 0, 0);
        public static Thickness LightsViewLabelMargin = new Thickness(0, 0, screenWidth/10, 0);
    }

    public static class ColorUtil
    {
        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        public static void HsvToRgb(double h, double S, double V, out byte r, out byte g, out byte b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((byte)(R * 255.0));
            g = Clamp((byte)(G * 255.0));
            b = Clamp((byte)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private static byte Clamp(byte i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }


    public class AccountData
    {
        public string username { get; set; }
        public string password { get; set; }

        public AccountData(string user, string pass)
        {
            this.username = user;
            this.password = pass;
        }
        public AccountData() { }
    }

    public class Utils
    {

        public static void AddColumnDefinition(Grid grid, int width)
        {
            ColumnDefinition column = new ColumnDefinition();
            GridLength size = new GridLength(width, GridUnitType.Star);
            column.Width = size;
            grid.ColumnDefinitions.Add(column);
        }

        public static void AddRowDefinition(Grid grid, int Height)
        {
            RowDefinition row = new RowDefinition();
            GridLength size = new GridLength(Height, GridUnitType.Star);
            row.Height = size;
            grid.RowDefinitions.Add(row);
        }

        public static void SetupMainTemplate(MainWindow window)
        {
            window.WindowStyle = WindowStyle.None;
            window.WindowState = WindowState.Maximized;
            window.Background = Utils.GetBrush(Colours.BackgroundDark);
        }


        public static Brush GetBrush(string colourHex)
        {
            BrushConverter bc = new BrushConverter();

            Brush brush = (Brush)bc.ConvertFrom(colourHex);

            brush.Freeze();

            return brush;
        }

        /// <summary>
        /// Create a Row or Column definition and automatically add it to the grid
        /// </summary>
        /// <param name="definition">GridDefinitions Enum type so Row or Column</param>
        /// <param name="val">Width or Height of the Column or Row (star values)</param>
        /// <param name="count">How many copies of the definition to be made</param>
        public static void AddGridDefinition(Grid grid, GridDefinitions definition, int val, int count = 1)
        {
            GridLength size = new GridLength(val, GridUnitType.Star);

            switch (definition)
            {
                case GridDefinitions.Row:
                    for (int i = 0; i < count; i++)
                    {
                        RowDefinition row = new RowDefinition();
                        row.Height = size;
                        grid.RowDefinitions.Add(row);
                    }
                    break;
                case GridDefinitions.Column:
                    for (int i = 0; i < count; i++)
                    {
                        ColumnDefinition column = new ColumnDefinition();
                        column.Width = size;
                        grid.ColumnDefinitions.Add(column);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// https://zetcode.com/csharp/getpostrequest/   lmao i guess i dont need to use generics
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostRequest(string url, object data)
        {

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";

            string jsonData = JsonConvert.SerializeObject(data);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);

            System.IO.Stream reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            request.ContentType = "application/json";
           // request.ContentLength = byteArray.Length;

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            System.IO.Stream respStream = response.GetResponseStream();

            System.IO.StreamReader reader = new System.IO.StreamReader(respStream);

            string finalResponse = reader.ReadToEnd();
            return finalResponse;

        }

    }
}

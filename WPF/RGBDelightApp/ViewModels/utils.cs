using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
    }
}

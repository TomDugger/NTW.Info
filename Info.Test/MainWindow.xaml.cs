using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Info.Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random rnd = new Random();

            List<Color> crl = new List<Color> { Colors.DodgerBlue, Colors.Maroon, Colors.Maroon };

            IEnumerable<Item> items = Enumerable.Range(0, 10).Select(x => new Item(rnd.Next(0, 10), rnd.Next(0, 10), crl[rnd.Next(0, 3)])).ToArray();

            scv.VerticalMetric = (item) =>
            {
                var value = (Item)item;
                return new Tuple<int, int, int, int, Color>(0, value.Row, 1, 1, value.Color);
            };

            scv.HorizontalMetric = (item) =>
            {
                var value = (Item)item;
                return new Tuple<int, int, int, int, Color>(value.Column, 0, 1, 1, value.Color);
            };

            scv.HorizontalGrouping = (item) => {
                item.Row = crl.IndexOf((Color)item.Color);
            };

            scv.VerticalGrouping = (item) => {
                item.Column = crl.IndexOf((Color)item.Color);
            };

            this.DataContext = items;

            scv.HorizontalDataSource = items;
            scv.VerticalDataSource = items;

            scv.RowCount = 10;
            scv.ColumnCount = 10;
        }

        public class Item {

            public Item(int row, int column, Color color) {
                this.Row = row;
                this.Column = column;
                this.Color = color;
            }

            public int Row { get; private set; }
            public int Column { get; private set; }
            public Color Color { get; private set; }
        }
    }
}

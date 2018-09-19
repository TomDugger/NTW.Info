using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Info.Models
{
    public class InfoItem: IEquatable<InfoItem>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public InfoItem() { }

        public InfoItem(int column, int row, int columnSpan, int rowSpan, Color? color = null) {
            this.Column = column;
            this.Row = row;
            this.ColumnSpan = columnSpan;
            this.RowSpan = rowSpan; 
            this.Color = color;
        }

        private int column;
        public int Column {
            get { return column; }
            set { column = value; this.SendProperyChnaged(nameof(Column)); }
        }

        private int row;
        public int Row {
            get { return row; }
            set { row = value; this.SendProperyChnaged(nameof(Row)); }
        }

        private int rowSpan = 1;
        public int RowSpan {
            get { return rowSpan; }
            set { rowSpan = value; this.SendProperyChnaged(nameof(RowSpan)); }
        }

        private int columnSpan = 1;
        public int ColumnSpan {
            get { return columnSpan; }
            set { columnSpan = value; this.SendProperyChnaged(nameof(ColumnSpan)); }
        }

        private Color? _color;
        public Color? Color {
            get { return _color; }
            set { _color = value; this.SendProperyChnaged(nameof(Color)); }
        }

        public static bool operator ==(InfoItem obj1, InfoItem obj2) {
            return obj1.row == obj2.row && obj1.column == obj2.column;
        }

        public static bool operator !=(InfoItem obj1, InfoItem obj2)
        {
            if (object.ReferenceEquals(obj1, null)) return false;
            if (object.ReferenceEquals(obj2, null)) return true;

            return obj1.row != obj2.row || obj1.column != obj2.column;
        }

        public bool Equals(InfoItem other)
        {
            return this.row.Equals(other.row) && this.column.Equals(other.column);
        }

        public override int GetHashCode()
        {
            int hashRow = Row.GetHashCode();

            int hashColumn = Column.GetHashCode();

            return hashRow ^ hashColumn;
        }

        protected void SendProperyChnaged(string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

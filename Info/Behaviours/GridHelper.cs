using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Info.Behaviours
{
    public class GridHelper
    {
        #region Dependency property
        public static readonly DependencyProperty RowCountProperty = DependencyProperty.RegisterAttached("RowCount", typeof(int), typeof(GridHelper), new PropertyMetadata(-1, RowCountChangedHandler));

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.RegisterAttached("ColumnCount", typeof(int), typeof(GridHelper), new PropertyMetadata(-1, ColumnCountChangedHandler));
        #endregion

        #region Accessors
        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }

        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }
        #endregion

        #region Property changed helpers
        public static void RowCountChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var grid = (System.Windows.Controls.Grid)obj;
            int c = GridHelper.GetRowCount(grid);

            if (!(obj is System.Windows.Controls.Grid) || c < 0 )
                return;

            grid.RowDefinitions.Clear();

            for (int i = 0; i < c; i++)
                grid.RowDefinitions.Add(new RowDefinition {  Height = new GridLength(1, GridUnitType.Star) });
        }

        public static void ColumnCountChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var grid = (System.Windows.Controls.Grid)obj;
            int c = GridHelper.GetColumnCount(grid);

            if (!(obj is System.Windows.Controls.Grid) || c < 0)
                return;

            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < c; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        #endregion
    }
}

using Info.Commands;
using Info.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Info.Controls
{
    [DefaultEvent("ScrollChangedEvent")]
    [Localizability(LocalizationCategory.Ignore)]
    [TemplatePart(Name = "PART_HorizontalScrollBar", Type = typeof(ScrollBarInfo))]
    [TemplatePart(Name = "PART_VerticalScrollBar", Type = typeof(ScrollBarInfo))]
    [TemplatePart(Name = "PART_ScrollContentPresenter", Type = typeof(ScrollContentPresenter))]
    public class ScrollViewInfo: ScrollViewer
    {
        static ScrollViewInfo() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewInfo), new FrameworkPropertyMetadata(typeof(ScrollViewInfo)));
        }

        public ScrollViewInfo():base() {
            var t = this.IsInitialized;
            this.CanContentScroll = true;
        }

        public IEnumerable HorizontalDataSource {
            get { return (IEnumerable)GetValue(HorizontalDataSourceProperty); }
            set { SetValue(HorizontalDataSourceProperty, value); }
        }

        public IEnumerable VerticalDataSource {
            get { return (IEnumerable)GetValue(VerticalDataSourceProperty); }
            set { SetValue(VerticalDataSourceProperty, value); }
        }

        public int RowCount {
            get { return (int)this.GetValue(RowCountProperty); }
            set { this.SetValue(RowCountProperty, value); }
        }

        public int ColumnCount {
            get { return (int)this.GetValue(ColumnCountProperty); }
            set { this.SetValue(ColumnCountProperty, value); }
        }

        public Func<object, Tuple<int, int, int, int, Color>> HorizontalMetric {
            get { return (Func<object, Tuple<int, int, int, int, Color>>)GetValue(HorizontalMetricProperty); }
            set { SetValue(HorizontalMetricProperty, value); }
        }

        public Func<object, Tuple<int, int, int, int, Color>> VerticalMetric {
            get { return (Func<object, Tuple<int, int, int, int, Color>>)GetValue(VerticalMetricProperty); }
            set { SetValue(VerticalMetricProperty, value); }
        }

        public Action<InfoItem> HorizontalSelectItem {
            get { return (Action<InfoItem>)GetValue(HorizontalSelectItemProperty); }
            set { SetValue(HorizontalSelectItemProperty, value); }
        }

        public Action<InfoItem> VerticalSelectItem {
            get { return (Action<InfoItem>)GetValue(VerticalSelectItemProperty); }
            set { SetValue(VerticalSelectItemProperty, value); }
        }

        public double HorisontalSizeItems {
            get { return (double)this.GetValue(HorisontalSizeItemsProperty); }
            set { this.SetValue(HorisontalSizeItemsProperty, value); }
        }

        public double VerticalSizeItems {
            get { return (double)this.GetValue(VerticalSizeItemsProperty); }
            set { this.SetValue(VerticalSizeItemsProperty, value); }
        }

        public Color ScrollColor {
            get { return (Color)this.GetValue(ScrollColorProperty); }
            set { this.SetValue(ScrollColorProperty, value); }
        }

        public Action<InfoItem> HorizontalGrouping {
            get { return (Action<InfoItem>)this.GetValue(HorizontalGroupingProperty); }
            set { this.SetValue(HorizontalGroupingProperty, value); }
        }

        public Action<InfoItem> VerticalGrouping {
            get { return (Action<InfoItem>)this.GetValue(VerticalGroupingProperty); }
            set { this.SetValue(VerticalGroupingProperty, value); }
        }

        public bool IsHorizontalShowMoreInfo {
            get { return (bool)this.GetValue(IsHorizontalShowMoreInfoProperty); }
            set { this.SetValue(IsHorizontalShowMoreInfoProperty, value); }
        }

        public bool IsVerticalShowMoreInfo {
            get { return (bool)this.GetValue(IsVerticalShowMoreInfoProperty); }
            set { this.SetValue(IsVerticalShowMoreInfoProperty, value); }
        }

        public double TopMarging {
            get { return (double)this.GetValue(TopMargingProperty); }
            set { this.SetValue(TopMargingProperty, value); }
        }

        public double LeftMarging {
            get { return (double)this.GetValue(LeftMargingProperty); }
            set { this.SetValue(LeftMargingProperty, value); }
        }


        public static readonly DependencyProperty HorizontalDataSourceProperty =
            DependencyProperty.Register("HorizontalDataSource", typeof(IEnumerable), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty VerticalDataSourceProperty =
            DependencyProperty.Register("VerticalDataSource", typeof(IEnumerable), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty HorizontalMetricProperty =
            DependencyProperty.Register("HorizontalMetric", typeof(Func<object, Tuple<int, int, int, int, Color>>), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty VerticalMetricProperty =
            DependencyProperty.Register("VerticalMetric", typeof(Func<object, Tuple<int, int, int, int, Color>>), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty RowCountProperty = DependencyProperty.Register("RowCount", typeof(int), typeof(ScrollViewInfo), new PropertyMetadata(0));

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(ScrollViewInfo), new PropertyMetadata(0));


        public static readonly DependencyProperty HorizontalSelectItemProperty = DependencyProperty.Register("HorizontalSelectItem", typeof(Action<InfoItem>), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty VerticalSelectItemProperty = DependencyProperty.Register("VerticalSelectItem", typeof(Action<InfoItem>), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty HorisontalSizeItemsProperty = DependencyProperty.Register("HorisontalSizeItems", typeof(double), typeof(ScrollViewInfo), new PropertyMetadata(70.0));

        public static readonly DependencyProperty VerticalSizeItemsProperty = DependencyProperty.Register("VerticalSizeItems", typeof(double), typeof(ScrollViewInfo), new PropertyMetadata(70.0));

        public static readonly DependencyProperty ScrollColorProperty = DependencyProperty.Register("ScrollColor", typeof(Color), typeof(ScrollViewInfo), new PropertyMetadata(Colors.DodgerBlue));

        public static readonly DependencyProperty HorizontalGroupingProperty = DependencyProperty.Register("HorizontalGrouping", typeof(Action<InfoItem>), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty VerticalGroupingProperty = DependencyProperty.Register("VerticalGrouping", typeof(Action<InfoItem>), typeof(ScrollViewInfo), new PropertyMetadata(null));

        public static readonly DependencyProperty IsHorizontalShowMoreInfoProperty = DependencyProperty.Register("IsHorizontalShowMoreInfo", typeof(bool), typeof(ScrollViewInfo), new PropertyMetadata(true));

        public static readonly DependencyProperty IsVerticalShowMoreInfoProperty = DependencyProperty.Register("IsVerticalShowMoreInfo", typeof(bool), typeof(ScrollViewInfo), new PropertyMetadata(true));

        public static readonly DependencyProperty TopMargingProperty = DependencyProperty.Register("TopMarging", typeof(double), typeof(ScrollViewInfo), new PropertyMetadata(0.0));

        public static readonly DependencyProperty LeftMargingProperty = DependencyProperty.Register("LeftMarging", typeof(double), typeof(ScrollViewInfo), new PropertyMetadata(0.0));

        #region Commands
        private Command _horizontalSelectItemCommand;
        public Command HorizontalSelectItemCommand {
            get {
                return _horizontalSelectItemCommand ?? (_horizontalSelectItemCommand = new Command(obj =>
                {
                    HorizontalSelectItem?.Invoke((InfoItem)obj);
                }));
            }
        }

        private Command _verticalSelectItemCommand;
        public Command VerticalSelectItemCommand {
            get {
                return _verticalSelectItemCommand ?? (_verticalSelectItemCommand = new Command(obj =>
                {
                    VerticalSelectItem?.Invoke((InfoItem)obj);
                }));
            }
        }
        #endregion
    }
}

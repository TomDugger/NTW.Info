using Info.Commands;
using Info.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Info.Controls
{
    public class ScrollBarInfo: ScrollBar
    {
        #region Fileds
        protected Func<object, InfoItem> ProcessingItem;

        protected bool IsBusy = false;
        private bool updateDataSourceAgain;
        #endregion

        static ScrollBarInfo() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollBarInfo), new FrameworkPropertyMetadata(typeof(ScrollBarInfo)));
        }

        public ScrollBarInfo() : base() {

        }

        #region Dependency property accessors
        public IEnumerable InfoItems {
            get { return (IEnumerable)GetValue(InfoItemsProperty); }
            set { SetValue(InfoItemsProperty, value); }
        }

        public IEnumerable DataSource {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public int RowCount {
            get { return (int)this.GetValue(RowCountProperty); }
            set { this.SetValue(RowCountProperty, value); }
        }

        public int ColumnCount {
            get { return (int)this.GetValue(ColumnCountProperty); }
            set { this.SetValue(ColumnCountProperty, value); }
        }

        public Func<object, Tuple<int, int, int, int, Color>> Metric {
            get { return (Func<object, Tuple<int, int, int, int, Color>>)GetValue(MetricProperty); }
            set { SetValue(MetricProperty, value); }
        }

        public Action<InfoItem> Grouping {
            get { return (Action<InfoItem>)this.GetValue(GroupingProperty); }
            set { this.SetValue(GroupingProperty, value); }
        }
        #endregion


        #region Dependency property
        protected static readonly DependencyProperty InfoItemsProperty =
            DependencyProperty.Register("InfoItems", typeof(IEnumerable), typeof(ScrollBarInfo), new PropertyMetadata(null));

        protected static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(ScrollBarInfo), new PropertyMetadata(null, new PropertyChangedCallback((d, a) => {
                var scroll = (ScrollBarInfo)d;
                scroll.UpdateCache();
            })));

        public static readonly DependencyProperty RowCountProperty = DependencyProperty.Register("RowCount", typeof(int), typeof(ScrollBarInfo), new PropertyMetadata(0));

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(ScrollBarInfo), new PropertyMetadata(0));

        protected static readonly DependencyProperty MetricProperty =
            DependencyProperty.Register("Metric", typeof(Func<object, Tuple<int, int, int, int, Color>>), typeof(ScrollBarInfo), new PropertyMetadata(new Func<object, Tuple<int, int, int, int, Color>>((item) => {
                return null;
            }), new PropertyChangedCallback((d, a) => {
                ScrollBarInfo sender = (ScrollBarInfo)d;
                Func<object, Tuple<int, int, int, int, Color>> processingInfo = (Func<object, Tuple<int, int, int, int, Color>>)a.NewValue;

                sender.ProcessingItem = new Func<object, InfoItem>((item) => {
                    InfoItem result = new InfoItem();
                    if (processingInfo != null)
                    {
                        var proc = processingInfo(item);
                        if (proc != null)
                            result = new InfoItem(proc.Item1, proc.Item2, proc.Item3, proc.Item4, proc.Item5 == default(Color) ? null : (Color?)proc.Item5);
                        else result = null;
                    }
                    else result = null;
                    return result;
                });

                sender.UpdateCache();
            })));

        protected static readonly DependencyProperty GroupingProperty =
            DependencyProperty.Register("Grouping", typeof(Action<InfoItem>), typeof(ScrollBarInfo), new PropertyMetadata(null, new PropertyChangedCallback((d, a) => {
                ScrollBarInfo sender = (ScrollBarInfo)d;
                sender.UpdateCache();
            })));
        #endregion

        #region Methods (helps)
        public void SetMetricParametry(Func<object, Tuple<int, int, int, int, Color>> processingInfo) {

            ProcessingItem = new Func<object, InfoItem>((item) => {
                InfoItem result = new InfoItem();
                if (processingInfo != null) {
                    var proc = processingInfo(item);
                    result = new InfoItem(proc.Item1, proc.Item2, proc.Item3, proc.Item4, proc.Item5);
                }
                return result;
            });

            UpdateCache();
        }

        protected void UpdateCache() {

            var items = new InfoItem[0];

            if (this.IsBusy) {

                this.updateDataSourceAgain = true;
                return;
            }

            var datasource = this.DataSource;

            if (datasource == null)
            {
                UpdateData(items);
                return;
            }

            this.IsBusy = true;

            bool updateAgain = false;
            Action<InfoItem> group = Grouping;

            Task.Factory.StartNew(() => {
                do
                {
                    if (this.updateDataSourceAgain)
                    {
                        updateAgain = true;
                        this.updateDataSourceAgain = false;
                        continue;
                    }

                    var arrayItems = datasource.Cast<object>().ToArray();
                    items = new InfoItem[arrayItems.Length];

                    for (int i = 0; i < items.Length; i++) {
                        var res = ProcessingItem(arrayItems[i]);
                        if (res != null) {
                            items[i] = res;
                        }
                    }

                    //items = items.Where(x => x != null).ToArray();

                    if (group != null)
                        foreach (var item in items)
                            group(item);

                    updateAgain = false;
                    this.updateDataSourceAgain = false;
                    this.IsBusy = false;

                } while (updateAgain);

            }).ContinueWith((task) => {
                this.IsBusy = false;

                UpdateData(items);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected void UpdateData(IEnumerable<InfoItem> items) {

            //List<InfoItem> dataItems = items.ToList();

            this.InfoItems = items;
        }
        #endregion
    }

    class Comparer : IEqualityComparer<InfoItem>
    {
        public bool Equals(InfoItem x, InfoItem y)
        {
            return !((x.Row >= y.Row) && (x.Row <= y.Row + y.RowSpan))
                && !((x.Column >= y.Column) && (x.Column <= y.Column + y.ColumnSpan))
                ;
        }

        public int GetHashCode(InfoItem obj)
        {
            int hashRow = obj.Row.GetHashCode();

            int hashColumn = obj.Column.GetHashCode();

            int hashRowSpan = obj.RowSpan.GetHashCode();

            int hashColumnSpan = obj.ColumnSpan.GetHashCode();

            return hashRow ^ hashColumn ^ hashRowSpan ^ hashColumnSpan;
        }
    }
}

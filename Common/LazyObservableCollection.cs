using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MaximStartsev.SmallUtilities.Common
{
    public sealed class LazyObservableCollection<T>:ObservableCollection<T>
    {
        private List<T> _list;
        public LazyObservableCollection(List<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            _list = list;
            CollectionChanged += LazyCollection_CollectionChanged;
        }

        private void LazyCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    _list.AddRange(e.NewItems.Cast<T>());
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        _list.Remove(item);
                    }
                    break;
            }
        }
    }
}

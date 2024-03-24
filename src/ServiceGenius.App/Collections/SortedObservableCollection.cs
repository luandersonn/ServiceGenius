using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ServiceGenius.App.Collections;

public class SortedObservableCollection<T> : ObservableCollection<T>
{
    #region fields		
    private readonly List<T> _items = [];
    private Func<T, bool> _filter;
    private IComparer<T> _comparer;
    private bool _suppressNotification;
    #endregion

    #region properties		
    public IReadOnlyCollection<T> AllItems => _items.AsReadOnly();

    public Func<T, bool> Filter
    {
        get => _filter;
        set
        {
            _filter = value;

            if (value is null)
            {
                if (_items is not null)
                {
                    foreach (T item in _items)
                    {
                        int index = FindIndexItem(item);
                        if (index < 0)
                        {
                            index = ~index;
                            Insert(index, item);
                        }
                    }
                }
            }
            else
            {
                foreach (T item in _items)
                {
                    int index = FindIndexItem(item);
                    if (_filter(item))
                    {
                        if (index < 0)
                        {
                            index = ~index;
                            Insert(index, item);
                        }
                    }
                    else
                    {
                        if (index >= 0)
                            RemoveAt(index);
                    }
                }
            }
        }
    }

    public IComparer<T> Comparer
    {
        get => _comparer;
        set
        {
            ClearItems();
            _comparer = value ?? throw new ArgumentNullException(nameof(Comparer));
            foreach (T item in _items)
            {
                if (_filter == null || _filter(item))
                    AddOnObservableCollection(item);
            }
        }
    }
    #endregion

    #region constructor		
    public SortedObservableCollection(IComparer<T> comparer) => Comparer = comparer;

    public SortedObservableCollection(IComparer<T> comparer, IEnumerable<T> items) : this(comparer) => AddRange(items);
    #endregion

    #region public methods		
    public new void Add(T item)
    {
        if (_filter == null || _filter(item))
            AddOnObservableCollection(item);
        _items.Add(item);
    }

    public void AddRange(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        try
        {
            _suppressNotification = true;
            foreach (T item in collection)
            {
                Add(item);
            }
        }
        finally
        {
            _suppressNotification = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    public new bool Remove(T item)
    {
        if (_filter == null || _filter(item))
            RemoveFromObservableCollection(item);
        return _items.Remove(item);
    }

    public new void Clear()
    {
        _items.Clear();
        base.ClearItems();
    }
    #endregion

    #region protected methods		
    protected int FindIndexItem(T item)
    {
        int min = 0;
        int max = Count - 1;
        while (min <= max)
        {
            int mid = (min + max) / 2;
            if (Comparer.Compare(item, this[mid]) == 0)
            {
                return mid;
            }
            else if (Comparer.Compare(item, this[mid]) < 0)
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }
        return ~(max + 1);
    }

    protected void AddOnObservableCollection(T item)
    {
        if (Count != 0)
        {
            int index = FindIndexItem(item);
            if (index < 0)
                index = ~index;
            Insert(index, item);
        }
        else
            base.Add(item);
    }
    protected bool RemoveFromObservableCollection(T item)
    {
        if (Count == 0)
            return false;
        int index = FindIndexItem(item);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!_suppressNotification)
            base.OnCollectionChanged(e);
    }
    #endregion
}

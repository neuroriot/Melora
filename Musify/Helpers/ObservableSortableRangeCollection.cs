﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Musify.Helpers;

public class ObservableSortableRangeCollection<T> : ObservableCollection<T>
{
    readonly IList<T> defaultOrderItems = [];

    public void ForceRefresh() =>
        OnCollectionChanged(new(NotifyCollectionChangedAction.Reset));


    public void AddRange(
        IEnumerable<T> items)
    {
        try
        {
            CheckReentrancy();

            foreach (T item in items)
            {
                Items.Add(item);
                defaultOrderItems.Add(item);
            }
        }
        finally
        {
            ForceRefresh();
        }
    }

    public new void Add(
        T item)
    {
        try
        {
            Items.Add(item);
            defaultOrderItems.Add(item);
        }
        finally
        {
            ForceRefresh();
        }
    }


    public new bool Remove(
        T item)
    {
        try
        {
            return Items.Remove(item) && defaultOrderItems.Remove(item);
        }
        finally
        {
            ForceRefresh();
        }
    }

    public new void RemoveAt(
        int index)
    {
        try
        {
            Items.RemoveAt(index);
            defaultOrderItems.RemoveAt(index);
        }
        finally
        {
            ForceRefresh();
        }
    }

    public void RemoveRange(
        IEnumerable<T> items)
    {
        try
        {
            CheckReentrancy();

            foreach (T item in items)
            {
                Items.Remove(item);
                defaultOrderItems.Remove(item);
            }
        }
        finally
        {
            ForceRefresh();
        }
    }


    public void OrderBy<TKey>(
        Func<T, TKey> keySelector,
        bool descending = false)
    {
        QuickSort(0, Items.Count - 1, keySelector, descending);
        ForceRefresh();
    }

    public void OrderbyDefault(
        bool descending = false)
    {
        Items.Clear();
        if (descending)
            for (int i = defaultOrderItems.Count - 1; i >= 0; i--)
                Items.Add(defaultOrderItems[i]);
        else
            foreach (T item in defaultOrderItems)
                Items.Add(item);

        ForceRefresh();
    }


    void QuickSort<TKey>(
        int left,
        int right,
        Func<T, TKey> keySelector,
        bool descending)
    {
        if (left >= right)
            return;

        int pivotIndex = Partition(left, right, keySelector, descending);
        QuickSort(left, pivotIndex - 1, keySelector, descending);
        QuickSort(pivotIndex + 1, right, keySelector, descending);
    }

    int Partition<TKey>(
        int left,
        int right,
        Func<T, TKey> keySelector,
        bool descending)
    {
        T pivotValue = Items[right];
        int pivotIndex = left;

        for (int i = left; i < right; i++)
        {
            int comparisonResult = Comparer<TKey>.Default.Compare(keySelector(Items[i]), keySelector(pivotValue));
            if (descending)
                comparisonResult = -comparisonResult;

            if (comparisonResult < 0)
            {
                Swap(i, pivotIndex);
                pivotIndex++;
            }
        }

        Swap(pivotIndex, right);
        return pivotIndex;
    }

    void Swap(int i, int j) =>
        (Items[j], Items[i]) = (Items[i], Items[j]);
}
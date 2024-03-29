using System;
using System.Collections.ObjectModel;

namespace App.ExchangeXAML
{
    public class ObservableResourceCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        protected override void InsertItem(int index, T item)
        {
            // A simple check to see if the item is already in the list.
            if (Items.Contains(item))
                throw new Exception($"Item: '{item}' is already in the list!");
            base.InsertItem(index, item);
        }
    }
}
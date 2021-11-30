using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;

namespace YouTubeGUI.Controls.HomeContent
{
    public class ContentItemContainerGenerator : IItemContainerGenerator
    {
        public ItemContainerInfo Materialize(int index, object item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemContainerInfo> Dematerialize(int startingIndex, int count)
        {
            throw new NotImplementedException();
        }

        public void InsertSpace(int index, int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemContainerInfo> RemoveRange(int startingIndex, int count)
        {
            throw new NotImplementedException();
        }

        public bool TryRecycle(int oldIndex, int newIndex, object item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemContainerInfo> Clear()
        {
            throw new NotImplementedException();
        }

        public IControl ContainerFromIndex(int index)
        {
            throw new NotImplementedException();
        }

        public int IndexFromContainer(IControl container)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemContainerInfo> Containers { get; }
        public IDataTemplate ItemTemplate { get; set; }
        public Type ContainerType { get; }
        public event EventHandler<ItemContainerEventArgs>? Materialized;
        public event EventHandler<ItemContainerEventArgs>? Dematerialized;
        public event EventHandler<ItemContainerEventArgs>? Recycled;
    }
}
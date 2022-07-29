using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace App.ResourceManagement
{
    public interface ISwitchingResource : IResourceProvider
    {
        public string? Identifier { get; set; }
        public Uri? BaseUri { get; set; }
        public ObservableCollection<ResourceData> Resources { get; }
    }
}
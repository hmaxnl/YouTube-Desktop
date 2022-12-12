using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using App.Management;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.Styling;

namespace App.ExchangeXAML
{
    public class StyleExchanger : AvaloniaObject, IStyle, IResourceProvider
    {
        public StyleExchanger()
        {
            if (Loaded != null) return;
            Loaded = new Avalonia.Styling.Styles();
            StyleResources.CollectionChanged += StyleResourcesOnCollectionChanged;
            ExchangeManager.StyleExchangers.Add(this);
        }
        
        public IStyle? Loaded;
        public string DefaultId { get; set; } = string.Empty;
        [Content]
        public ObservableResourceCollection<ExchangeResource> StyleResources { get; } = new ObservableResourceCollection<ExchangeResource>();
        private ExchangeResource? _selectedResource;
        public IReadOnlyList<IStyle> Children => Loaded?.Children ?? Array.Empty<IStyle>();
        public bool HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;
        public IResourceHost? Owner { get; }
        public event EventHandler? OwnerChanged
        {
            add
            {
                if (Loaded is IResourceProvider rp)
                    rp.OwnerChanged += value;
            }
            remove
            {
                if (Loaded is IResourceProvider rp)
                    rp.OwnerChanged -= value;
            }
        }
        private string _identifier = string.Empty;
        public string Identifier
        {
            get => _identifier;
            set
            {
                if (value == string.Empty)
                    throw new Exception("Value cannot be null!");
                // Only set this once!
                if (string.IsNullOrEmpty(_identifier))
                    _identifier = value;
            }
        }

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded?.TryAttach(target, host) ?? SelectorMatchResult.NeverThisType;
        public bool TryGetResource(object key, out object? value)
        {
            if (Loaded is IResourceProvider rp)
                return rp.TryGetResource(key, out value);
            value = null;
            return false;
        }
        public void AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);
        public void RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);

        public void Exchange(ExchangeResource newExchangeResource)
        {
            if (!StyleResources.Contains(newExchangeResource) || newExchangeResource.Style == null || _selectedResource?.Style == null) return;
            if (!(Loaded as Avalonia.Styling.Styles)?.Contains(_selectedResource.Style) ?? true) return;
            (Loaded as Avalonia.Styling.Styles)?.Remove(_selectedResource.Style);
            _selectedResource = newExchangeResource;
            (Loaded as Avalonia.Styling.Styles)?.Add(_selectedResource.Style);
        }

        private void StyleResourcesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null || string.IsNullOrEmpty(DefaultId)) return;
            foreach (ExchangeResource resource in e.NewItems)
            {
                if (resource.Style == null || !resource.Identifier.Equals(DefaultId)) continue;
                _selectedResource = resource;
                (Loaded as Avalonia.Styling.Styles)?.Add(resource.Style);
            }
        }
    }
}
using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace App.ExchangeXAML
{
    public class ExchangeResource : IdentifierBase
    {
        public ExchangeResource(IServiceProvider provider) => BaseUri = ((IUriContext)provider.GetService(typeof(IUriContext))!).BaseUri;
        
        private Uri? _resourceUri;
        public Uri? ResourceUri
        {
            get => _resourceUri;
            set
            {
                if (value == null || _resourceUri != null) return;
                _resourceUri = value;
                ResourceProvider = (ResourceDictionary)AvaloniaXamlLoader.Load(ResourceUri, BaseUri);
            }
        }
        public IResourceProvider? ResourceProvider { get; private set; }

        private Uri? _styleUri;
        public Uri? StyleUri
        {
            get => _styleUri;
            set
            {
                if (value == null || _styleUri != null) return;
                _styleUri = value;
                Style = (Avalonia.Styling.Styles)AvaloniaXamlLoader.Load(StyleUri, BaseUri);
            }
        }
        public Avalonia.Styling.Styles? Style { get; private set; }
        
        private Uri? BaseUri { get; }
        public void TryLoadResource(Uri? baseUri = null)
        {
            if (ResourceUri != null && ResourceProvider == null)
                ResourceProvider = (ResourceDictionary)AvaloniaXamlLoader.Load(ResourceUri, baseUri);
            if (StyleUri != null && Style == null)
                Style = (Avalonia.Styling.Styles)AvaloniaXamlLoader.Load(StyleUri, baseUri);
        }
    }
}
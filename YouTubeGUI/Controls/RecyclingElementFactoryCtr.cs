using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using YouTubeGUI.Core.XamlTools;
using YouTubeScrap.Core;

namespace YouTubeGUI.Controls
{
    public class RecyclingElementFactoryCtr : RecyclingElementFactory
    {
        public RecyclingElementFactoryCtr()
        {
            _dts = new DataTemplateSelector();
            _dts.IsItemTemplates = true;
        }

        private readonly DataTemplateSelector _dts;
        protected override IControl GetElementCore(ElementFactoryGetArgs args)
        {
            string tempName = args.Data.ToString();
            if (tempName.IsNullEmpty())
                return new DataTemplate().Build(args.Data);
            var element = RecyclePool.TryGetElement(tempName, args.Parent);
            if (element is null)
            {
                element = _dts.Build(args.Data);
                if (element != null)
                    RecyclePool.PutElement(element, tempName, args.Parent);
            }
            return element;
        }
    }
}
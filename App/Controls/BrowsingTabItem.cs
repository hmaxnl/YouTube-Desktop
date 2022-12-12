using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace App.Controls
{
    public class BrowsingTabItem : TabItem
    {
        private Button? _closeButton;
        public BrowsingTabItem()
        {
            Header = Content;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            _closeButton = e.NameScope.Get<Button>("CloseTabButton");
            if (_closeButton == null) return;
            _closeButton.Command = ReactiveCommand.Create(() =>
            {
                if (Parent is BrowsingTabControl btc)
                {
                    btc.Tabs.Remove(this);
                }
            });
        }
    }
}
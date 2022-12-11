using System;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace App.Controls
{
    public class BrowsingTabControl : TabControl
    {
        private Button? _addButton;
        private AvaloniaList<object> Tabs => (Items as AvaloniaList<object>) ?? throw new InvalidOperationException();
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            _addButton = e.NameScope.Get<Button>("AddTabButton");
            _addButton.Command = ReactiveCommand.Create(() =>
            {
                AddTab("Header", "Content");
            });
        }

        private void AddTab(string header, object content)
        {
            TabItem tab = new TabItem()
            {
                Classes = { "BrowsingTabItem" },
                Header = header,
                Content = content
            };
            Tabs.Add(tab);
        }
    }
}
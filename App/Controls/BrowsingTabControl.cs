using System;
using App.Models;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace App.Controls
{
    public class BrowsingTabControl : TabControl
    {
        private Button? _addButton;
        public AvaloniaList<object> Tabs => (Items as AvaloniaList<object>) ?? throw new InvalidOperationException();
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
            BrowsingTabItem tab = new BrowsingTabItem()
            {
                Content = new HomeModel()
            };
            Tabs.Add(tab);
        }
    }
}
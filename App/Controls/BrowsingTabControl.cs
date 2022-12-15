using System;
using App.ViewModels;
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
                AddTab(new HomeViewModel()); // Default to the home page/model
            });
        }

        private void AddTab(object content)
        {
            BrowsingTabItem tab = new BrowsingTabItem()
            {
                Content = content
            };
            Tabs.Add(tab);
        }
    }
}
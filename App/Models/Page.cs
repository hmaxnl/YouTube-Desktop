using System;
using System.Collections.Generic;

namespace App.Models
{
    public sealed class Page : IDisposable
    {
        public Page(Workspace workspace)
        {
            _workspace = workspace ?? throw new ArgumentNullException(nameof(workspace), "The workspace cannot be null!");
            OnInitialized(this);
        }

        public event Action<Page> Initialized;
        
        private readonly Workspace _workspace;

        private void OnInitialized(Page obj) => Initialized?.Invoke(obj);
        
        public void Dispose()
        {
        }
    }
}
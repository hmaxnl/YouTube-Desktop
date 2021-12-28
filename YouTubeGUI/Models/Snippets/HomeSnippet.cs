using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Models.Snippets
{
    public class HomeSnippet : SnippetBase
    {
        public HomeSnippet(ResponseMetadata meta) : base(meta)
        {
            _bgItemFilter = new BackgroundWorker();
            _bgItemFilter.DoWork += BgItemFilterOnDoWork;
            _bgItemFilter.RunWorkerCompleted += BgItemFilterOnRunWorkerCompleted;
            _bgItemFilter.RunWorkerAsync();
        }
        
        public event Action? OnContentsChanged;
        public Tab? Tab { get; private set; }
        public List<object?> Contents => Tab?.Content.Contents;
        
        private readonly List<RichItemRenderer> _itemContents = new List<RichItemRenderer>();
        public List<RichItemRenderer> ItemContents => _itemContents;
        
        private readonly List<RichSectionRenderer> _sectionContents = new List<RichSectionRenderer>();
        public List<RichSectionRenderer> SectionContents => _sectionContents;

        public ContinuationItemRenderer? CurrentContinuation { get; private set; }


        private readonly BackgroundWorker _bgItemFilter;
        
        public void UpdateContents(ResponseMetadata respMeta)
        {
            // Update the list with the new contents/tabs!
            _bgItemFilter.RunWorkerAsync();
        }
        private void InvokeOnContentsChanged() => OnContentsChanged?.Invoke();
        private void BgItemFilterOnDoWork(object sender, DoWorkEventArgs e)
        {
            if (Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs.Count > 1)
                Trace.WriteLine("There is more than one tab! This is not handled, report this to the developers!");
            foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
            {
                Tab = tab;
            }

            foreach (var content in Contents)
            {
                switch (content)
                {
                    case RichItemRenderer rir:
                        _itemContents.Add(rir);
                        break;
                    case RichSectionRenderer rsr:
                        _sectionContents.Add(rsr);
                        break;
                    case ContinuationItemRenderer cir:
                        CurrentContinuation = cir;
                        break;
                }
            }
        }
        private void BgItemFilterOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeOnContentsChanged();
        }
    }
}
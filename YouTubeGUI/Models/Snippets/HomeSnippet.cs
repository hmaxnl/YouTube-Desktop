using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using YouTubeGUI.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Models.Snippets
{
    public class HomeSnippet : SnippetBase
    {
        public HomeSnippet(ResponseMetadata meta) : base(meta)
        {
            _bgItemFilter = new BackgroundWorker();
            _bgContinuationWorker = new BackgroundWorker();
            MetaChanged += OnMetaChanged;
            _bgItemFilter.DoWork += BgItemFilterOnDoWork;
            _bgItemFilter.RunWorkerCompleted += BgItemFilterOnRunWorkerCompleted;
            _bgContinuationWorker.DoWork += ContinuationWorkerOnDoWork;
            Contents = new List<object?>();
            lock (_bgItemFilter)
                _bgItemFilter.RunWorkerAsync();
        }

        public event Action? ContentsChanged;
        public Tab? Tab { get; private set; }
        public List<object?>? Contents { get; }
        
        public ContinuationItemRenderer? CurrentContinuation { get; private set; }
        
        private readonly BackgroundWorker _bgItemFilter;
        private readonly BackgroundWorker _bgContinuationWorker;
        
        public void UpdateContents(ResponseMetadata respMeta) => Metadata = respMeta;

        public void LoadContinuation(YoutubeUser user)
        {
            lock (_bgContinuationWorker)
            {
                if (!_bgContinuationWorker.IsBusy)
                    _bgContinuationWorker.RunWorkerAsync(user);
                else
                    Logger.Log("The bg worker is busy, could not receive continuation data!", LogType.Warning);
            }
        }
        
        private void InvokeOnContentsChanged() => ContentsChanged?.Invoke();
        private void OnMetaChanged()
        {
            lock (_bgItemFilter)
                _bgItemFilter.RunWorkerAsync();
        }
        private void BgItemFilterOnDoWork(object sender, DoWorkEventArgs e)
        {
            if (Metadata?.Contents != null)
            {
                if (Metadata?.Contents.TwoColumnBrowseResultsRenderer.Tabs.Count > 1)
                    Trace.WriteLine("There is more than one tab! This is not handled, report this to the developers!");
                foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
                {
                    Tab = tab;
                    FilterItems(tab.Content.Contents);
                }
            }

            if (Metadata?.ResponseReceivedActions != null)
            {
                foreach (var rra in Metadata.ResponseReceivedActions)
                {
                    FilterItems(rra.ContinuationItemsAction.ContinuationItems);
                }
            }
        }
        private void BgItemFilterOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => InvokeOnContentsChanged();
        private void ContinuationWorkerOnDoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is not YoutubeUser youtubeUser) return;
            if (CurrentContinuation == null) return;
            ContinuationItemRenderer cir = CurrentContinuation;
            CurrentContinuation = null;
                
            var conReq = youtubeUser.GetApiMetadataAsync(ApiRequestType.Endpoint, endpoint: cir.Endpoint).Result;
            Metadata = conReq;
        }
        private void FilterItems(List<object> items)
        {
            List<RichItemRenderer> itemRenderers = new List<RichItemRenderer>();
            foreach (var content in items)
            {
                switch (content)
                {
                    case RichItemRenderer rir:
                        itemRenderers.Add(rir);
                        break;
                    case RichSectionRenderer rsr:
                        if (itemRenderers.Count >= 1)
                        {
                            Contents?.Add(new ItemSection() { Items = new List<RichItemRenderer>(itemRenderers) });
                            itemRenderers.Clear();
                        }
                        Contents?.Add(rsr);
                        break;
                    case ContinuationItemRenderer cir:
                        CurrentContinuation = cir;
                        break;
                }
            }

            if (itemRenderers.Count > 0)
            {
                Contents?.Add(new ItemSection() { Items = new List<RichItemRenderer>(itemRenderers) });
                itemRenderers.Clear();
            }
        }
    }

    public class ItemSection
    {
        public List<RichItemRenderer> Items { get; set; }
        public int SectionIndex { get; set; }
    }
}
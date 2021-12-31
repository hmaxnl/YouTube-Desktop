using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
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
            MetaChanged += OnMetaChanged;
            _bgItemFilter.DoWork += BgItemFilterOnDoWork;
            _bgItemFilter.RunWorkerCompleted += BgItemFilterOnRunWorkerCompleted;
            lock (_bgItemFilter)
                _bgItemFilter.RunWorkerAsync();
        }

        public event Action? ContentsChanged;
        public Tab? Tab { get; private set; }
        public List<object?>? Contents => Tab?.Content.Contents;
        
        private readonly List<RichItemRenderer> _itemContents = new List<RichItemRenderer>();
        public List<RichItemRenderer> ItemContents => _itemContents;
        
        private readonly List<RichSectionRenderer> _sectionContents = new List<RichSectionRenderer>();
        public List<RichSectionRenderer> SectionContents => _sectionContents;
        
        public ContinuationItemRenderer? CurrentContinuation { get; private set; }
        
        private readonly BackgroundWorker _bgItemFilter;
        
        public void UpdateContents(ResponseMetadata respMeta) => Metadata = respMeta;

        public void LoadContinuation(YoutubeUser user)
        {
            /*if (CurrentContinuation == null) return;
            ContinuationItemRenderer cir = CurrentContinuation;
            CurrentContinuation = null;*/
            // Need to be on a bg worker.
            /*Task.Run(async () =>
            {
                var conReq = await user.GetApiMetadataAsync(ApiRequestType.Endpoint, endpoint: cir.Endpoint);
            });*/
        }
        
        private void InvokeOnContentsChanged() => ContentsChanged?.Invoke();
        private void OnMetaChanged()
        {
            lock (_bgItemFilter)
                _bgItemFilter.RunWorkerAsync();
        }
        private void BgItemFilterOnDoWork(object sender, DoWorkEventArgs e)
        {
            if (Metadata?.Contents.TwoColumnBrowseResultsRenderer.Tabs.Count > 1)
                Trace.WriteLine("There is more than one tab! This is not handled, report this to the developers!");
            foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
            {
                Tab = tab;
            }

            if (Contents == null) return;
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
        private void BgItemFilterOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => InvokeOnContentsChanged();
    }
}
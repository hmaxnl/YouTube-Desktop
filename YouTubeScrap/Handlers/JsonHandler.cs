using System;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;
using YouTubeScrap.Core.ReverseEngineer.Cipher;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Handlers
{
    //INFO: Implementing deserialize converter when thats done this can be removed!
    public static class JsonHandler
    {
        public static JObject RestructureJSONArray(JArray jsonArray)
        {
            return JObject.FromObject(jsonArray);
        }
        private static JObject playerProperties;
        public static JObject RestructureJSON(JObject jsonObject, JObject properties = null)// Response properties.
        {
            playerProperties = properties;
            JObject jsonReconstructured = new JObject();
            // Check for properties and extract them.
            if (jsonObject.TryGetValue("responseContext", out JToken responseContextToken))// The response context.
                jsonReconstructured.Add("responseContext", responseContextToken);
            if (jsonObject.TryGetValue("actions", out JToken actionToken))// The popup menu for logged in account.
                jsonReconstructured.Add("actions", ExtractActions(actionToken));
            if (jsonObject.TryGetValue("contents", out JToken contentsToken))// Main contents property.
                jsonReconstructured.Add("contents", ExtractContents(contentsToken));
            if (jsonObject.TryGetValue("onResponseReceivedActions", out JToken continuedContentsToken))// Contents from continuation.
                jsonReconstructured.Add("continuationContents", ExtractContents(continuedContentsToken));
            if (jsonObject.TryGetValue("header", out JToken headerToken))// The header
                jsonReconstructured.Add("headerText", ExtractRuns(headerToken.TryGetToken("feedTabbedHeaderRenderer", out JToken feedTabbedHeaderToken) ? feedTabbedHeaderToken["title"] : null));
            if (jsonObject.TryGetValue("topbar", out JToken topbarToken))// Topbar contents
                jsonReconstructured.Add("topbarMetadata", ExtractTopbar(topbarToken));
            if (jsonObject.TryGetValue("playabilityStatus", out JToken playabilityStatusToken))
                jsonReconstructured.Add("playabiblityStatus", playabilityStatusToken);
            if (jsonObject.TryGetValue("streamingData", out JToken streamingDataToken))
                jsonReconstructured.Add("streamingData", ExtractStreamingData(streamingDataToken));
            if (jsonObject.TryGetValue("playerAds", out JToken playerAdsToken))
                jsonReconstructured.Add("playerAdsWIP", playerAdsToken);//TODO: Check if there is usable info to extract else don't.
            if (jsonObject.TryGetValue("playbackTracking", out JToken playbackTrackingToken))
                jsonReconstructured.Add("playbackTrackingWIP", playbackTrackingToken);//TODO: Check if there is usable info to extract else don't.
            if (jsonObject.TryGetValue("videoDetails", out JToken videoDetailsToken))
                jsonReconstructured.Add("videoDetails", videoDetailsToken);
            if (jsonObject.TryGetValue("playerConfig", out JToken playerConfigToken))
                jsonReconstructured.Add("playerConfigWIP", playerConfigToken);//TODO: Need to extract buttons and commands.
            if (jsonObject.TryGetValue("storyboards", out JToken storyboardsToken))
                jsonReconstructured.Add("storyboards", storyboardsToken);
            if (jsonObject.TryGetValue("microformat", out JToken microformatToken))
                jsonReconstructured.Add("microformat", ExtractMicroformat(microformatToken));
            if (jsonObject.TryGetValue("trackingParams", out JToken trackingToken))
                jsonReconstructured.Add("trackingParams", trackingToken);
            if (jsonObject.TryGetValue("items", out JToken itemsToken))
                jsonReconstructured.Add("guide", ExtractGuide(itemsToken));
            if (jsonObject.TryGetValue("attestation", out JToken attestationToken))
                jsonReconstructured.Add("attestationWIP", attestationToken);//TODO: Need to reverse engineer the attestation/botguard!
            if (jsonObject.TryGetValue("messages", out JToken messagesToken))
                jsonReconstructured.Add("messages", ExtractMessages(messagesToken));
            if (jsonObject.TryGetValue("endscreen", out JToken endscreenToken))
                jsonReconstructured.Add("endScreen", ExtractEndscreen(endscreenToken));
            if (jsonObject.TryGetValue("adPlacements", out JToken adPlacementsToken))
                jsonReconstructured.Add("adPlacementsWIP", ExtractAdPlacements(adPlacementsToken as JArray));
            if (jsonObject.ContainsKey("currentVideoEndpoint"))
                jsonReconstructured.Add("endpoint", ExtractEndpoint(jsonObject));
            if (jsonObject.ContainsKey("onResponseReceivedEndpoints"))
                jsonReconstructured.Add("endpoints", ExtractEndpoint(jsonObject));
            if (jsonObject.TryGetValue("playerOverlays", out JToken playerOverlaysToken))
                jsonReconstructured.Add("playerOverlays", ExtractPlayerOverlays(playerOverlaysToken));
            if (jsonObject.TryGetValue("frameworkUpdates", out JToken frameworkUpdatesToken))
                jsonReconstructured.Add("frameworkUpdatesWIP", frameworkUpdatesToken);//TODO: Check if there is usable info to extract else don't.
            if (jsonObject.TryGetValue("webWatchNextResponseExtensionData", out JToken webWatchNextResponseDataToken))
                jsonReconstructured.Add("webWatchNextResponseExtensionDataWIP", webWatchNextResponseDataToken);//TODO: Check if there is usable info to extract else don't.
            return jsonReconstructured;
        }
        // Privates and some internal extractors.
        private static JObject ExtractRenderer(JObject rendererToken)
        {
            JObject newRenderer = new JObject();
            foreach (JProperty rendererProperty in rendererToken.Properties())
            {
                newRenderer.Add("kind", rendererProperty.Name);
                //newRenderer.Merge(rendererProperty.Value.ExtractProperties());
            }
            return newRenderer.Count != 0 ? newRenderer : null;
        }
        private static JToken ExtractGuide(JToken items)
        {
            if (items == null)
                return null;
            JArray guideSections = new JArray();
            foreach (JToken guideRenderer in (JArray)items)
            {
                JObject newGuideRenderer = new JObject();

                foreach (JProperty guideRendererProperty in (guideRenderer as JObject).Properties())
                {
                    newGuideRenderer.Add("kind", guideRendererProperty.Name);
                    //newGuideRenderer.Merge(guideRendererProperty.Value.ExtractProperties());
                }

                //if (guideRenderer.TryGetTokenIfContains(new string[] { "guideSectionRenderer", "guideSubscriptionsSectionRenderer" }, out JToken sectionRenderer, out string keyResult))
                //{
                //    newGuideRenderer.Add("trackingParams", sectionRenderer["trackingParams"]);
                //    newGuideRenderer.Add("title", sectionRenderer.TryGetToken("formattedTitle", out JToken formattedTitleToken) ? formattedTitleToken["simpleText"] : null);
                //    if (keyResult == "guideSectionRenderer")
                //        newGuideRenderer.Add("kind", GuideRenderers.SectionRenderer.ToString());
                //    if (keyResult == "guideSubscriptionsSectionRenderer")
                //    {
                //        newGuideRenderer.Add("kind", GuideRenderers.SubscriptionsSelectionRenderer.ToString());
                //        newGuideRenderer.Add("sort", sectionRenderer["sort"]);
                //        newGuideRenderer.Add("handlerDatas", sectionRenderer["handlerDatas"]);
                //    }
                //    if (sectionRenderer.TryGetToken("items", out JToken itemsToken))
                //    {
                //        JArray newItems = new JArray();
                //        foreach (JToken item in (JArray)itemsToken)
                //            newItems.Add(ExtractEntry(item));
                //        newGuideRenderer.Add("items", newItems);
                //    }
                //}
                guideSections.Add(newGuideRenderer);
            }
            return guideSections;
        }
        //private static JObject ExtractEntry(JToken entry)
        //{
        //    if (entry == null)
        //        return null;
        //    JObject newEntry = new JObject();
        //    if (entry.TryGetToken("guideEntryRenderer", out JToken entryRenderer))
        //    {
        //        newEntry = ExtractRenderer(entryRenderer.ToObject<JObject>());
        //        //newEntry.Add("endpoint", ExtractEndpoint(entryRenderer));
        //        //newEntry.Add("thumbnails", ExtractThumbnail(entryRenderer));
        //        //newEntry.Add("badges", entryRenderer["badges"]);
        //        //newEntry.Add("icon", entryRenderer.TryGetToken("icon", out JToken iconToken) ? iconToken["iconType"] : null);
        //        //newEntry.Add("trackingParams", entryRenderer["trackingParams"]);
        //        //newEntry.Add("title", entryRenderer.TryGetToken("formattedTitle", out JToken formattedTitleToken) ? formattedTitleToken["simpleText"] : null);
        //        //newEntry.Add("label", ExtractFromAccessibility(entryRenderer, "accessibility"));
        //        //newEntry.Add("isPrimary", entryRenderer["isPrimary"]);
        //        //if (entryRenderer.TryGetToken("entryData", out JToken entryData))
        //        //    newEntry.Add("entryData", entryData.TryGetToken("guideEntryData", out JToken guideEntryData) ? guideEntryData["guideEntryId"] : null);
        //        //newEntry.Add("presentationStyle", entryRenderer["presentationStyle"]);
        //    }
        //    if (entry.TryGetToken("guideCollapsibleSectionEntryRenderer", out JToken collapsibleSectionEntry))
        //    {
        //        newEntry = ExtractRenderer(collapsibleSectionEntry.ToObject<JObject>());
        //        //newEntry.Add("kind", GuideRenderers.CollapsibleSectionEntryRenderer.ToString());
        //        //if (collapsibleSectionEntry.TryGetToken("headerEntry", out JToken headerEntry))
        //        //    newEntry.Add("headerEntry", ExtractEntry(headerEntry));
        //        //newEntry.Add("expanderIcon", collapsibleSectionEntry.TryGetToken("expanderIcon", out JToken expanderIconToken) ? expanderIconToken["iconType"] : null);
        //        //newEntry.Add("collapserIcon", collapsibleSectionEntry.TryGetToken("collapserIcon", out JToken collapserIconToken) ? collapserIconToken["iconType"] : null);
        //        //if (collapsibleSectionEntry.TryGetToken("sectionItems", out JToken sectionItems))
        //        //{
        //        //    JArray newSectionItems = new JArray();
        //        //    foreach (JToken sectionItem in (JArray)sectionItems)
        //        //        newSectionItems.Add(ExtractEntry(sectionItem));
        //        //    newEntry.Add("sectionItems", newSectionItems);
        //        //}
        //        //newEntry.Add("handlerDatas", collapsibleSectionEntry["handlerDatas"]);
        //    }
        //    if (entry.TryGetToken("guideCollapsibleEntryRenderer", out JToken collapsibleEntryRenderer))
        //    {
        //        newEntry = ExtractRenderer(collapsibleEntryRenderer.ToObject<JObject>());
        //        //newEntry.Add("kind", GuideRenderers.CollapsibleEntryRenderer.ToString());
        //        //if (collapsibleEntryRenderer.TryGetToken("expanderItem", out JToken expanderItem))
        //        //    newEntry.Add("expanderItem", ExtractEntry(expanderItem));
        //        //if (collapsibleEntryRenderer.TryGetToken("expandableItems", out JToken expandableItems))
        //        //{
        //        //    JArray newExpandableItems = new JArray();
        //        //    foreach (JToken expandableItem in (JArray)expandableItems)
        //        //        newExpandableItems.Add(ExtractEntry(expandableItem));
        //        //    newEntry.Add("expandableItems", newExpandableItems);
        //        //}
        //        //if (collapsibleEntryRenderer.TryGetToken("collapserItem", out JToken collapserItem))
        //        //    newEntry.Add("collapserItem", ExtractEntry(collapserItem));
        //    }
        //    return newEntry;
        //}
        //public enum GuideRenderers
        //{
        //    SectionRenderer,
        //    SubscriptionsSelectionRenderer,
        //    EntryRenderer,
        //    CollapsibleSectionEntryRenderer,
        //    CollapsibleEntryRenderer
        //}
        private static JToken ExtractPlayerOverlays(JToken playerOverlays)
        {
            return playerOverlays == null ? null : (JToken)ExtractRenderer(playerOverlays.ToObject<JObject>());
        }
        //private static JToken ExtractAutoplay(JToken autoplay)
        //{
        //    if (autoplay == null)
        //        return null;
        //    JObject newAutoplay = new JObject();
        //    if (autoplay.TryGetToken("playerOverlayAutoplayRenderer", out JToken autoplayRenderer))
        //    {
        //        newAutoplay = ExtractRenderer(autoplayRenderer.ToObject<JObject>()); ;
        //        //newAutoplay.Add("title", autoplayRenderer.TryGetToken("title", out JToken autoplayTitleToken) ? autoplayTitleToken["simpleText"] : null);
        //        //newAutoplay.Add("videoTitleMetadata", new JObject()
        //        //{
        //        //    { "videoTitle", ExtractFromAccessibility(autoplayRenderer["videoTitle"], "accessibility") },
        //        //    { "shortVideoTitle", autoplayRenderer.TryGetToken("videoTitle", out JToken shortVideoTitleToken) ? shortVideoTitleToken["simpleText"]: null }
        //        //});
        //        //newAutoplay.Add("byline", ExtractRuns(autoplayRenderer["byline"]));
        //        //newAutoplay.Add("cancelText", autoplayRenderer.TryGetToken("cancelText", out JToken cancelTextToken) ? cancelTextToken["simpleText"] : null);
        //        //newAutoplay.Add("pauseText", autoplayRenderer.TryGetToken("pauseText", out JToken pauseTextToken) ? pauseTextToken["simpleText"] : null);
        //        //newAutoplay.Add("backgrounds", autoplayRenderer.TryGetToken("background", out JToken backgroundToken) ? backgroundToken["thumbnails"] : null);
        //        //newAutoplay.Add("countDownSecs", autoplayRenderer["countDownSecs"]);
        //        //newAutoplay.Add("nextButton", ExtractButton(autoplayRenderer["nextButton"]));
        //        //newAutoplay.Add("trackingParams", autoplayRenderer["trackingParams"]);
        //        //newAutoplay.Add("preferImmediateRedirect", autoplayRenderer["preferImmediateRedirect"]);
        //        //newAutoplay.Add("videoId", autoplayRenderer["videoId"]);
        //        //newAutoplay.Add("publishedTimeText", autoplayRenderer.TryGetToken("publishedTimeText", out JToken publishedTimeTextToken) ? publishedTimeTextToken["simpleText"] : null);
        //        //newAutoplay.Add("webShowNewAutonavCountdown", autoplayRenderer["webShowNewAutonavCountdown"]);
        //        //newAutoplay.Add("webShowBigThumbnailEndscreen", autoplayRenderer["webShowBigThumbnailEndscreen"]);
        //        //newAutoplay.Add("shortViewCountText", autoplayRenderer.TryGetToken("shortViewCountText", out JToken shortViewCountTextToken) ? shortViewCountTextToken["simpleText"] : null);
        //    }
        //    return newAutoplay;
        //}
        private static JArray ExtractAdPlacements(JArray adPlacements)// TODO: Needs to be implemented! First check wich data can be used.
        {
            if (adPlacements == null)
                return null;
            //JArray adPlacementsArray = new JArray();
            //foreach (JToken adPlacement in adPlacements)
            //{
            //    if (adPlacement.TryGetToken("adPlacementRenderer", out JToken adRendererToken))
            //    {

            //    }
            //}
            return adPlacements;
        }
        private static JObject ExtractEndscreen(JToken endscreen)
        {
            if (endscreen == null)
                return null;
            JObject rebuildedEndscreen = new JObject();
            if (endscreen.TryGetToken("endscreenRenderer", out JToken endscreenRendererToken))
            {
                rebuildedEndscreen = ExtractRenderer(endscreenRendererToken.ToObject<JObject>());
                //if (endscreenRendererToken.TryGetToken("elements", out JToken elementsToken))
                //{
                //    JArray elements = new JArray();
                //    foreach (JToken element in (JArray)elementsToken)
                //    {
                //        JObject newElement = new JObject();
                //        if (element.TryGetToken("endscreenElementRenderer", out JToken elementRenderer))
                //        {
                //            newElement = ExtractRenderer(elementRenderer.ToObject<JObject>());
                //            //newElement.Add("style", elementRenderer["style"]);
                //            //newElement.Add("images", elementRenderer.TryGetToken("image", out JToken imageToken) ? imageToken["thumbnails"] : null);
                //            //newElement.Add("icon", elementRenderer.TryGetToken("icon", out JToken iconToken) ? iconToken["thumbnails"] : null);
                //            //if (elementRenderer.TryGetTokenIfContains(new string[] { "videoDuration", "playlistLength" }, out JToken rendererLengthDuration, out string resultKey))
                //            //{
                //            //    switch (resultKey)
                //            //    {
                //            //        case "videoDuration":
                //            //            newElement.Add("videoDuration", rendererLengthDuration["simpleText"]);
                //            //            break;
                //            //        case "playlistLength":
                //            //            newElement.Add("playlistLength", rendererLengthDuration["simpleText"]);
                //            //            break;
                //            //        default:
                //            //            break;
                //            //    }
                //            //}
                //            //newElement.Add("leftPos", elementRenderer["left"]);
                //            //newElement.Add("width", elementRenderer["width"]);
                //            //newElement.Add("topPos", elementRenderer["top"]);
                //            //newElement.Add("aspectRatio", elementRenderer["aspectRatio"]);
                //            //newElement.Add("startMs", elementRenderer["startMs"]);
                //            //newElement.Add("endMs", elementRenderer["endMs"]);
                //            //newElement.Add("title", ExtractFromAccessibility(elementRenderer["title"], "accessibility"));
                //            //newElement.Add("metadata", elementRenderer.TryGetToken("metadata", out JToken metadataToken) ? metadataToken["simpleText"] : null);
                //            //newElement.Add("callToAction", elementRenderer.TryGetToken("callToAction", out JToken callToActionToken) ? callToActionToken["simpleText"] : null);
                //            //newElement.Add("dismiss", elementRenderer.TryGetToken("dismiss", out JToken dismissToken) ? dismissToken["simpleText"] : null);
                //            //newElement.Add("endpoint", ExtractEndpoint(elementRenderer));
                //            //newElement.Add("hovercardButton", ExtractButton(elementRenderer["hovercardButton"]));
                //            //newElement.Add("trackingParams", elementRenderer["trackingParams"]);
                //            //newElement.Add("isSubscribe", elementRenderer["isSubscribe"]);
                //            //newElement.Add("id", elementRenderer["id"]);
                //        }
                //        elements.Add(newElement);
                //    }
                //    rebuildedEndscreen.Add("elements", elements);
                //}
                //rebuildedEndscreen.Add("startMs", endscreenRendererToken["startMs"]);
                //rebuildedEndscreen.Add("trackingParams", endscreenRendererToken["trackingParams"]);
            }
            if (endscreen.TryGetToken("watchNextEndScreenRenderer", out JToken watchNextEndScreenRendererToken))
            {
                rebuildedEndscreen = ExtractRenderer(watchNextEndScreenRendererToken.ToObject<JObject>());
                //rebuildedEndscreen.Add("title", watchNextEndScreenRendererToken.TryGetToken("title", out JToken titleToken) ? titleToken["simpleText"] : null);
                //rebuildedEndscreen.Add("trackingParams", watchNextEndScreenRendererToken["trackingParams"]);
                //if (watchNextEndScreenRendererToken.TryGetToken("results", out JToken resultsToken))
                //{
                //    JArray results = new JArray();
                //    foreach (JToken result in (JArray)resultsToken)
                //    {
                //        JObject newResult = new JObject();
                //        if (result.TryGetToken("endScreenVideoRenderer", out JToken endScreenRenderer))
                //        {
                //            newResult.Add("videoId", endScreenRenderer["videoId"]);
                //            newResult.Add("thumbnails", ExtractThumbnail(endScreenRenderer));
                //            newResult.Add("title", endScreenRenderer.TryGetToken("title", out JToken endScreenItemTitleToken) ? endScreenItemTitleToken["simpleText"] : null);
                //            newResult.Add("label", ExtractFromAccessibility(endScreenRenderer["title"], "accessibility"));
                //            newResult.Add("shortBylineText", ExtractRuns(endScreenRenderer["shortBylineText"]));
                //            newResult.Add("lengthMetadata", new JObject()
                //            {
                //                { "lengthText", ExtractFromAccessibility(endScreenRenderer["lengthText"], "accessibility") },
                //                { "shortLengthText", endScreenRenderer.TryGetToken("lengthText", out JToken shortLengthTextToken) ? shortLengthTextToken["simpleText"] : null },
                //                { "lengthInSeconds", endScreenRenderer["lengthInSeconds"] }
                //            });
                //            newResult.Add("endpoint", ExtractEndpoint(endScreenRenderer));
                //            newResult.Add("trackingParams", endScreenRenderer["trackingParams"]);
                //            newResult.Add("shortViewCount", endScreenRenderer.TryGetToken("shortViewCountText", out JToken shortViewCountToken) ? shortViewCountToken["simpleText"] : null);
                //            newResult.Add("publishedTimeText", endScreenRenderer.TryGetToken("publishedTimeText", out JToken publishedTimeTextToken) ? publishedTimeTextToken["simpleText"] : null);
                //            newResult.Add("thumbnailOverlays", ExtractThumbnailOverlays(endScreenRenderer["thumbnailOverlays"]));
                //        }
                //        results.Add(newResult);
                //    }
                //    rebuildedEndscreen.Add("results", results);
                //}
            }
            return rebuildedEndscreen;
        }
        private static JArray ExtractMessages(JToken messages)
        {
            if (messages == null)
                return null;
            JArray rebuildedMessages = new JArray();
            foreach (JToken message in messages)
            {
                JObject rebuildRenderer = ExtractRenderer(message.ToObject<JObject>());
                //if (message.TryGetToken("mealbarPromoRenderer", out JToken promoRenderer))
                //{
                //    rebuildRenderer = ExtractRenderer(promoRenderer.ToObject<JObject>());
                //    //if (promoRenderer.TryGetToken("messageTexts", out JToken textMessages))
                //    //{
                //    //    JArray textMsgArray = new JArray();
                //    //    foreach (JToken textMessage in (JArray)textMessages)
                //    //        textMsgArray.Add(ExtractRuns(textMessage));
                //    //    rebuildRenderer.Add("messageTexts", textMsgArray);
                //    //}
                //    //rebuildRenderer.Add("actionButton", ExtractButton(promoRenderer["actionButton"]));
                //    //rebuildRenderer.Add("dismissButton", ExtractButton(promoRenderer["dismissButton"]));
                //    //rebuildRenderer.Add("triggerCondition", promoRenderer["triggerCondition"]);
                //    //rebuildRenderer.Add("style", promoRenderer["style"]);
                //    //rebuildRenderer.Add("trackingParams", promoRenderer["trackingParams"]);
                //    //if (promoRenderer.TryGetToken("impressionEndpoints", out JToken endpoints))
                //    //{
                //    //    JArray newEndpoints = new JArray();
                //    //    foreach (JToken endpoint in endpoints)
                //    //        newEndpoints.Add(ExtractEndpoint(endpoint));
                //    //    rebuildRenderer.Add("endpoints", newEndpoints);
                //    //}
                //    //rebuildRenderer.Add("isVisible", promoRenderer["isVisible"]);
                //    //rebuildRenderer.Add("messageTitle", ExtractRuns(promoRenderer["messageTitle"]));
                //}
                rebuildedMessages.Add(rebuildRenderer);
            }
            return rebuildedMessages;
        }
        private static JObject ExtractMicroformat(JToken microformat)
        {
            if (microformat == null)
                return null;
            JObject rebuildededMicroformat = new JObject();
            //if (microformat.TryGetToken("playerMicroformatRenderer", out JToken playerMicroformat))
            //{
            //    rebuildededMicroformat = playerMicroformat.ExtractProperties();
            //}
            return rebuildededMicroformat;
        }
        private static JObject ExtractStreamingData(JToken streamingDataToken)
        {
            if (streamingDataToken == null)
                return null;
            JObject rebuildedStreamingData = new JObject
            {
                { "expiresInSeconds", streamingDataToken["expiresInSeconds"] }
            };
            if (double.TryParse(streamingDataToken["expiresInSeconds"].ToString(), out double doubleOutput))
                rebuildedStreamingData.Add("expires", DateTime.Now.AddSeconds(doubleOutput));// TODO: need to get the datetime from the request and add the seconds to that time thingie.
            CipherManager cipherManager = new CipherManager(playerProperties);
            if (streamingDataToken.TryGetToken("formats", out JToken formatsToken))
            {
                JArray formatArray = new JArray();
                foreach (JObject format in (JArray)formatsToken)
                    formatArray.Add(ExtractSignature(format, cipherManager));
                rebuildedStreamingData.Add("mixxedFormats", formatArray);
            }
            if (streamingDataToken.TryGetToken("adaptiveFormats", out JToken adaptiveFormatsToken))
            {
                JArray adaptiveVideoFormatArray = new JArray();
                JArray adaptiveAudioFormatArray = new JArray();
                foreach (JObject format in (JArray)adaptiveFormatsToken)
                {
                    if (format["mimeType"].ToString().Contains("video/"))
                        adaptiveVideoFormatArray.Add(ExtractSignature(format, cipherManager));
                    else if (format["mimeType"].ToString().Contains("audio/"))
                        adaptiveAudioFormatArray.Add(ExtractSignature(format, cipherManager));
                }
                rebuildedStreamingData.Add("videoFormats", adaptiveVideoFormatArray);
                rebuildedStreamingData.Add("audioFormats", adaptiveAudioFormatArray);
            }
            return rebuildedStreamingData;
        }
        private static JObject ExtractSignature(JObject format, CipherManager cipherManager)
        {
            JObject newFormat = format;
            if (format.TryGetValue("signatureCipher", out JToken signatureCipherToken))
                newFormat.Add("url", cipherManager.DecipherAndBuildUrl(WebUtility.UrlDecode(signatureCipherToken.ToString())));
            return newFormat;
        }
        private static JObject ExtractTopbar(JToken topbarToken)
        {
            if (topbarToken == null)
                return null;
            JObject topbar = new JObject();
            if (topbarToken.TryGetToken("desktopTopbarRenderer", out JToken desktopTopbarRenderer))
            {
                topbar.Add("trackingParams", desktopTopbarRenderer["trackingParams"]);
                topbar.Add("countryCode", desktopTopbarRenderer["countryCode"]);
                if (desktopTopbarRenderer.TryGetToken("logo", out JToken logoToken))
                {
                    JObject logoTopbar = new JObject();
                    if (logoToken.TryGetToken("topbarLogoRenderer", out JToken topbarLogoRenderer))
                    {
                        logoTopbar.Add("iconImage", topbarLogoRenderer.TryGetToken("iconImage", out JToken iconImageToken) ? iconImageToken["iconType"] : null);
                        logoTopbar.Add("tooltip", ExtractRuns(topbarLogoRenderer["tooltipText"]));
                        logoTopbar.Add("endpoint", ExtractEndpoint(topbarLogoRenderer));
                        logoTopbar.Add("trackingParams", topbarLogoRenderer["trackingParams"]);
                    }
                    topbar.Add("logo", logoTopbar);
                }
                if (desktopTopbarRenderer.TryGetToken("searchbox", out JToken searchBoxToken))
                {
                    JObject searchBoxTopbar = new JObject();
                    if (searchBoxToken.TryGetToken("fusionSearchboxRenderer", out JToken fusionSearchBoxRenderer))
                    {
                        searchBoxTopbar.Add("icon", fusionSearchBoxRenderer.TryGetToken("icon", out JToken iconToken) ? iconToken["iconType"] : null);
                        searchBoxTopbar.Add("placeholderText", ExtractRuns(fusionSearchBoxRenderer["placeholderText"]));
                        if (fusionSearchBoxRenderer.TryGetToken("config", out JToken searchboxConfigToken))
                        {
                            if (searchboxConfigToken.TryGetToken("webSearchboxConfig", out JToken searchboxWebConfig))
                                searchBoxTopbar.Add("config", searchboxWebConfig);
                        }
                        searchBoxTopbar.Add("trackingParams", fusionSearchBoxRenderer["trackingParams"]);
                        searchBoxTopbar.Add("endpoint", ExtractEndpoint(fusionSearchBoxRenderer));
                    }
                    topbar.Add("searchbox", searchBoxTopbar);
                }
                if (desktopTopbarRenderer.TryGetToken("topbarButtons", out JToken topbarButtonsToken))
                {
                    topbar.Add("topbarButtons", (JArray)ExtractButton(topbarButtonsToken));
                }
                if (desktopTopbarRenderer.TryGetToken("hotkeyDialog", out JToken hotkeyDialogToken))
                {
                    if (hotkeyDialogToken.TryGetToken("hotkeyDialogRenderer", out JToken hotkeyDialogRenderer))
                    {
                        topbar.Add("hotkeyDialog", new JObject()
                        {
                            new JProperty("title", ExtractRuns(hotkeyDialogRenderer["title"])),
                            new JProperty("sections", hotkeyDialogRenderer["sections"]),// not fully extracted sections, maybe not be used in final application.
                            new JProperty("dismissButton", ExtractButton(hotkeyDialogRenderer["dismissButton"])),
                            new JProperty("trackingParams", hotkeyDialogRenderer["trackingParams"])
                        });
                    }
                }
                // Buttons
                JObject topbarButtons = new JObject();
                if (desktopTopbarRenderer.TryGetToken("backButton", out JToken backButtonToken))
                    topbarButtons.Add("backButton", ExtractButton(backButtonToken));
                if (desktopTopbarRenderer.TryGetToken("forwardButton", out JToken forwardButtonToken))
                    topbarButtons.Add("forwardButton", ExtractButton(forwardButtonToken));
                if (desktopTopbarRenderer.TryGetToken("a11ySkipNavigationButton", out JToken skipNavigationButtonToken))
                    topbarButtons.Add("skipNavigationButton", ExtractButton(skipNavigationButtonToken));
                if (desktopTopbarRenderer.TryGetToken("voiceSearchButton", out JToken voiceSearchButtonToken))
                    topbarButtons.Add("voiceSearchButton", ExtractButton(voiceSearchButtonToken));
                topbar.Add("topbarButton", topbarButtons);
            }
            return topbar;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        private static JObject ExtractContents(JToken contentsToken) //TODO: Need to expand the extraction and properties.
        {
            if (contentsToken == null)
                return null;
            JObject contents = new JObject();
            JArray oldContentsArray = new JArray();
            JArray restructuredContentsArray = new JArray();
            switch (contentsToken.Type)
            {
                case JTokenType.Object:
                    if (contentsToken.TryGetTokenIfContains(new string[] { "twoColumnBrowseResultsRenderer", "twoColumnWatchNextResults" }, out JToken columnBrowseResultsToken, out string keyResult))
                    {
                        if (columnBrowseResultsToken.TryGetToken("tabs", out JToken tabsToken))
                        {
                            foreach (JToken tab in (JArray)tabsToken["tabs"]) // Itterate through the tabs array.
                            {
                                JToken tabRenderer = tab["tabRenderer"];
                                JToken content = tabRenderer["content"];
                                JToken contentGridRenderer = content["richGridRenderer"];
                                oldContentsArray.Merge((JArray)contentGridRenderer["contents"]); // Merge the arrays so we can restructure all the item renderers once.
                            }
                        }
                        if (columnBrowseResultsToken.TryGetToken("results", out JToken contentResultsToken))
                        {
                            if (contentResultsToken.TryGetToken("results", out JToken resultsToken))
                            {
                                JObject videoMetadata = new JObject();
                                foreach (var resultItem in (JArray)resultsToken["contents"])
                                {
                                    if (resultItem.TryGetTokenIfContains(new string[] { "videoPrimaryInfoRenderer", "videoSecondaryInfoRenderer", "merchandiseShelfRenderer", "itemSectionRenderer" }, out JToken infoRenderer, out string resultKey))
                                    {
                                        switch (resultKey)
                                        {
                                            case "videoPrimaryInfoRenderer":// Main video info.
                                                JObject videoPrimary = new JObject
                                                {
                                                    { "title", ExtractRuns(infoRenderer["title"]) }
                                                };
                                                if (infoRenderer.TryGetToken("viewCount", out JToken viewCountToken))
                                                {
                                                    JToken viewCountRenderer = viewCountToken["videoViewCountRenderer"];
                                                    videoPrimary.Add("viewCount", new JObject()
                                                    {
                                                        new JProperty("viewCount", viewCountRenderer.TryGetToken("viewCount", out JToken viewCount) ? viewCount["simpleText"] : null),
                                                        new JProperty("shortViewCount", viewCountRenderer.TryGetToken("shortViewCount", out JToken shortViewCount) ? shortViewCount["simpleText"] : null)
                                                    });
                                                }
                                                videoPrimary.Add("actionMenu", ExtractActionMenu(infoRenderer["videoActions"]));
                                                videoPrimary.Add("trackingParams", infoRenderer["trackingParams"]);
                                                videoPrimary.Add("sentiment", infoRenderer.TryGetToken("sentimentBar", out JToken sentimentToken) ? sentimentToken["sentimentBarRenderer"] : null);
                                                videoPrimary.Add("superTitles", ExtractRuns(infoRenderer["superTitleLink"]));
                                                videoPrimary.Add("dateText", infoRenderer.TryGetToken("dateText", out JToken dateTextToken) ? dateTextToken["simpleText"] : null);
                                                videoMetadata.Add("videoPrimaryInfo", videoPrimary);
                                                break;
                                            case "videoSecondaryInfoRenderer":// Video owner metadata
                                                JObject videoSecondary = new JObject();
                                                if (infoRenderer.TryGetToken("owner", out JToken ownerMetadataToken))
                                                {
                                                    if (ownerMetadataToken.TryGetToken("videoOwnerRenderer", out JToken ownerRenderer))
                                                    {
                                                        JObject ownerMetadata = new JObject
                                                        {
                                                            { "thumbnails", ExtractThumbnail(ownerRenderer) },
                                                            { "title", ExtractRuns(ownerRenderer["title"]) },
                                                            { "subscriptionButton", ownerRenderer["subscriptionButton"] },
                                                            { "endpoint", ExtractEndpoint(ownerRenderer) },
                                                            { "subscriberCountText", ownerRenderer.TryGetToken("subscriberCountText", out JToken subscriberCountTextToken) ? subscriberCountTextToken["simpleText"] : null },
                                                            { "trackingParams", ownerRenderer["trackingParams"] },
                                                            { "badges", ExtractBadge(ownerRenderer["badges"]) }
                                                        };
                                                        videoSecondary.Add("ownerMetadata", ownerMetadata);
                                                    }
                                                }
                                                videoSecondary.Add("description", ExtractRuns(infoRenderer["description"]));
                                                videoSecondary.Add("subscribeButton", ExtractButton(infoRenderer["subscribeButton"]));
                                                videoSecondary.Add("metadata", ExtractMetadataContainers(infoRenderer["metadataRowContainer"]));
                                                videoSecondary.Add("showMoreText", ExtractRuns(infoRenderer["showMoreText"]));
                                                videoSecondary.Add("showLessText", ExtractRuns(infoRenderer["showLessText"]));
                                                videoSecondary.Add("trackingParams", infoRenderer["trackingParams"]);
                                                videoSecondary.Add("defaultExpanded", infoRenderer["defaultExpanded"]);
                                                videoSecondary.Add("descriptionCollapsedLines", infoRenderer["descriptionCollapsedLines"]);
                                                videoMetadata.Add("videoSecondaryInfo", videoSecondary);
                                                break;
                                            case "merchandiseShelfRenderer":
                                                JObject merchRenderer = new JObject
                                                {
                                                    { "title", infoRenderer["title"] },
                                                    { "trackingParams", infoRenderer["trackingParams"] },
                                                    { "showText", infoRenderer["showText"] },
                                                    { "hideText", infoRenderer["hideText"] },
                                                    { "informationButton", ExtractButton(infoRenderer["informationButton"]) }
                                                };
                                                if (infoRenderer.TryGetToken("items", out JToken merchItems))
                                                {
                                                    JArray merchandiseItems = new JArray();
                                                    foreach (JToken merchItem in (JArray)merchItems)
                                                    {
                                                        JObject itemRenderer = new JObject();
                                                        if (merchItem.TryGetToken("merchandiseItemRenderer", out JToken merchandiseItemRenderer))
                                                        {
                                                            itemRenderer.Add("title", merchandiseItemRenderer["title"]);
                                                            itemRenderer.Add("description", merchandiseItemRenderer["description"]);
                                                            itemRenderer.Add("thumbnails", ExtractThumbnail(merchandiseItemRenderer));
                                                            itemRenderer.Add("price", merchandiseItemRenderer["price"]);
                                                            itemRenderer.Add("vendorName", merchandiseItemRenderer["vendorName"]);
                                                            itemRenderer.Add("trackingParams", merchandiseItemRenderer["trackingParams"]);
                                                            itemRenderer.Add("buttonText", merchandiseItemRenderer["buttonText"]);
                                                            itemRenderer.Add("buttonCommand", ExtractCommand(merchandiseItemRenderer));
                                                            itemRenderer.Add("accessibilityTitle", merchandiseItemRenderer["accessibilityTitle"]);
                                                            itemRenderer.Add("fromVendorText", merchandiseItemRenderer["fromVendorText"]);
                                                            itemRenderer.Add("additionalFeesText", merchandiseItemRenderer["additionalFeesText"]);
                                                            itemRenderer.Add("regionFormat", merchandiseItemRenderer["regionFormat"]);
                                                        }
                                                        merchandiseItems.Add(itemRenderer);
                                                    }
                                                    merchRenderer.Add("merchandiseItems", merchandiseItems);
                                                }
                                                videoMetadata.Add("merchandiseInfo", merchRenderer);
                                                break;
                                            case "itemSectionRenderer":// Reactions token
                                                JObject reactionMetadata = new JObject();
                                                if (infoRenderer.TryGetToken("continuations", out JToken continuationsToken))
                                                {
                                                    JArray continuations = new JArray();
                                                    foreach (JToken continuationItem in (JArray)continuationsToken)
                                                    {
                                                        if (continuationItem.TryGetToken("nextContinuationData", out JToken nextContinuationToken))
                                                            continuations.Add(nextContinuationToken);
                                                    }
                                                    reactionMetadata.Add("continuations", continuations);
                                                }
                                                reactionMetadata.Add("trackingParams", infoRenderer["trackingParams"]);
                                                reactionMetadata.Add("sectionIdentifier", infoRenderer["sectionIdentifier"]);
                                                videoMetadata.Add("reactionMetadata", reactionMetadata);
                                                break;
                                            default:
                                                Trace.WriteLine($"Could not extract renderer. Renderer: {keyResult}");
                                                break;
                                        }
                                    }
                                }
                                videoMetadata.Add("trackingParams", resultsToken["trackingParams"]);
                                return videoMetadata;
                            }
                        }
                    }
                    break;
                case JTokenType.Array:
                    foreach (var item in contentsToken)
                    {
                        if (item.TryGetToken("appendContinuationItemsAction", out JToken continuationResultsToken))
                            oldContentsArray.Merge((JArray)continuationResultsToken["continuationItems"]);
                    }
                    break;
                default:
                    Trace.WriteLine("No contents found!");
                    return null;
            }
            // Restructure the renderers
            foreach (JObject itemRenderer in oldContentsArray)
            {
                if (itemRenderer.TryGetValue("continuationItemRenderer", out JToken continuationItemRendererToken))// Checks for continuation token
                {
                    Trace.WriteLine("Found continuation!");
                    contents.Add("continuationData", ExtractContinuationItem(continuationItemRendererToken));
                    continue;
                }

                JObject displayRenderer = new JObject(); // The new display item.
                if (itemRenderer.TryGetTokenIfContains(new string[] { "richSectionRenderer", "richItemRenderer" }, out JToken richItemRendererToken, out string key)) // Checks for item renderers.
                {
                    if (richItemRendererToken.TryGetToken("content", out JToken contentToken))
                    {
                        if (contentToken.TryGetToken("displayAdRenderer", out JToken adRenderer))// Advertisements
                        {
                            displayRenderer.Add("kind", ContentIdentifier.Advertisement.ToString());
                            displayRenderer.Add("trackingParams", adRenderer["trackingParams"]);
                            displayRenderer.Add("layout", adRenderer["layout"]);
                            displayRenderer.Add("title", adRenderer.TryGetToken("titleText", out JToken titleToken) ? titleToken["simpleText"] : null);
                            displayRenderer.Add("thumbnails", ExtractThumbnail(adRenderer["image"]));
                            displayRenderer.Add("bodyText", adRenderer.TryGetToken("bodyText", out JToken bodyTextToken) ? bodyTextToken["simpleText"] : null);
                            displayRenderer.Add("secondaryText", adRenderer.TryGetToken("secondaryText", out JToken secondaryTextToken) ? secondaryTextToken["simpleText"] : null);
                            displayRenderer.Add("badge", ExtractBadge(adRenderer["badge"]));
                            displayRenderer.Add("actionMenu", ExtractActionMenu(adRenderer["menu"]));
                            displayRenderer.Add("ctaButton", ExtractButton(adRenderer["ctaButton"]));
                            displayRenderer.Add("impressionEndpoints", adRenderer["impressionEndpoints"]);
                            displayRenderer.Add("clickCommand", ExtractCommand(adRenderer));
                            displayRenderer.Add("mediaHoverOverlayButton", ExtractButton(adRenderer["mediaHoverOverlay"]));
                            displayRenderer.Add("mediaBadge", ExtractBadge(adRenderer["mediaBadge"]));

                            restructuredContentsArray.Add(displayRenderer);
                            continue;
                        }
                        else if (contentToken.TryGetToken("videoRenderer", out JToken videoRenderer))// Video's
                        {
                            displayRenderer.Add("kind", ContentIdentifier.Video.ToString());
                            displayRenderer.Add("videoId", videoRenderer["videoId"]);
                            displayRenderer.Add("thumbnails", ExtractThumbnail(videoRenderer));
                            displayRenderer.Add("title", ExtractTitleProperty(videoRenderer["title"]));
                            displayRenderer.Add("descriptionSnippet", ExtractRuns(videoRenderer["descriptionSnippet"]));
                            displayRenderer.Add("longByLineText", ExtractRuns(videoRenderer["longBylineText"]));
                            displayRenderer.Add("publishedTimeText", videoRenderer.TryGetToken("publishedTimeText", out JToken publishedTimeToken) ? publishedTimeToken["simpleText"] : null);
                            displayRenderer.Add("videoLength", new JObject()
                            {
                                new JProperty("lengthCount", ExtractFromAccessibility(videoRenderer["lengthText"], "accessibility")),
                                new JProperty("shortLengthCount", videoRenderer.TryGetToken("lengthText", out JToken lengthToken) ? lengthToken["simpleText"] : null)
                            });
                            displayRenderer.Add("videoViewCount", new JObject()
                            {
                                new JProperty("viewCount", videoRenderer.TryGetToken("viewCountText", out JToken viewCountToken) ? viewCountToken["simpleText"] : null),
                                new JProperty("shortViewCount", videoRenderer.TryGetToken("shortViewCountText", out JToken shortViewCountToken) ? shortViewCountToken["simpleText"] : null)
                            });
                            displayRenderer.Add("endpoint", ExtractEndpoint(videoRenderer));
                            displayRenderer.Add("ownerBadges", ExtractBadge(videoRenderer["ownerBadges"]));
                            displayRenderer.Add("ownerText", ExtractRuns(videoRenderer["ownerText"]));
                            displayRenderer.Add("upcomingEventData", videoRenderer["upcomingEventData"]);
                            displayRenderer.Add("shortBylineText", ExtractRuns(videoRenderer["shortBylineText"]));// Idk why there are two of the same property. The longByLineText is the same.
                            displayRenderer.Add("actionMenu", ExtractActionMenu(videoRenderer["menu"]));
                            displayRenderer.Add("channelThumbnail", ExtractChannelThumbnail(videoRenderer["channelThumbnailSupportedRenderers"]));
                            displayRenderer.Add("thumbnailOverlays", ExtractThumbnailOverlays(videoRenderer["thumbnailOverlays"]));
                            displayRenderer.Add("buttons", ExtractButton(videoRenderer["buttons"]));
                            displayRenderer.Add("richThumbnail", ExtractRichThumbnail(videoRenderer["richThumbnail"]));
                            displayRenderer.Add("trackingParams", videoRenderer["trackingParams"]);
                            displayRenderer.Add("showActionMenu", videoRenderer["showActionMenu"]);

                            restructuredContentsArray.Add(displayRenderer);
                            continue;
                        }
                        else if (contentToken.TryGetToken("radioRenderer", out JToken radioRenderer))// Playlists
                        {
                            displayRenderer.Add("kind", ContentIdentifier.Radio.ToString());
                            displayRenderer.Add("playlistId", radioRenderer["playlistId"]);
                            displayRenderer.Add("title", radioRenderer.TryGetToken("title", out JToken titleTextToken) ? titleTextToken["simpleText"] : null);
                            displayRenderer.Add("thumbnails", ExtractThumbnail(radioRenderer));
                            displayRenderer.Add("videoCount", new JObject()
                            {
                                new JProperty("videoCount", ExtractRuns(radioRenderer["videoCountText"])),
                                new JProperty("shortVideoCount", ExtractRuns(radioRenderer["videoCountShortText"]))
                            });
                            displayRenderer.Add("endpoint", ExtractEndpoint(radioRenderer));
                            displayRenderer.Add("trackingParams", radioRenderer["trackingParams"]);
                            displayRenderer.Add("videos", ExtractChildVideo(radioRenderer["videos"]));
                            displayRenderer.Add("thumbnailText", ExtractRuns(radioRenderer["thumbnailText"]));
                            displayRenderer.Add("longByLineText", radioRenderer.TryGetToken("longByLineText", out JToken longByLineToken) ? longByLineToken["simpleText"] : null);
                            displayRenderer.Add("actionMenu", ExtractActionMenu(radioRenderer["menu"]));
                            displayRenderer.Add("thumbnailOverlays", ExtractThumbnailOverlays(radioRenderer["thumbnailOverlays"]));

                            restructuredContentsArray.Add(displayRenderer);
                            continue;
                        }
                        else if (contentToken.TryGetToken("richShelfRenderer", out JToken shelfRenderer))// Shelfs
                        {
                            displayRenderer.Add("kind", ContentIdentifier.Shelf.ToString());
                            displayRenderer.Add("title", ExtractRuns(shelfRenderer["title"]));
                            displayRenderer.Add("contents", ExtractShelfContents((JArray)shelfRenderer["contents"]));
                            displayRenderer.Add("trackingParams", shelfRenderer["trackingParams"]);
                            displayRenderer.Add("showMoreButton", ExtractButton(shelfRenderer["showMoreButton"]));

                            restructuredContentsArray.Add(displayRenderer);
                            continue;
                        }
                        else
                            continue;
                    }
                }
            }
            contents.Add("contentsList", restructuredContentsArray);
            return contents;
        }
        private static JToken ExtractShelfContents(JArray shelfContents)
        {
            if (shelfContents == null)
                return shelfContents;
            JArray shelfContentsRestructured = new JArray();
            foreach (JToken itemRenderer in shelfContents)
            {
                JObject shelfItem = new JObject();
                if (itemRenderer.TryGetToken("richItemRenderer", out JToken richItemRendererToken))
                {
                    if (richItemRendererToken.TryGetToken("content", out JToken contentToken))
                    {
                        if (contentToken.TryGetToken("postRenderer", out JToken postRendererToken))
                        {
                            shelfItem.Add("postId", postRendererToken["postId"]);
                            shelfItem.Add("authorMetadata", new JObject()
                            {
                                new JProperty("text", ExtractRuns(postRendererToken["authorText"])),
                                new JProperty("label", ExtractFromAccessibility(postRendererToken["authorText"], "accessibility")),
                                new JProperty("thumbnails", postRendererToken.TryGetToken("authorThumbnail", out JToken authorThumbnails) ? authorThumbnails["thumbnails"] : null),
                                new JProperty("endpoint", ExtractEndpoint(postRendererToken))
                            });
                            shelfItem.Add("contentText", ExtractRuns(postRendererToken["contentText"]));
                            if (postRendererToken.TryGetToken("backstageAttachment", out JToken backstageAttachment))
                            {
                                if (backstageAttachment.TryGetToken("backstageImageRenderer", out JToken backstageImageRenderer))
                                {
                                    shelfItem.Add("backstageAttachment", new JObject()
                                    {
                                        new JProperty("trackingParams", backstageImageRenderer["trackingParams"]),
                                        new JProperty("images", backstageImageRenderer.TryGetToken("image", out JToken imageBackstage) ? imageBackstage["thumbnails"] : null)
                                    });
                                }
                            }
                            shelfItem.Add("publishedTimeText", ExtractRuns(postRendererToken["publishedTimeText"]));
                            shelfItem.Add("voteMetadata", new JObject()
                            {
                                new JProperty("voteCount", new JObject()
                                {
                                    new JProperty("voteCount", ExtractFromAccessibility(postRendererToken["voteCount"], "accessibility")),
                                    new JProperty("shortVoteCount", postRendererToken.TryGetToken("voteCount", out JToken voteCountShort) ? voteCountShort["simpleText"] : null)
                                }),
                                new JProperty("voteStatus", postRendererToken["voteStatus"])
                            });
                            shelfItem.Add("actionButtons", ExtractButton(postRendererToken["actionButtons"]));
                            shelfItem.Add("actionMenu", ExtractActionMenu(postRendererToken["actionMenu"]));
                            shelfItem.Add("trackingParams", postRendererToken["trackingParams"]);
                            shelfItem.Add("surface", postRendererToken["surface"]);
                            shelfItem.Add("endpoint", ExtractEndpoint(postRendererToken));
                            shelfItem.Add("directives", postRendererToken["loggingDirectives"]);// This will maybe be extracted if more properties are using it.
                        }
                    }
                }
                shelfContentsRestructured.Add(shelfItem);
            }
            return shelfContentsRestructured;
        }
        private static JToken ExtractChildVideo(JToken videos)
        {
            JArray videosArray = new JArray();
            foreach (JToken video in videos)
            {
                JObject videoItem = new JObject();
                if (video.TryGetToken("childVideoRenderer", out JToken childVideo))
                {
                    videoItem.Add("videoId", childVideo["videoId"]);
                    videoItem.Add("title", childVideo.TryGetToken("title", out JToken titleTextToken) ? titleTextToken["simpleText"] : null);
                    videoItem.Add("endpoint", ExtractEndpoint(childVideo));
                    videoItem.Add("videoLength", new JObject()
                    {
                        new JProperty("lengthCount", ExtractFromAccessibility(childVideo["lengthText"], "accessibility")),
                        new JProperty("shortLengthCount", childVideo.TryGetToken("lengthText", out JToken lengthToken) ? lengthToken["simpleText"] : null)
                    });
                }
                videosArray.Add(videoItem);
            }
            return videosArray;
        }
        private static JToken ExtractMetadataContainers(JToken metadata)
        {
            if (metadata == null)
                return null;
            if (metadata.TryGetToken("metadataRowContainerRenderer", out JToken metadataContainerRenderer))
            {
                JArray rows = new JArray();
                JObject rowContainer = new JObject();
                if (metadataContainerRenderer.TryGetToken("rows", out JToken rowsToken))
                {
                    foreach (JToken row in rowsToken)
                    {
                        JObject rowRendererRebuild = new JObject();
                        if (row.TryGetToken("metadataRowHeaderRenderer", out JToken rowHeaderRenderer))
                        {
                            rowRendererRebuild.Add("content", ExtractRuns(rowHeaderRenderer["content"]));
                            rowRendererRebuild.Add("hasDividerLine", rowHeaderRenderer["hasDividerLine"]);
                        }
                        if (row.TryGetToken("metadataRowRenderer", out JToken rowRenderer))
                        {
                            rowRendererRebuild.Add("title", rowRenderer.TryGetToken("title", out JToken rowRendererTitle) ? rowRendererTitle["simpleText"] : null);
                            if (rowRenderer.TryGetToken("contents", out JToken contentsToken))
                            {
                                JArray contentsArray = new JArray();
                                foreach (JToken content in (JArray)contentsToken)
                                    contentsArray.Add(content["simpleText"]);
                                rowRendererRebuild.Add("contents", contentsArray);
                            }
                            rowRendererRebuild.Add("trackingParams", rowRenderer["trackingParams"]);
                            rowRendererRebuild.Add("hasDividerLine", rowRenderer["hasDividerLine"]);
                        }
                        rows.Add(rowRendererRebuild);
                    }
                    rowContainer.Add("rows", rows);
                }
                rowContainer.Add("collapsedItemCount", metadataContainerRenderer["collapsedItemCount"]);
                rowContainer.Add("trackingParams", metadataContainerRenderer["trackingParams"]);
                return rowContainer;
            }
            return null;
        }
        private static JToken ExtractThumbnailOverlays(JToken thumbnailOverlayArray)
        {
            if (thumbnailOverlayArray == null)
                return null;
            JArray thumbnailOverlayButtons = (JArray)ExtractButton(thumbnailOverlayArray);
            JObject thumbnailOverlays = new JObject
            {
                { "toggleButtons", thumbnailOverlayButtons ?? null }
            };
            foreach (JToken thumbnailOverlay in (JArray)thumbnailOverlayArray)
            {
                if (thumbnailOverlay.TryGetToken("thumbnailOverlayResumePlaybackRenderer", out JToken overlayResumeRendererToken))
                {
                    thumbnailOverlays.Add("percentDurationWatchedOverlay", overlayResumeRendererToken["percentDurationWatched"]);
                    continue;
                }
                if (thumbnailOverlay.TryGetToken("thumbnailOverlayTimeStatusRenderer", out JToken overlayStatusToken))
                {
                    thumbnailOverlays.Add("timeStatusOverlay", new JObject()
                    {
                        new JProperty("text", overlayStatusToken.TryGetToken("text", out JToken textToken) ? textToken["simpleText"] : null),
                        new JProperty("label", overlayStatusToken.TryGetToken("text", out JToken textLabelToken) ? ExtractFromAccessibility(textLabelToken, "accessibility") : null),
                        new JProperty("style", overlayStatusToken["style"])
                    });
                    continue;
                }
                if (thumbnailOverlay.TryGetTokenIfContains(new string[] { "thumbnailOverlayHoverTextRenderer", "thumbnailOverlaySidePanelRenderer" }, out JToken thumbnailOverlayToken, out string key))// Error is here!
                {
                    switch (key)
                    {
                        case "thumbnailOverlayHoverTextRenderer":
                            thumbnailOverlays.Add("hoverTextOverlay", new JObject()
                            {
                                new JProperty("text", ExtractRuns(thumbnailOverlayToken["text"])),
                                new JProperty("icon", thumbnailOverlayToken.TryGetToken("icon", out JToken iconHoverOverlayToken) ? iconHoverOverlayToken["iconType"] : null)
                            });
                            break;
                        case "thumbnailOverlaySidePanelRenderer":
                            thumbnailOverlays.Add("sidePanelOverlay", new JObject()
                            {
                                new JProperty("text", ExtractRuns(thumbnailOverlayToken["text"])),
                                new JProperty("icon", thumbnailOverlayToken.TryGetToken("icon", out JToken iconPanelOverlayToken) ? iconPanelOverlayToken["iconType"] : null)
                            });
                            break;
                        default:
                            break;
                    }
                    continue;
                }
                if (thumbnailOverlay.TryGetToken("thumbnailOverlayNowPlayingRenderer", out JToken nowPlayingOverlayToken))
                {
                    thumbnailOverlays.Add("nowPlayingOverlay", ExtractRuns(nowPlayingOverlayToken["text"]));
                    continue;
                }
                if (thumbnailOverlay.TryGetToken("thumbnailOverlayEndorsementRenderer", out JToken endorsementOverlayToken))
                {
                    thumbnailOverlays.Add("endorsementOverlay", endorsementOverlayToken);// Maybe changed in the future. Who knows!
                    continue;
                }
            }
            return thumbnailOverlays;
        }
        private static JObject ExtractChannelThumbnail(JToken channelThumbnailToken)
        {
            JObject channelThumbnailItem = new JObject();
            if (channelThumbnailToken == null)
                return null;
            if (channelThumbnailToken.TryGetToken("channelThumbnailWithLinkRenderer", out JToken channelThumbnailLinkToken))
            {
                channelThumbnailItem.Add("thumbnail", ExtractThumbnail(channelThumbnailLinkToken));
                channelThumbnailItem.Add("endpoint", ExtractEndpoint(channelThumbnailLinkToken));
                channelThumbnailItem.Add("label", ExtractFromAccessibility(channelThumbnailLinkToken, "accessibility"));
                return channelThumbnailItem;
            }
            return null;
        }
        private static JArray ExtractRuns(JToken snippetToken)
        {
            JArray runs = new JArray();
            if (snippetToken == null)
                return null;
            if (snippetToken.TryGetToken("runs", out JToken runsToken))
            {
                foreach (JToken run in (JArray)runsToken)
                {
                    JObject runItem = new JObject
                    {
                        { "text", run["text"] },
                        { "endpoint", ExtractEndpoint(run) }
                    };
                    runs.Add(runItem);
                }
                return runs;
            }
            return null;
        }
        private static JToken ExtractBadge(JToken badgeToken)
        {
            JToken badge = null;
            if (badgeToken == null)
                return badge;
            switch (badgeToken.Type)
            {
                case JTokenType.Array:
                    JArray badgeArray = new JArray();
                    foreach (JToken badgeMetadata in badgeToken)
                        badgeArray.Add(ExtractBadge(badgeMetadata));
                    badge = badgeArray;
                    break;
                case JTokenType.Object:
                    JObject badgeObject = new JObject();
                    if (badgeToken.TryGetToken("metadataBadgeRenderer", out JToken badgeMetaToken))
                    {
                        badgeObject.Add("icon", badgeMetaToken.TryGetToken("icon", out JToken badgeIconToken) ? badgeIconToken["iconType"] : null);
                        badgeObject.Add("style", badgeMetaToken["style"]);
                        badgeObject.Add("label", badgeMetaToken["label"]);
                        badgeObject.Add("tooltip", badgeMetaToken["tooltip"]);
                        badgeObject.Add("trackingParams", badgeMetaToken["trackingParams"]);
                    }
                    badge = badgeObject;
                    break;
            }
            return badge;
        }
        private static JToken ExtractCommand(JToken commandToken)
        {
            if (commandToken == null)
                return null;
            JObject commandObject = new JObject();
            JArray commandsArray = new JArray();
            if (commandToken.TryGetToken("commands", out JToken commandsToken))
            {
                foreach (var commandItem in (JArray)commandsToken)
                {
                    commandObject.Add("clickTrackingParams", commandItem["clickTrackingParams"]);
                    commandObject.Add("action", ExtractActions(commandItem));
                    commandsArray.Add(commandObject);
                }
                return commandsArray;
            }
            if (commandToken.TryGetToken("command", out JToken command))
            {
                commandObject.Add("kind", CommandType.Command.ToString());
                if (command.TryGetToken("commandExecutorCommand", out JToken commandExecuter))
                {
                    JArray commandExecuters = new JArray();
                    foreach (var commandExecute in commandExecuter["commands"])
                    {
                        JObject commandExecuterRebuild = new JObject
                        {
                            { "clickTrackingParams", commandExecute["clickTrackingParams"] },
                            { "action", ExtractActions(commandExecute) }
                        };
                        commandExecuters.Add(commandExecuterRebuild);
                    }
                    commandObject.Add("commands", commandExecuters);
                }
                commandObject.Add("clickTrackingParams", command["clickTrackingParams"]);
                commandObject.Add("commandMetadata", ExtractCommandMetadata(command));
                commandObject.Add("endpoint", ExtractEndpoint(command));
                return commandObject;
            }
            if (commandToken.TryGetToken("buttonCommand", out JToken buttonCommand))
            {
                commandObject.Add("kind", CommandType.ButtonCommand.ToString());
                commandObject.Add("clickTrackingParams", buttonCommand["clickTrackingParams"]);
                commandObject.Add("commandMetadata", ExtractCommandMetadata(buttonCommand));
                commandObject.Add("endpoint", ExtractEndpoint(buttonCommand));
                return commandObject;
            }
            if (commandToken.TryGetToken("clickCommand", out JToken clickCommand))
            {
                commandObject.Add("kind", CommandType.ClickCommand.ToString());
                commandObject.Add("clickTrackingParams", clickCommand["clickTrackingParams"]);
                commandObject.Add("commandMetadata", ExtractCommandMetadata(clickCommand));
                commandObject.Add("endpoint", ExtractEndpoint(clickCommand));
                return commandObject;
            }
            if (commandToken.TryGetToken("addToPlaylistCommand", out JToken addToPlaylistCommand))
            {
                commandObject.Add("kind", CommandType.AddToPlaylist.ToString());
                commandObject.Add("openMiniplayer", addToPlaylistCommand["openMiniPlayer"]);
                commandObject.Add("videoId", addToPlaylistCommand["videoId"]);
                commandObject.Add("listType", addToPlaylistCommand["listType"]);
                commandObject.Add("onCreateListCommand", ExtractCommand(addToPlaylistCommand));
                commandObject.Add("videoIds", addToPlaylistCommand["videoIds"]);
                return commandObject;
            }
            if (commandToken.TryGetToken("onCreateListCommand", out JToken onCreateListCommand))
            {
                commandObject.Add("kind", CommandType.OnCreateList.ToString());
                commandObject.Add("clickTrackingParams", onCreateListCommand["clickTrackingParams"]);
                commandObject.Add("commandMetadata", ExtractCommandMetadata(onCreateListCommand));
                commandObject.Add("endpoint", ExtractEndpoint(onCreateListCommand));
                return commandObject;
            }
            return null;
        }
        public enum CommandType
        {
            Command,
            ButtonCommand,
            ClickCommand,
            AddToPlaylist,
            OnCreateList
        }
        private static JToken ExtractButton(JToken buttonRenderer)
        {
            JToken buttonToken = null;
            if (buttonRenderer == null)
                return buttonToken;
            JObject buttonObj;
            switch (buttonRenderer.Type)
            {
                case JTokenType.None:
                    break;
                case JTokenType.Object:
                    buttonObj = new JObject();
                    if (buttonRenderer.TryGetToken("buttonRenderer", out JToken buttonRendererToken))
                        buttonObj = ExtractButtonRenderer(buttonRendererToken, ButtonType.Button);
                    if (buttonRenderer.TryGetToken("commentActionButtonsRenderer", out JToken commentActionButtonToken))
                    {
                        if (commentActionButtonToken.TryGetToken("likeButton", out JToken likeButtonToken))
                        {
                            buttonObj.Add("likeButton", ExtractButtonRenderer(likeButtonToken["toggleButtonRenderer"], ButtonType.ToggleButton));
                        }
                        if (commentActionButtonToken.TryGetToken("replyButton", out JToken replayButtonToken))
                        {
                            buttonObj.Add("replyButton", ExtractButtonRenderer(replayButtonToken["buttonRenderer"], ButtonType.Button));
                        }
                        if (commentActionButtonToken.TryGetToken("dislikeButton", out JToken dislikeButtonToken))
                        {
                            buttonObj.Add("dislikeButton", ExtractButtonRenderer(dislikeButtonToken["toggleButtonRenderer"], ButtonType.ToggleButton));
                        }
                        buttonObj.Add("trackingParams", commentActionButtonToken["trackingParams"]);
                        buttonObj.Add("style", commentActionButtonToken["style"]);
                    }
                    if (buttonRenderer.TryGetToken("subscribeButtonRenderer", out JToken subscribeButtonToken))
                        buttonObj = ExtractButtonRenderer(subscribeButtonToken, ButtonType.SubscribeButton);
                    if (buttonRenderer.TryGetToken("notificationPreferenceButton", out JToken notificationPreference))
                        buttonObj = ExtractButtonRenderer(notificationPreference["subscriptionNotificationToggleButtonRenderer"], ButtonType.NotificationPreferenceButton);
                    buttonToken = buttonObj;
                    break;
                case JTokenType.Array:
                    JArray buttonsArray = new JArray();
                    foreach (var button in buttonRenderer)
                    {
                        buttonObj = new JObject();
                        if (button.TryGetToken("buttonRenderer", out JToken buttonRendererFromArray))
                        {
                            buttonObj = ExtractButtonRenderer(buttonRendererFromArray, ButtonType.Button);
                            buttonsArray.Add(buttonObj);
                            continue;
                        }
                        if (button.TryGetToken("toggleButtonRenderer", out JToken toggleButtonToken))
                        {
                            buttonObj = ExtractButtonRenderer(toggleButtonToken, ButtonType.ToggleButton);
                            buttonsArray.Add(buttonObj);
                            continue;
                        }
                        if (button.TryGetToken("thumbnailOverlayToggleButtonRenderer", out JToken overlayToggleButtonToken))
                        {
                            buttonObj = ExtractButtonRenderer(overlayToggleButtonToken, ButtonType.OverlayToggleButton);
                            buttonsArray.Add(buttonObj);
                            continue;
                        }
                        if (button.TryGetToken("topbarMenuButtonRenderer", out JToken topbarMenuButtonRenderer))
                        {
                            buttonObj = ExtractButtonRenderer(topbarMenuButtonRenderer, ButtonType.MenuButton);
                            buttonsArray.Add(buttonObj);
                            continue;
                        }
                        if (button.TryGetToken("notificationTopbarButtonRenderer", out JToken topbarNotificationButtonRenderer))
                        {
                            buttonObj = ExtractButtonRenderer(topbarNotificationButtonRenderer, ButtonType.MenuNotificationButton);
                            buttonsArray.Add(buttonObj);
                            continue;
                        }
                    }
                    buttonToken = buttonsArray;
                    break;
                default:
                    break;
            }
            return buttonToken;
        }
        private static JObject ExtractButtonRenderer(JToken buttonRenderer, ButtonType buttonType)
        {
            if (buttonRenderer == null)
                return null;
            JObject buttonRebuild = new JObject();
            switch (buttonType)
            {
                case ButtonType.Button:
                    buttonRebuild.Add("kind", buttonType.ToString());
                    buttonRebuild.Add("style", buttonRenderer["style"]);
                    buttonRebuild.Add("size", buttonRenderer["size"]);
                    buttonRebuild.Add("isDisabled", buttonRenderer["isDisabled"]);
                    buttonRebuild.Add("text", new JObject()
                    {
                        new JProperty("text", ExtractFromAccessibility(buttonRenderer["text"], "accessibility") ?? ExtractRuns(buttonRenderer["text"])),
                        new JProperty("shortText", buttonRenderer.TryGetToken("text", out JToken textToken) ? textToken["simpleText"] : null)
                    });
                    buttonRebuild.Add("endpoint", ExtractEndpoint(buttonRenderer));
                    buttonRebuild.Add("icon", buttonRenderer.TryGetToken("icon", out JToken iconToken) ? iconToken["iconType"] : null);
                    buttonRebuild.Add("label", ExtractFromAccessibility(buttonRenderer, "accessibility"));
                    buttonRebuild.Add("tooltip", buttonRenderer["tooltip"] ?? ExtractFromAccessibility(buttonRenderer, "accessibilityData"));
                    buttonRebuild.Add("trackingParams", buttonRenderer["trackingParams"]);
                    buttonRebuild.Add("command", ExtractCommand(buttonRenderer));
                    buttonRebuild.Add("iconPosition", buttonRenderer["iconPosition"]);
                    break;
                case ButtonType.ToggleButton:
                    buttonRebuild.Add("kind", buttonType.ToString());
                    buttonRebuild.Add("size", buttonRenderer.TryGetToken("size", out JToken sizeToken) ? sizeToken["sizeType"] : buttonRenderer["size"]);
                    buttonRebuild.Add("isToggled", buttonRenderer["isToggled"]);
                    buttonRebuild.Add("isDisabled", buttonRenderer["isDisabled"]);
                    buttonRebuild.Add("untoggledMetadata", GetToggleMetadata(buttonRenderer, false));
                    buttonRebuild.Add("toggledMetadata", GetToggleMetadata(buttonRenderer, true));
                    buttonRebuild.Add("trackingParams", buttonRenderer["trackingParams"]);
                    break;
                case ButtonType.OverlayToggleButton:
                    buttonRebuild.Add("kind", buttonType.ToString());
                    buttonRebuild.Add("isToggled", buttonRenderer["isToggled"]);
                    buttonRebuild.Add("untoggledMetadata", GetToggleMetadata(buttonRenderer, false));
                    buttonRebuild.Add("toggledMetadata", GetToggleMetadata(buttonRenderer, true));
                    break;
                case ButtonType.MenuButton:
                case ButtonType.MenuNotificationButton:
                    buttonRebuild.Add("icon", buttonRenderer.TryGetToken("icon", out JToken iconMenuButtonToken) ? iconMenuButtonToken["iconType"] : null);
                    if (buttonType == ButtonType.MenuButton)
                    {
                        buttonRebuild.Add("kind", buttonType.ToString());
                        buttonRebuild.Add("menuRenderer", ExtractMenuRenderer(buttonRenderer));
                    }
                    if (buttonType == ButtonType.MenuNotificationButton)
                    {
                        buttonRebuild.Add("kind", buttonType.ToString());
                        if (buttonRenderer.TryGetToken("menuRequest", out JToken menuRequest))
                        {
                            buttonRebuild.Add("menuRequest", new JObject()
                            {
                                new JProperty("clickTrackingParams", menuRequest["clickTrackingParams"]),
                                new JProperty("commandMetadata", ExtractCommandMetadata(menuRequest)),
                                new JProperty("endpoint", ExtractEndpoint(menuRequest))
                            });
                        }
                        buttonRebuild.Add("endpoint", ExtractEndpoint(buttonRenderer));
                        buttonRebuild.Add("notificationCount", buttonRenderer["notificationCount"]);
                        buttonRebuild.Add("handlerDatas", buttonRenderer["handlerDatas"]);
                    }
                    buttonRebuild.Add("trackingParams", buttonRenderer["trackingParams"]);
                    buttonRebuild.Add("label", ExtractFromAccessibility(buttonRenderer, "accessibility"));
                    buttonRebuild.Add("tooltip", buttonRenderer["tooltip"]);
                    buttonRebuild.Add("style", buttonRenderer["style"]);
                    break;
                case ButtonType.SubscribeButton:
                    buttonRebuild.Add("kind", buttonType.ToString());
                    buttonRebuild.Add("buttonText", ExtractRuns(buttonRenderer["buttonText"]));
                    buttonRebuild.Add("subscribed", buttonRenderer["subscribed"]);
                    buttonRebuild.Add("enabled", buttonRenderer["enabled"]);
                    buttonRebuild.Add("type", buttonRenderer["type"]);
                    buttonRebuild.Add("channelId", buttonRenderer["channelId"]);
                    buttonRebuild.Add("showPreferences", buttonRenderer["showPreferences"]);
                    buttonRebuild.Add("subscribeMetadata", new JObject()
                    {
                        new JProperty("buttonText", ExtractRuns(buttonRenderer["subscribedButtonText"])),
                        new JProperty("label", ExtractFromAccessibility(buttonRenderer, "subscribeAccessibility")),
                        new JProperty("endpoint", ExtractEndpoint(buttonRenderer))
                    });
                    buttonRebuild.Add("unsubscribeMetadata", new JObject()
                    {
                        new JProperty("buttonText", ExtractRuns(buttonRenderer["unsubscribeButtonText"])),
                        new JProperty("label", ExtractFromAccessibility(buttonRenderer, "unsubscribeAccessibility")),
                        new JProperty("endpoint", ExtractEndpoint(buttonRenderer))
                    });
                    buttonRebuild.Add("trackingParams", buttonRenderer["trackingParams"]);
                    buttonRebuild.Add("notificationPreferenceButton", ExtractButton(buttonRenderer));
                    buttonRebuild.Add("targetId", buttonRenderer["targetId"]);
                    buttonRebuild.Add("subscribedEntityKey", buttonRenderer["subscribedEntityKey"]);
                    //buttonRebuild.Add("endpoints");
                    break;
                case ButtonType.NotificationPreferenceButton:
                    buttonRebuild.Add("kind", buttonType.ToString());
                    if (buttonRenderer.TryGetToken("states", out JToken statesButtonToken))
                    {
                        JArray buttonStates = new JArray();
                        foreach (var state in (JArray)statesButtonToken)
                        {
                            JObject buttonState = new JObject
                            {
                                { "stateId", state["stateId"] },
                                { "nextStateId", state["nextStateId"] },
                                { "state", ExtractButton(state["state"]) }
                            };
                            buttonStates.Add(buttonState);
                        }
                        buttonRebuild.Add("states", buttonStates);
                    }
                    buttonRebuild.Add("currentState", buttonRenderer["currentStateId"]);
                    buttonRebuild.Add("trackingParams", buttonRenderer["trackingParams"]);
                    buttonRebuild.Add("command", ExtractCommand(buttonRenderer));
                    buttonRebuild.Add("targetId", buttonRenderer["targetId"]);
                    break;
                default:
                    buttonRebuild = null;
                    Trace.WriteLine($"Unknown button found: {buttonType}");
                    break;
            }
            return buttonRebuild;
        }
        private static JToken ExtractMenuRenderer(JToken menuToken)
        {
            if (menuToken == null)
                return null;
            JObject menuRenderer = new JObject();
            if (menuToken.TryGetToken("menuRenderer", out JToken menuRendererToken))
            {
                if (menuRendererToken.TryGetToken("multiPageMenuRenderer", out JToken multiPageMenu))
                {
                    JArray multiPageMenuLinkRenderers = new JArray();
                    JArray sectionRenderersArray = new JArray();
                    foreach (var sections in multiPageMenu["sections"])
                    {
                        if (sections.TryGetToken("multiPageMenuSectionRenderer", out JToken multiPageMenuSectionsRenderer))
                            sectionRenderersArray.Add(multiPageMenuSectionsRenderer);
                    }
                    foreach (var multipageSection in sectionRenderersArray)
                    {
                        foreach (var item in (JArray)multipageSection["items"])
                        {
                            if (item.TryGetToken("compactLinkRenderer", out JToken compactLinkRenderer))
                            {
                                JObject multiPageItem = new JObject
                                {
                                    { "icon", compactLinkRenderer.TryGetToken("icon", out JToken multiPageIcon) ? multiPageIcon["iconType"] : null },
                                    { "title", ExtractRuns(compactLinkRenderer["title"]) },
                                    { "endpoint", ExtractEndpoint(compactLinkRenderer) },
                                    { "trackingParams", compactLinkRenderer["trackingParams"] },
                                    { "style", compactLinkRenderer["style"] }
                                };
                                multiPageMenuLinkRenderers.Add(multiPageItem);
                            }
                        }
                    }
                    JObject multiPageRenderer = new JObject
                    {
                        { "items", (multiPageMenuLinkRenderers.Count != 0) ? multiPageMenuLinkRenderers : null }
                    };
                    multiPageRenderer.Add("trackingParams", multiPageMenu["trackingParams"]);
                    multiPageRenderer.Add("style", multiPageMenu["style"]);
                    menuRenderer.Add("itemRenderer", multiPageRenderer);
                }
            }
            menuRenderer.Add("icon", menuToken.TryGetToken("icon", out JToken menuIconToken) ? menuIconToken["iconType"] : null);
            menuRenderer.Add("trackingParams", menuToken["trackingParams"]);
            menuRenderer.Add("label", ExtractFromAccessibility(menuToken, "accessibility"));
            menuRenderer.Add("tooltip", menuToken["tooltip"]);
            menuRenderer.Add("style", menuToken["style"]);
            return menuRenderer;
        }
        private static JToken GetToggleMetadata(JToken metadata, bool toggled)
        {
            if (metadata == null)
                return metadata;
            JObject toggleMetadata = new JObject();
            if (toggled)// Toggled
            {
                toggleMetadata.Add("icon", metadata.TryGetToken("toggledIcon", out JToken toggledIcon) ? toggledIcon["iconType"] : null);
                toggleMetadata.Add("tooltip", metadata["toggledTooltip"]);
                toggleMetadata.Add("style", metadata.TryGetToken("toggledStyle", out JToken toggledStyleToken) ? toggledStyleToken["styleType"] : null);
                toggleMetadata.Add("label", ExtractFromAccessibility(metadata, "toggledAccessibility") ?? ExtractFromAccessibility(metadata, "toggledAccessibilityData"));
                toggleMetadata.Add("endpoint", ExtractEndpoint(metadata));
            }
            else// Untoggled
            {
                toggleMetadata.Add("icon", metadata.TryGetToken("untoggledIcon", out JToken untoggledIcon) ? untoggledIcon["iconType"] : null);
                toggleMetadata.Add("tooltip", metadata["untoggledTooltip"] ?? ExtractRuns(metadata["defaultText"]) ?? metadata["defaultTooltip"]);
                toggleMetadata.Add("style", metadata.TryGetToken("style", out JToken untoggledStyleToken) ? untoggledStyleToken["styleType"] : null);
                toggleMetadata.Add("label", ExtractFromAccessibility(metadata, "accessibilityData") ?? ExtractFromAccessibility(metadata, "untoggledAccessibility"));
                toggleMetadata.Add("endpoint", ExtractEndpoint(metadata));
            }
            return toggleMetadata;
        }
        public enum ButtonType
        {
            Button,
            ToggleButton,
            OverlayToggleButton,
            MenuButton,
            MenuNotificationButton,
            SubscribeButton,
            NotificationPreferenceButton
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        private static JObject ExtractActionMenu(JToken actionMenu)
        {
            JObject menu = new JObject();
            if (actionMenu == null)
                return null;
            if (actionMenu.TryGetTokenIfContains(new string[] { "menuRenderer", "menuPopupRenderer" }, out JToken menuRenderer, out string keyResult))
            {
                JArray menuItems = new JArray();
                foreach (var menuItem in (JArray)menuRenderer["items"])
                {
                    JObject item = new JObject();
                    if (menuItem.TryGetToken("menuServiceItemRenderer", out JToken menuItemServiceToken))
                    {
                        item.Add("text", menuItemServiceToken.TryGetToken("text", out JToken menuTextToken) ? menuTextToken["simpleText"] ?? ExtractRuns(menuItemServiceToken["text"]) : null);
                        item.Add("icon", menuItemServiceToken.TryGetToken("icon", out JToken iconToken) ? iconToken["iconType"] : null);
                        item.Add("endpoint", ExtractEndpoint(menuItemServiceToken));
                        item.Add("trackingParams", menuItemServiceToken["trackingParams"]);
                        item.Add("isSelected", menuItemServiceToken["isSelected"]);
                    }
                    if (menuItem.TryGetToken("menuNavigationItemRenderer", out JToken menuItemNavigationToken))
                    {
                        item.Add("text", menuItemNavigationToken.TryGetToken("text", out JToken textNavigationToken) ? textNavigationToken["simpleText"] : null);
                        item.Add("icon", menuItemNavigationToken.TryGetToken("icon", out JToken iconNavigationToken) ? iconNavigationToken["iconType"] : null);
                        item.Add("endpoint", ExtractEndpoint(menuItemNavigationToken));
                        item.Add("trackingParams", menuItemNavigationToken["trackingParams"]);
                    }
                    menuItems.Add(item);
                }
                menu.Add("menuItems", menuItems);
                menu.Add("trackingParams", menuRenderer["trackingParams"]);
                menu.Add("topLevelButtons", ExtractButton(menuRenderer["topLevelButtons"]));
                menu.Add("label", ExtractFromAccessibility(menuRenderer, "accessibility"));
                menu.Add("menuPopupAccessibility", ExtractFromAccessibility(menuRenderer, "menuPopupAccessibility"));
                return menu;
            }
            return null;
        }
        private static JToken ExtractThumbnail(JToken property)
        {
            JToken thumbnailsToken = null;
            if (property.TryGetToken("thumbnail", out JToken thumbnail))
                thumbnailsToken = thumbnail["thumbnails"];
            if (property.TryGetToken("movingThumbnailDetails", out JToken thumbnailDetails))
            {
                JObject thumbnailsProperty = new JObject
                {
                    { "thumbnails", thumbnailDetails["thumbnails"] },
                    { "logAsMovingThumbnail", thumbnailDetails["logAsMovingThumbnail"] }
                };
                thumbnailsToken = thumbnailsProperty;
            }
            return thumbnailsToken;
        }
        private static JObject ExtractRichThumbnail(JToken richThumbnailToken)
        {
            JObject richThumbnail = new JObject();
            if (richThumbnailToken == null)
                return richThumbnail;
            if (richThumbnailToken.TryGetToken("movingThumbnailRenderer", out JToken thumbnailRendererToken))
            {
                JToken thumbnailDetails = ExtractThumbnail(thumbnailRendererToken);
                richThumbnail.Merge(thumbnailDetails);
                richThumbnail.Add("enableHoveredLogging", thumbnailRendererToken["enableHoveredLogging"]);
                richThumbnail.Add("enableOverlay", thumbnailRendererToken["enableOverlay"]);
                return richThumbnail;
            }
            return null;
        }
        private static JToken ExtractTitleProperty(JToken titleProp)
        {
            JObject title = new JObject
            {
                { "shortTitle", titleProp["runs"] },
                { "longTitle", ExtractFromAccessibility(titleProp, "accessibility") }
            };
            return title;
        }
        private static JToken ExtractFromAccessibility(JToken accesibilityDataObject, string key)
        {
            JToken labelToken = null;
            if (accesibilityDataObject.TryGetToken(key, out JToken accessibilityToken))
            {
                if (accessibilityToken.TryGetToken("accessibilityData", out JToken accessibilityDataToken))
                    labelToken = accessibilityDataToken["label"];
                if (accessibilityToken.TryGetToken("label", out JToken labelAccessibilityToken))
                    labelToken = labelAccessibilityToken;
            }
            return labelToken;
        }
        private static JObject ExtractContinuationItem(JToken continuationItem)
        {
            JObject continuationEndpoint = new JObject
            {
                { "trigger", continuationItem["trigger"] }
            };
            if (continuationItem.TryGetToken("continuationEndpoint", out JToken continuationToken))
            {
                continuationEndpoint.Add("clickTrackingParams", continuationToken["clickTrackingParams"]);
                continuationEndpoint.Add("commandMetadata", ExtractCommandMetadata(continuationToken));
                if (continuationToken.TryGetToken("continuationCommand", out JToken continuationCommand))
                {
                    continuationEndpoint.Add("token", continuationCommand["token"]);
                    continuationEndpoint.Add("request", continuationCommand["request"]);
                }
                return continuationEndpoint;
            }
            return null;
        }
        private static JObject ExtractCommandMetadata(JToken commandMetadata)
        {
            if (commandMetadata.TryGetToken("commandMetadata", out JToken commandMetadataToken))
                return (JObject)commandMetadataToken["webCommandMetadata"];
            return null;
        }
        private static JToken ExtractEndpoint(JToken endpointToken)
        {
            JObject endpointRebuild = new JObject();
            if (endpointToken == null) return null;
            foreach (JProperty endpointProperty in (endpointToken as JObject).Properties())
            {
                switch (endpointProperty.Name)
                {
                    case "endpoint":
                        endpointRebuild.Add("kind", EndpointType.Endpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "navigationEndpoint":
                        endpointRebuild.Add("kind", EndpointType.NavigationEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("loggingUrls", endpointProperty.Value["loggingUrls"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        if (endpointProperty.Value.TryGetToken("openPopupAction", out JToken popupActionToken))
                        {
                            endpointRebuild.Add("openPopupAction", GetAction(popupActionToken));
                        }
                        return endpointRebuild;
                    case "defaultServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.DefaultServiceEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "serviceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ServiceEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("loggingUrls", endpointProperty.Value["loggingUrls"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "signalServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.SignalServiceEndpoint.ToString());
                        endpointRebuild.Add("signal", endpointProperty.Value["signal"]);
                        endpointRebuild.Add("actions", ExtractActions((JArray)endpointProperty.Value["actions"]));
                        return endpointRebuild;
                    case "signalNavigationEndpoint":
                        endpointRebuild.Add("signal", endpointProperty.Value["signal"]);
                        return endpointRebuild;
                    case "searchEndpoint":
                        endpointRebuild.Add("kind", EndpointType.SearchEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("query", endpointProperty.Value.TryGetToken("searchEndpoint", out JToken searchEndpointToken) ? searchEndpointToken["query"] : null);
                        return endpointRebuild;
                    case "toggledServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ToggledServiceEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "untoggledServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.UntoggledServiceEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "confirmServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ConfirmServiceEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("loggingUrls", endpointProperty.Value["loggingUrls"]);
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "authorEndpoint":
                        endpointRebuild.Add("kind", EndpointType.AuthorEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "adInfoDialogEndpoint":
                        endpointRebuild.Add("kind", EndpointType.AdInfoDialogEndpoint.ToString());
                        if (endpointProperty.Value["dialog"].TryGetToken("adInfoDialogRenderer", out JToken adInfoDialogRendererToken))
                        {
                            endpointRebuild.Add("dialogMessage", ExtractRuns(adInfoDialogRendererToken["dialogMessage"]));
                            endpointRebuild.Add("confirmLabel", adInfoDialogRendererToken.TryGetToken("confirmLabel", out JToken confirmLabelToken) ? confirmLabelToken["simpleText"] : null);
                            endpointRebuild.Add("confirmServiceEndpoint", adInfoDialogRendererToken["confirmServiceEndpoint"]);
                            endpointRebuild.Add("trackingParams", adInfoDialogRendererToken["trackingParams"]);
                            endpointRebuild.Add("title", adInfoDialogRendererToken.TryGetToken("title", out JToken titleToken) ? titleToken["simpleText"] : null);
                            endpointRebuild.Add("adReasons", ExtractReasons(adInfoDialogRendererToken["adReasons"]));
                            endpointRebuild.Add("endpoints", ExtractEndpoint(endpointProperty.Value));
                        }
                        return endpointRebuild;
                    case "addUpcomingEventReminderEndpoint":
                        endpointRebuild.Add("kind", EndpointType.AddUpcomingEventReminderEndpoint.ToString());
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        return endpointRebuild;
                    case "removeUpcomingEventReminderEndpoint":
                        endpointRebuild.Add("kind", EndpointType.RemoveUpcomingEventReminderEndpoint.ToString());
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        return endpointRebuild;
                    case "playlistEditEndpoint":
                        endpointRebuild.Add("kind", EndpointType.PlaylistEditEndpoint.ToString());
                        endpointRebuild.Add("playlistId", endpointProperty.Value["playlistId"]);
                        endpointRebuild.Add("actions", endpointProperty.Value["actions"]);
                        return endpointRebuild;
                    case "addToPlaylistServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.AddToPlaylistServiceEndpoint.ToString());
                        endpointRebuild.Add("videoId", endpointProperty.Value["videoId"]);
                        return endpointRebuild;
                    case "createPlaylistServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.CreatePlaylistServiceEndpoint.ToString());
                        endpointRebuild.Add("videoIds", endpointProperty.Value["videoIds"]);
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        return endpointRebuild;
                    case "updateUnseenCountEndpoint":
                        endpointRebuild.Add("kind", EndpointType.UpdateUnseenCountEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "impressionEndpoints":
                        JArray impressionEndpoints = new JArray();
                        foreach (JToken impressionEndpoint in endpointProperty.Value)
                        {
                            ((JObject)impressionEndpoint).Add("kind", EndpointType.ImpressionEndpoint.ToString());
                            impressionEndpoints.Add(impressionEndpoint);
                        }
                        return impressionEndpoints;
                    case "onResponseReceivedEndpoints":
                        JArray responseRecievedEndpoints = new JArray();
                        foreach (JToken responseRecievedEndpoint in endpointProperty.Value)
                        {
                            JObject newResponseRecievedEndpoint = new JObject
                            {
                                { "kind", EndpointType.OnResponseReceivedEndpoints.ToString() },
                                { "clickTrackingParams", responseRecievedEndpoint["clickTrackingParams"] },
                                { "commandMetadata", ExtractCommandMetadata(responseRecievedEndpoint) },
                                { "endpoint", ExtractEndpoint(endpointProperty.Value) }
                            };
                            responseRecievedEndpoints.Add(newResponseRecievedEndpoint);
                        }
                        return responseRecievedEndpoints;
                    case "urlEndpoint":
                        endpointRebuild.Add("kind", EndpointType.UrlEndpoint.ToString());
                        endpointRebuild.Add("url", endpointProperty.Value["url"]);
                        endpointRebuild.Add("target", endpointProperty.Value["target"]);
                        endpointRebuild.Add("nofollow", endpointProperty.Value["nofollow"]);
                        return endpointRebuild;
                    case "watchEndpoint":
                        endpointRebuild.Add("kind", EndpointType.WatchEndpoint.ToString());
                        endpointRebuild.Add("videoId", endpointProperty.Value["videoId"]);
                        endpointRebuild.Add("playlistId", endpointProperty.Value["playlistId"]);
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        endpointRebuild.Add("continuePlayback", endpointProperty.Value["continuePlayback"]);
                        return endpointRebuild;
                    case "browseEndpoint":
                        endpointRebuild.Add("kind", EndpointType.BrowseEndpoint.ToString());
                        endpointRebuild.Add("browseId", endpointProperty.Value["browseId"]);
                        endpointRebuild.Add("canonicalBaseUrl", endpointProperty.Value["canonicalBaseUrl"]);
                        return endpointRebuild;
                    case "feedbackEndpoint":
                        endpointRebuild.Add("kind", EndpointType.FeedbackEndpoint.ToString());
                        endpointRebuild.Add("feedbackToken", endpointProperty.Value["feedbackToken"]);
                        endpointRebuild.Add("uiActions", endpointProperty.Value["uiActions"]);
                        endpointRebuild.Add("actions", ExtractActions((JArray)endpointProperty.Value["actions"]));
                        return endpointRebuild;
                    case "undoFeedbackEndpoint":
                        endpointRebuild.Add("kind", EndpointType.UndoFeedbackEndpoint.ToString());
                        endpointRebuild.Add("undoToken", endpointProperty.Value["undoToken"]);
                        endpointRebuild.Add("actions", ExtractActions((JArray)endpointProperty.Value["actions"]));
                        return endpointRebuild;
                    case "getReportFormEndpoint":
                        endpointRebuild.Add("kind", EndpointType.GetReportFormEndpoint.ToString());
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        return endpointRebuild;
                    case "pingingEndpoint":
                        endpointRebuild.Add("kind", EndpointType.PingingEndpoint.ToString());
                        endpointRebuild.Add("hack", endpointProperty.Value["hack"]);
                        return endpointRebuild;
                    case "performCommentActionEndpoint":
                        endpointRebuild.Add("kind", EndpointType.PerformCommentActionEndpoint.ToString());
                        endpointRebuild.Add("action", endpointProperty.Value["action"]);
                        endpointRebuild.Add("clientActions", ExtractActions((JArray)endpointProperty.Value["clientActions"]));
                        return endpointRebuild;
                    case "uploadEndpoint":
                        endpointRebuild.Add("kind", EndpointType.UploadEndpoint.ToString());
                        endpointRebuild.Add("hack", endpointProperty.Value["hack"]);
                        return endpointRebuild;
                    case "likeEndpoint":
                        endpointRebuild.Add("kind", EndpointType.LikeEndpoint.ToString());
                        endpointRebuild.Add("status", endpointProperty.Value["status"]);
                        endpointRebuild.Add("target", endpointProperty.Value.TryGetToken("target", out JToken targetVideoToken) ? targetVideoToken["videoId"] : null);
                        endpointRebuild.Add("likeParams", endpointProperty.Value["likeParams"]);
                        endpointRebuild.Add("dislikeParams", endpointProperty.Value["dislikeParams"]);
                        endpointRebuild.Add("removeLikeParams", endpointProperty.Value["removeLikeParams"]);
                        return endpointRebuild;
                    case "shareEntityServiceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ShareEntityServiceEndpoint.ToString());
                        endpointRebuild.Add("serializedShareEntity", endpointProperty.Value["serializedShareEntity"]);
                        endpointRebuild.Add("commands", ExtractCommand(endpointProperty.Value));
                        return endpointRebuild;
                    case "modifyChannelNotificationPreferenceEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ModifyChannelNotificationPreferenceEndpoint.ToString());
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        return endpointRebuild;
                    case "currentVideoEndpoint":
                        endpointRebuild.Add("kind", EndpointType.CurrentVideoEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("commandMetadata", ExtractCommandMetadata(endpointProperty.Value));
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "shareEntityEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ShareEntityEndpoint.ToString());
                        endpointRebuild.Add("serializedShareEntity", endpointProperty.Value["serializedShareEntity"]);
                        return endpointRebuild;
                    case "settingsEndpoint":
                        endpointRebuild.Add("kind", EndpointType.SettingsEndpoint.ToString());
                        endpointRebuild.Add("clickTrackingParams", endpointProperty.Value["clickTrackingParams"]);
                        endpointRebuild.Add("endpoint", ExtractEndpoint(endpointProperty.Value));
                        return endpointRebuild;
                    case "applicationSettingsEndpoint":
                        endpointRebuild.Add("kind", EndpointType.ApplicationSettingsEndpoint.ToString());
                        endpointRebuild.Add("hack", endpointProperty.Value["hack"]);
                        return endpointRebuild;
                    case "onSubscribeEndpoints":
                    case "onUnsubscribeEndpoints":
                        JArray subEndpoints = new JArray();
                        foreach (JToken subEndpoint in (JArray)endpointProperty.Value)
                        {
                            JObject subEndpointRebuild = new JObject();
                            if (endpointProperty.Name == "onUnsubscribeEndpoints")
                                subEndpointRebuild.Add("kind", EndpointType.OnUnsubscribeEndpoints.ToString());
                            if (endpointProperty.Name == "onSubscribeEndpoints")
                                subEndpointRebuild.Add("kind", EndpointType.OnSubscribeEndpoints.ToString());
                            subEndpointRebuild.Add("clickTrackingParams", subEndpoint["clickTrackingParams"]);
                            subEndpointRebuild.Add("commandMetadata", ExtractCommandMetadata(subEndpoint));
                            subEndpointRebuild.Add("endpoint", ExtractEndpoint(subEndpoint));
                            subEndpoints.Add(subEndpointRebuild);
                        }
                        return subEndpoints;
                    case "subscribeEndpoint":
                    case "unsubscribeEndpoint":
                        if (endpointProperty.Name == "subscribeEndpoint")
                            endpointRebuild.Add("kind", EndpointType.SubscribeEndpoint.ToString());
                        if (endpointProperty.Name == "unsubscribeEndpoint")
                            endpointRebuild.Add("kind", EndpointType.UnsubscribeEndpoint.ToString());
                        endpointRebuild.Add("channelIds", endpointProperty.Value["channelIds"]);
                        endpointRebuild.Add("params", endpointProperty.Value["params"]);
                        return endpointRebuild;
                    default:
                        break;
                }
            }
            return null;
        }
        private static JToken ExtractActions(JToken actionToken)//TODO: Needs a action type identifier!
        {
            if (actionToken == null)
                return null;
            switch (actionToken.Type)
            {
                case JTokenType.Array:
                    JArray actions = new JArray();
                    foreach (JToken action in actionToken)
                    {
                        JObject actionRebuild = new JObject
                        {
                            { "clickTrackingParams", action["clickTrackingParams"] },
                            { "command", ExtractCommand(action) }
                        };
                        actionRebuild.Add("items", GetAction(action));
                        actions.Add(actionRebuild);
                    }
                    return actions;
                case JTokenType.Object:
                    return GetAction(actionToken);
                default:
                    Trace.WriteLine("Could not extract action!");
                    break;
            }
            return null;
        }
        private static JToken GetAction(JToken action)
        {
            JObject actionObj = new JObject();
            if (action.TryGetTokenIfContains(new string[] { "replaceEnclosingAction", "updateCommentVoteAction", "undoFeedbackAction", "signalAction", "openPopupAction" }, out JToken actionToken, out string resultKey))
            {
                switch (resultKey)
                {
                    case "replaceEnclosingAction":
                        if (actionToken.TryGetToken("item", out JToken replaceEnclosingItem))
                        {
                            if (replaceEnclosingItem.TryGetToken("notificationMultiActionRenderer", out JToken multiActionRenderer))
                            {
                                actionObj.Add("responseText", ExtractRuns(multiActionRenderer["responseText"]));
                                actionObj.Add("buttons", ExtractButton(multiActionRenderer["buttons"]));
                                actionObj.Add("trackingParams", multiActionRenderer["trackingParams"]);
                                actionObj.Add("dismissalViewStyle", multiActionRenderer["dismissalViewStyle"]);
                            }
                        }
                        break;
                    case "updateCommentVoteAction":
                        actionObj.Add("clickTrackingParams", actionToken["clickTrackingParams"]);
                        actionObj.Add("voteCount", new JObject()
                        {
                            { "voteCount", ExtractFromAccessibility(actionToken["voteCount"], "accessibility") },
                            { "shortVoteCount", actionToken.TryGetToken("voteCount", out JToken voteCountToken) ? voteCountToken["simpleText"] : null },
                            { "voteStatus", actionToken["voteStatus"] }
                        });
                        break;
                    case "undoFeedbackAction":
                        actionObj.Add("hack", actionToken["hack"]);
                        break;
                    case "signalAction":
                        actionObj.Add("signal", actionToken["signal"]);
                        break;
                    case "openPopupAction":
                        if (actionToken.TryGetToken("popup", out JToken popupToken))
                        {
                            if (popupToken.TryGetToken("multiPageMenuRenderer", out JToken multiPageMenuRenderer))
                            {
                                actionObj.Add("popup", new JObject()
                                {
                                    { "trackingParams", multiPageMenuRenderer["trackingParams"] },
                                    { "style", multiPageMenuRenderer["style"] }
                                });
                                if (multiPageMenuRenderer.TryGetToken("header", out JToken headerToken))
                                {
                                    if (headerToken.TryGetToken("activeAccountHeaderRenderer", out JToken activeAccountHeader))
                                    {
                                        JObject activeAccount = new JObject
                                        {
                                            { "accountName", activeAccountHeader.TryGetToken("accountName", out JToken accountNameToken) ? accountNameToken["simpleText"] : null },
                                            { "accountPhoto", activeAccountHeader.TryGetToken("accountPhoto", out JToken accountPhotoToken) ? accountPhotoToken["thumbnails"] : null },
                                            { "endpoint", ExtractEndpoint(activeAccountHeader) },
                                            { "email", activeAccountHeader.TryGetToken("email", out JToken emailToken) ? emailToken["simpleText"] : null },
                                            { "manageAccountTitle", ExtractRuns(activeAccountHeader["manageAccountTitle"]) },
                                            { "trackingParams", activeAccountHeader["trackingParams"] }
                                        };
                                        actionObj.Add("activeAccount", activeAccount);
                                    }
                                }
                                //if (multiPageMenuRenderer.TryGetToken("sections", out JToken sectionsArray))// TODO: Needs to be implemented.
                                //{
                                //    foreach (JToken section in (JArray)sectionsArray)
                                //    {

                                //    }
                                //}
                            }
                            if (popupToken.TryGetToken("voiceSearchDialogRenderer", out JToken voiceSearchDialogRenderer))
                            {
                                actionObj.Add("popup", new JObject()
                                {
                                    { "placeholderHeader", ExtractRuns(voiceSearchDialogRenderer["placeholderHeader"]) },
                                    { "promptHeader", ExtractRuns(voiceSearchDialogRenderer["promptHeader"]) },
                                    { "exampleQuery1", ExtractRuns(voiceSearchDialogRenderer["exampleQuery1"]) },
                                    { "exampleQuery2", ExtractRuns(voiceSearchDialogRenderer["exampleQuery2"]) },
                                    { "promptMicrophoneLabel", ExtractRuns(voiceSearchDialogRenderer["promptMicrophoneLabel"]) },
                                    { "loadingHeader", ExtractRuns(voiceSearchDialogRenderer["loadingHeader"]) },
                                    { "connectionErrorHeader", ExtractRuns(voiceSearchDialogRenderer["connectionErrorHeader"]) },
                                    { "connectionErrorMicrophoneLabel", ExtractRuns(voiceSearchDialogRenderer["connectionErrorMicrophoneLabel"]) },
                                    { "permissionsHeader", ExtractRuns(voiceSearchDialogRenderer["permissionsHeader"]) },
                                    { "permissionsSubtext", ExtractRuns(voiceSearchDialogRenderer["permissionsSubtext"]) },
                                    { "disabledHeader", ExtractRuns(voiceSearchDialogRenderer["disabledHeader"]) },
                                    { "disabledSubtext", ExtractRuns(voiceSearchDialogRenderer["disabledSubtext"]) },
                                    { "exitButton", ExtractButton(voiceSearchDialogRenderer["exitButton"]) },
                                    { "trackingParams", voiceSearchDialogRenderer["trackingParams"] },
                                    { "microphoneOffPromptHeader", ExtractRuns(voiceSearchDialogRenderer["microphoneOffPromptHeader"])}
                                });
                            }
                            if (popupToken.TryGetToken("unifiedSharePanelRenderer", out JToken sharePanelRenderer))
                                actionObj.Add("popup", sharePanelRenderer);
                            if (((JObject)popupToken).ContainsKey("menuPopupRenderer"))
                                actionObj.Add("popup", ExtractActionMenu(popupToken));
                            if (popupToken.TryGetToken("confirmDialogRenderer", out JToken confirmDialogRenderer))
                            {
                                JArray dialogMessagesArray = new JArray();
                                if (confirmDialogRenderer.TryGetToken("dialogMessages", out JToken dialogMessages))
                                {
                                    foreach (var dialogMessage in (JArray)dialogMessages)
                                        dialogMessagesArray.Add(ExtractRuns(dialogMessage));
                                }
                                actionObj.Add("popup", new JObject()
                                {
                                    new JProperty("trackingParams", confirmDialogRenderer["trackingParams"]),
                                    new JProperty("dialogMessages", dialogMessagesArray),
                                    new JProperty("confirmButton", ExtractButton(confirmDialogRenderer["confirmButton"])),
                                    new JProperty("cancelButton", ExtractButton(confirmDialogRenderer["cancelButton"]))
                                });
                            }
                            if (popupToken.TryGetToken("fancyDismissibleDialogRenderer", out JToken dismissibleDialogRenderer))
                            {
                                actionObj.Add("popup", new JObject()
                                {
                                    { "dialogMessage", dismissibleDialogRenderer.TryGetToken("dialogMessage", out JToken dismissMessageToken) ? dismissMessageToken["simpleText"] : null },
                                    { "confirmLabel", dismissibleDialogRenderer.TryGetToken("confirmLabel", out JToken confirmLabelToken) ? confirmLabelToken["simpleText"] : null },
                                    { "trackingParams", dismissibleDialogRenderer["trackingParams"] }
                                });
                            }
                        }
                        actionObj.Add("popupType", actionToken["popupType"]);
                        actionObj.Add("beReused", actionToken["beReused"]);
                        break;
                    default:
                        Trace.WriteLine("No action found!");
                        break;
                }
            }
            return actionObj;
        }
        private static JArray ExtractReasons(JToken reasons)
        {
            JArray reasonsArray = new JArray();
            if (reasons != null)
            {
                foreach (var reason in (JArray)reasons)
                    reasonsArray.Add(reason["simpleText"]);
            }
            return reasonsArray;
        }
    }
}
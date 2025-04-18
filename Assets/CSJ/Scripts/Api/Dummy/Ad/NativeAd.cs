//------------------------------------------------------------------------------
// Copyright (c) 2018-2019 Beijing Bytedance Technology Co., Ltd.
// All Right Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
//------------------------------------------------------------------------------

#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IOS)

namespace ByteDance.Union
{
    using ByteDance.Union.Mediation;
    using UnityEngine;
    /// <summary>
    /// The native Ad.
    /// </summary>
    public class NativeAd : IClientBidding
    {
        public void Dispose() { }
        /// <summary>
        /// Gets the title for this Ad.
        /// </summary>
        public string GetTitle() { return string.Empty; }

        /// <summary>
        /// Gets the description for this Ad.
        /// </summary>
        public string GetDescription() { return string.Empty; }

        /// <summary>
        /// Gets the button text.
        /// </summary>
        public string GetButtonText() { return string.Empty; }

        /// <summary>
        /// Gets the app score.
        /// </summary>
        public int GetAppScore() { return 0; }

        /// <summary>
        /// Gets the app comment number.
        /// </summary>
        public int GetAppCommentNum() { return 0; }

        /// <summary>
        /// Gets the app size.
        /// </summary>
        public int GetAppSize() { return 0; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        public string GetSource() { return string.Empty; }

        /// <summary>
        /// Gets the interaction type.
        /// </summary>
        public int GetInteractionType() { return 0; }

        /// <summary>
        /// Gets the image mode.
        /// </summary>
        public int GetImageMode() { return 0; }

        /// <summary>
        /// Sets the download listener.
        /// </summary>
        public void SetDownloadListener(IAppDownloadListener listener)
        {
        }

        public void SetNativeAdInteractionListener(IInteractionAdInteractionListener listener, bool callbackOnMainThread = true) { }

        public void SetNativeAdDislikeListener(IDislikeInteractionListener dislikeInteractionListener) { }

        /// <summary>
        /// show the  express Ad
        /// <param name="type">the type of ad</param>
        /// <param name="x">the origin x of th ad</param>
        /// <param name="y">the origin y of th ad</param>
        /// </summary>
        public void ShowNativeAd(AdSlotType type, float x, float y) { }

        /// <summary>
        /// Gets the ad Type.
        /// </summary>
        public AdSlotType GetAdType() { return 0; }

        public void setAuctionPrice(double price) { }

        public void win(double price)
        {
        }

        public void Loss(double price, string reason, string bidder) { }
        
        public void UploadDislikeEvent(string dislikeStr) {}

        public void RenderNative(AndroidJavaObject activity, IDislikeInteractionListener dislikeInteractionListener, AdSlotType type, bool callbackOnMainThread = true) { }

        public MediationNativeManager GetMediationManager() { return null; }

        public void SetAdInteractionListener(ITTAdInteractionListener listener, bool callbackOnMainThread = true) {}
    }

}

#endif

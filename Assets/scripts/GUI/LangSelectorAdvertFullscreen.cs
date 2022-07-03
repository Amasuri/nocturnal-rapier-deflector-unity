using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LangSelectorAdvertFullscreen : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string _androidAdUnitId = "Interstitial_Android";
    private string _iOsAdUnitId = "Interstitial_iOS";
    private string _adUnitId;

    static public LangSelectorAdvertFullscreen fullscreen;
    public static bool Loaded = false;

    private void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

        fullscreen = this;
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Loaded = true;

        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");

        LangSelectorAdvertBanner.banner.ShowBannerAd();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");

        LangSelectorAdvertBanner.banner.ShowBannerAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        LangSelectorAdvertBanner.banner.ShowBannerAd();
    }
}

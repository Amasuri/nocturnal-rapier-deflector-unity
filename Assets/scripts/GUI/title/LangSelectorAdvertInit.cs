using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LangSelectorAdvertInit : MonoBehaviour, IUnityAdsInitializationListener
{
    private string _androidGameId = "4801407";
    private string _iOSGameId = "4801406";
    private bool _testMode = false;
    private string _gameId;

    static public bool Finalized => Advertisement.isInitialized && LangSelectorAdvertFullscreen.Loaded;
    static public bool AdSupported => SystemInfo.operatingSystem.Contains("Android");

    //Internal variable for easier compilation of Free and Paid builds
    public const bool ADS_DISABLED__BUILD_PAID = false;

    private void Awake()
    {
#if !UNITY_ANDROID
        return;
#endif
        if (!SystemInfo.operatingSystem.Contains("Android") || ADS_DISABLED__BUILD_PAID)
            return;

        InitializeAds();
    }

    private void Update()
    {
#if !UNITY_ANDROID
        return;
#endif

        if (!SystemInfo.operatingSystem.Contains("Android") || ADS_DISABLED__BUILD_PAID)
            return;

        if (Advertisement.isInitialized && LangSelectorAdvertBanner.banner != null)
        {
            if (!Advertisement.Banner.isLoaded)
                LangSelectorAdvertBanner.banner.LoadBanner();
        }

        if (Advertisement.isInitialized && LangSelectorAdvertFullscreen.fullscreen != null)
        {
            if (!LangSelectorAdvertFullscreen.Loaded)
                LangSelectorAdvertFullscreen.fullscreen.LoadAd();
        }
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}

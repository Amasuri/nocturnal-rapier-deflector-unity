using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LangSelectorAdvertInit : MonoBehaviour, IUnityAdsInitializationListener
{
    /*[SerializeField] */private string _androidGameId = "4801407";
    /*[SerializeField] */private string _iOSGameId = "4801406";
    /*[SerializeField] */private bool _testMode = false;
    private string _gameId;

    private void Awake()
    {
        InitializeAds();
    }

    private void Update()
    {
        if(Advertisement.isInitialized && LangSelectorAdvertBanner.banner != null)
        {
            if (!Advertisement.Banner.isLoaded)
                LangSelectorAdvertBanner.banner.LoadBanner();
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

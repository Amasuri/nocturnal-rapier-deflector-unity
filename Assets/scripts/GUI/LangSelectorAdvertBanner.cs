using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class LangSelectorAdvertBanner : MonoBehaviour
{
    private BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;

    private string _androidAdUnitId = "Banner_Android";
    private string _adUnitId = null;

    public static LangSelectorAdvertBanner banner;

    private void Start()
    {
        _adUnitId = _androidAdUnitId;

        Advertisement.Banner.SetPosition(_bannerPosition);

        banner = this;
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    public void ShowBannerAd()
    {
        Advertisement.Banner.Show(_adUnitId);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}

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

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);

        banner = this;

        //LoadBanner();
    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");

        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    private void ShowBannerAd()
    {
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    private void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
}

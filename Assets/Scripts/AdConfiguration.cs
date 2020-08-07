using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdConfiguration : MonoBehaviour
{
    public static AdConfiguration Instance { get; private set; }
    BannerView bannerView;
    InterstitialAd interstitial;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
    }

    private void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        GameOver();
    }

}

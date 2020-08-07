using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdConfiguration : MonoBehaviour
{
    public static AdConfiguration Instance { get; private set; }
    private RewardBasedVideoAd rewardBasedVideo;
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
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        RequestBanner();
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    public void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-5390271097374264/9169208237";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-5390271097374264/5458135886";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void UserOptToWatchAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        GameConfiguration.Instance.NextLevel();
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        UserOptToWatchAd();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-5390271097374264/7117759964";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-5390271097374264/4075103151";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5390271097374264/7559778943";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-5390271097374264/9135858146";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;

         // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
    // Load the interstitial with the request.
    this.interstitial.LoadAd(request);
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

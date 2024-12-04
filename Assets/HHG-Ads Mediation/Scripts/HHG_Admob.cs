
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.Advertisements;
using ToastPlugin;

namespace HHG_Mediation
{



    [Serializable]
    public class admobADIDs
    {
        public string admobAppID;
        public string banner1;
        public string banner2;
        public string interstitialID;
        public string rewardedVideo;

        public string bigBannerID;
        public string rewardInterstitial;
        public string appOpenID;

        public string UnityId;
        public string Unity_Interstitial_ID = "Interstitial_Android";
        public string Unity_RewardedVideo = "Rewarded_Android";

    }
    public class HHG_Admob : HHG_AdsCall
    {
        public admobADIDs AndroidAdmob_ID = new admobADIDs();
        public admobADIDs TestAdmob_ID = new admobADIDs();
        [HideInInspector]
        public admobADIDs ADMOB_ID = new admobADIDs();

        public AdPosition banner1_Position;
        public AdPosition banner2_Position;

        public static bool isSmallBannerLoadedFirst = false;
        public static bool isSmallBannerLoadedSecond = false;
        public static bool isMediumBannerLoaded = false;
        bool isAdmobInitialized = false;


        #region Small Banner ADs Variable
        [HideInInspector]
        public BannerView SmallBanner_L_Medium_Ecpm;
        [HideInInspector]
        public BannerView SmallBanner_R_Medium_Ecpm;

        public static bool Logs;

        #endregion

        #region Intersitial ADs Variable
        [HideInInspector]
        public InterstitialAd Interstitial_High_Ecpm;

        public delegate void InterstitialUnity();
        public static event InterstitialUnity Int_Unity;

        public static bool Interstitial_HighEcpm = true, UnityAds = false;
        #endregion

        #region RewardVideo ADs Variable
        private static RewardUserDelegate NotifyReward;

        [HideInInspector]
        public RewardedAd rewardBasedVideo;

        public delegate void RewardVideoUnity();
        public static event RewardVideoUnity RewardVideo_Unity;
        public static bool RewardVideo_High_Ecpm = true, UnityRewarded = false;
        #endregion

        #region Medium Banner ADs Variable

        [HideInInspector]
        public BannerView MediumBannerMediumEcpm;

        #endregion

        #region Rewared Interstitial ADs Variable

        [HideInInspector]
        public RewardedInterstitialAd rewardedInterstitialAd;
        [HideInInspector]
        public bool rewardedInterstitialHighECPMLoaded;

        #endregion

        #region AppOpen ADs Variable
        private AppOpenAd ad;
        private bool isShowingAd = false;
        private DateTime loadTime;
        #endregion

        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DontDestroyOnLoad(this.gameObject);
            Logs = disbaleLogMode;

            if (testingMode)
            {
                ADMOB_ID = TestAdmob_ID;
            }
            else
            {
#if UNITY_ANDROID
                ADMOB_ID = AndroidAdmob_ID;
#elif UNITY_IOS
        ADMOB_ID = IosAndroid_ID;
          RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
       .SetSameAppKeyEnabled(false)
       .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
#endif
            }
        }
        private void Start()
        {
            InitAdmob();
            InitializeAds();

            #region AppOpen
            LoadAd();
            #endregion
        }
        public void InitializeAds()
        {
           // Advertisement.Initialize(ADMOB_ID.UnityId, testingMode, this);
        }
        public void OnInitializationComplete()
        {
            HHG_Logger.HHG_LogEvent("unity_advertisement_initialized_done");
        }
        public void OnInitializationFailed( string message)
        {
            Debug.Log($"unity_advertisement_initialization_failed: {ToString()} - {message}");
        }
        private void InitAdmob()
        {
            HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Initializing);

            MobileAds.Initialize((initStatus) =>
            {
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    switch (status.InitializationState)
                    {
                        case AdapterState.NotReady:
                            break;
                        case AdapterState.Ready:
                            HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Initialized);
                            MediationAdapterConsent(className);
                            break;
                    }
                }
            });
#if UNITY_IOS
        MobileAds.SetiOSAppPauseOnBackground(true);    
#endif
        }
        void MediationAdapterConsent(string AdapterClassname)
        {
            if (AdapterClassname.Contains("ExampleClass"))
            {
                isAdmobInitialized = true;
                loadBanner1();
                loadBanner2();
                loadBigBannerAD();
                loadRewardInt();
                loadRewardVideoAD();
            }
            if (AdapterClassname.Contains("MobileAds"))
            {
                isAdmobInitialized = true;
                loadBanner1();
                loadBanner2();
                loadBigBannerAD();
                loadRewardInt();
                loadRewardVideoAD();
                
            }
        }

        #region -----------------AppOpenAdFunction--------------------

        private bool IsAdAvailable
        {
            get
            {
                // COMPLETE: Consider ad expiration
                return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
            }
        }

        public void LoadAd()
        {
            Debug.Log("load");

            var adRequest = new AdRequest();

            // Load an app open ad for portrait orientation
            AppOpenAd.Load(ADMOB_ID.appOpenID, adRequest, ((appOpenAd, error) =>
            {
                if (error != null)
                {
                // Handle the error.
                return;
                }

            // App open ad is loaded
            ad = appOpenAd;
                Debug.Log("App open ad loaded");

            // COMPLETE: Keep track of time when the ad is loaded.
            loadTime = DateTime.UtcNow;
            }));
        }

        public void ShowAdIfAvailable()
        {

            Debug.Log("show");

            if (!IsAdAvailable || isShowingAd)
            {
                return;
            }

            RegisterEventHandlers(ad);

            ad.Show();
        }
        private void RegisterEventHandlers(AppOpenAd ad)
        {
            ad.OnAdPaid += (AdValue adValue) =>
            {

            };

            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Displayed app open ad");
                isShowingAd = true;
                Debug.Log("Recorded ad impression");
            };

            ad.OnAdClicked += () =>
            {

            };

            ad.OnAdFullScreenContentOpened += () =>
            {

            };

            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Closed app open ad");
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
                isShowingAd = false;
                LoadAd();
            };

            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {

            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
                LoadAd();
            };
        }

        #endregion

        #region ------------------- Ad Calling Functions--------------------

        public override void showAppOpenAD()
        {
            ShowAdIfAvailable();
        }

        #endregion

        #region IntersititialCodeBlock
        public override bool checkInterstitialAD()
        {
            if (this.Interstitial_High_Ecpm != null)
                return this.Interstitial_High_Ecpm.CanShowAd();
            else
                return false;
        }

        public override void showInterstitialAD()
        {
            if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
            {
                return;
            }

            if (Interstitial_HighEcpm)
            {
                if (this.Interstitial_High_Ecpm != null)
                {
                    if (this.Interstitial_High_Ecpm.CanShowAd())
                    {
                        if(HHG_appOpenHandler.Instance)
                            HHG_appOpenHandler.Instance.AdShowing = true;

                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Interstitial_WillDisplay_High_Ecpm);
                        GameAnalytics.NewAdEvent(GAAdAction.Show,GAAdType.Interstitial,"Admob","Admob_Interstitial");

                        this.Interstitial_High_Ecpm.Show();

                    }
                }
            }
            else if (UnityAds)
            {
                if (HHG_appOpenHandler.Instance)
                    HHG_appOpenHandler.Instance.AdShowing = true;

                HHG_Logger.HHG_LogEvent("unity_interstitial_loaded");
                GameAnalytics.NewAdEvent(GAAdAction.Show,GAAdType.Interstitial,"Unity","Unity_Interstitial");
                //  Advertisement.Show(ADMOB_ID.Unity_Interstitial_ID, this);
            }
        }
        public override void loadInterstitialAD()
        {
            if (!isAdmobInitialized || checkInterstitialAD() || interstitial_Status == AdsLoadingStatus.Loading || !PreferenceManager.GetAdsStatus())
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (Interstitial_HighEcpm)
                {
                    Int_Unity += loadInterstitialAD;
                    HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_LoadInterstitial_High_Ecpm);
                    var request = new AdRequest();
                    interstitial_Status = AdsLoadingStatus.Loading;

                    InterstitialAd.Load(ADMOB_ID.interstitialID, request, (InterstitialAd ad, LoadAdError error) =>
                    {
                        if (error != null)
                        {
                            HHG_Logger.HHG_LogEvent("Interstitial ad failed to load an ad with error : " + error);
                            interstitial_Status = AdsLoadingStatus.NotLoaded;
                            return;
                        }

                        if (ad == null)
                        {
                            HHG_Logger.HHG_LogEvent("Unexpected error: Interstitial load event fired with null ad and null error.");
                            interstitial_Status = AdsLoadingStatus.NotLoaded;
                            return;
                        }

                        HHG_Logger.HHG_LogEvent("Interstitial ad loaded with response : " + ad.GetResponseInfo());
                        this.Interstitial_High_Ecpm = ad;

                        BindIntersititialHighEcpmEvents();
                    });
                }
                else if (UnityAds)
                {
                    HHG_Logger.HHG_LogEvent("Load_Unity_Int");
                   // Advertisement.Load(ADMOB_ID.Unity_Interstitial_ID, this);
                }
            }
        }

        #endregion

        #region IntersititialEventCallBacks
        //HighEcpmEvents
        private void BindIntersititialHighEcpmEvents()
        {
            this.Interstitial_High_Ecpm.OnAdPaid += (AdValue adValue) =>
            {
                HHG_Logger.HHG_LogEvent(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            this.Interstitial_High_Ecpm.OnAdImpressionRecorded += () =>
            {
                HHG_Logger.HHG_LogEvent("Interstitial ad recorded an impression.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (Interstitial_HighEcpm)
                    {
                        interstitial_Status = AdsLoadingStatus.Loaded;

                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Interstitial_Loaded_High_Ecpm);
                        Int_Unity -= loadInterstitialAD;
                        Interstitial_HighEcpm = true;
                        UnityAds = false;
                    }

                });
            };
            this.Interstitial_High_Ecpm.OnAdClicked += () =>
            {
                HHG_Logger.HHG_LogEvent("Interstitial ad was clicked.");
            };
            this.Interstitial_High_Ecpm.OnAdFullScreenContentOpened += () =>
            {
                HHG_Logger.HHG_LogEvent("Interstitial ad full screen content opened.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    interstitial_Status = AdsLoadingStatus.NotLoaded;

                    if (Interstitial_HighEcpm)
                    {
                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Interstitial_Displayed_High_Ecpm);
                        Int_Unity -= loadInterstitialAD;
                    }
                });
            };
            this.Interstitial_High_Ecpm.OnAdFullScreenContentClosed += () =>
            {
                HHG_Logger.HHG_LogEvent("Interstitial ad full screen content closed.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Interstitial_Closed_High_Ecpm);
                    interstitial_Status = AdsLoadingStatus.NotLoaded;
                    if (Interstitial_HighEcpm)
                    {
                        this.Interstitial_High_Ecpm.Destroy();
                        Int_Unity -= loadInterstitialAD;
                        Interstitial_HighEcpm = true;
                        UnityAds = false;
                    }
                });
            };
            this.Interstitial_High_Ecpm.OnAdFullScreenContentFailed += (AdError error) =>
            {
                HHG_Logger.HHG_LogEvent("Interstitial ad failed to open full screen content with error : "
                    + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (Interstitial_HighEcpm)
                    {
                        interstitial_Status = AdsLoadingStatus.NoInventory;
                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_Interstitial_NoInventory_High_Ecpm);
                        Interstitial_HighEcpm = false;
                        UnityAds = true;
                        if (Int_Unity != null)
                            Int_Unity();
                    }
                });
            };
        }


        #endregion

        #region BannerCodeBlock
        public override bool checkSmallFirstBannerAD()
        {
            return isSmallBannerLoadedFirst;
        }
        public override void loadBanner1()
        {
            if (!PreferenceManager.GetAdsStatus() || checkSmallFirstBannerAD() || smallBanner_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                this.SmallBanner_L_Medium_Ecpm = new BannerView(ADMOB_ID.banner1, AdSize.Banner, banner1_Position);
                HHG_Logging.Log("FirstSmallBanner_M_Ecpm");
                BindSmallBannerFirstMediumEcpm();
                var request = new AdRequest();
                this.SmallBanner_L_Medium_Ecpm.LoadAd(request);
                this.SmallBanner_L_Medium_Ecpm.Hide();
            }
        }
        public override void hideBanner1()
        {
            if (this.SmallBanner_L_Medium_Ecpm != null)
            {
                this.SmallBanner_L_Medium_Ecpm.Hide();
                HHG_Logging.Log("Admob:smallBanner:Hide_M_Ecpm");
            }
        }
        public void ShowBanner()
        {
            showBanner1();
        }
        public override void showBanner1()
        {
            hideBanner1();

            try
            {
                if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
                {
                    return;
                }



                if (SmallBanner_L_Medium_Ecpm != null)
                {
                    HHG_Logging.Log("GR >> FirstBanner_Medium_Ecpm_Show");
                    this.SmallBanner_L_Medium_Ecpm.Hide();

                    this.SmallBanner_L_Medium_Ecpm.Show();
                    this.SmallBanner_L_Medium_Ecpm.SetPosition(banner1_Position);
                }
                else
                {
                    loadBanner1();
                }




            }
            catch (Exception error)
            {
                HHG_Logging.Log("Small Banner Error: " + error);
            }
        }
        private void BindSmallBannerFirstMediumEcpm()
        {
            this.SmallBanner_L_Medium_Ecpm.OnBannerAdLoaded += () =>
            {
                HHG_Logging.Log("Banner view loaded an ad with response : "
                    + this.SmallBanner_L_Medium_Ecpm.GetResponseInfo());

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    smallBanner_Status = AdsLoadingStatus.Loaded;
                    HHG_Logging.Log("FirstSmallBanner_M_Loaded_Ecpm");
                    isSmallBannerLoadedFirst = true;
                });
            };

            this.SmallBanner_L_Medium_Ecpm.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    smallBanner_Status = AdsLoadingStatus.NoInventory;
                    HHG_Logging.Log("FirstSmallBanner_M_Fail_Ecpm");
                    isSmallBannerLoadedFirst = false;
                });
            };

        }
        /// <summary>
        /// 2nd BannerCode
        /// </summary>

        private void BindSmallBannerSecondMediumEcpm()
        {
            this.SmallBanner_R_Medium_Ecpm.OnBannerAdLoaded += () =>
            {
                HHG_Logging.Log("Banner view loaded an ad with response : "
                    + this.SmallBanner_R_Medium_Ecpm.GetResponseInfo());


                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    small2ndBanner_Status = AdsLoadingStatus.Loaded;
                    HHG_Logging.Log("2ndSmallBanner_M_Loaded_Ecpm");
                    isSmallBannerLoadedSecond = true;

                });
            };

            this.SmallBanner_R_Medium_Ecpm.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                HHG_Logging.Log("Banner view failed to load an ad with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    small2ndBanner_Status = AdsLoadingStatus.NoInventory;


                    isSmallBannerLoadedSecond = false;


                    HHG_Logging.Log("2ndSmallBanner_M_Failed_Ecpm");

                });
            };

        }
        public override bool checkSmallSecondBannedAD()
        {
            return isSmallBannerLoadedSecond;
        }
        public override void loadBanner2()
        {
            if (!PreferenceManager.GetAdsStatus() || checkSmallSecondBannedAD() || small2ndBanner_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {

                this.SmallBanner_R_Medium_Ecpm = new BannerView(ADMOB_ID.banner2, AdSize.Banner, banner2_Position);


                HHG_Logging.Log("SecondSmallBanner_M_Ecpm");
                BindSmallBannerSecondMediumEcpm();
                var request = new AdRequest();
                this.SmallBanner_R_Medium_Ecpm.LoadAd(request);
                this.SmallBanner_R_Medium_Ecpm.Hide();

            }
        }
        public override void hideBanner2()
        {
            if (this.SmallBanner_R_Medium_Ecpm != null)
            {
                this.SmallBanner_R_Medium_Ecpm.Hide();
                HHG_Logging.Log("Admob:smallBanner:Hide_M_Ecpm");
            }
        }
        public override void showBanner2()
        {
            hideBanner2();
            try
            {
                if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
                {
                    return;
                }


                if (SmallBanner_R_Medium_Ecpm != null)
                {

                    this.SmallBanner_R_Medium_Ecpm.Hide();

                    this.SmallBanner_R_Medium_Ecpm.Show();
                    this.SmallBanner_R_Medium_Ecpm.SetPosition(banner2_Position);
                    HHG_Logging.Log("SecondBanner_Medium__Ecpm_Show");
                }
                else
                {
                    loadBanner2();
                }


            }
            catch (Exception error)
            {
                HHG_Logging.Log("Small Banner Error: " + error);
            }
        }

        #endregion

        #region MediumBannerCodeBlocks
        public override bool checkBigBannerAD()
        {
            return isMediumBannerLoaded;
        }

        public override void loadBigBannerAD()
        {
            if (!PreferenceManager.GetAdsStatus() || checkBigBannerAD() || bigBanner_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {

                HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_MediumBanner_Load_MediumEcpm);
                this.MediumBannerMediumEcpm = new BannerView(ADMOB_ID.bigBannerID, AdSize.MediumRectangle, AdPosition.BottomLeft);
                BindMediumBannerEvents_M_Ecpm();
                var request = new AdRequest();
                this.MediumBannerMediumEcpm.LoadAd(request);
                this.MediumBannerMediumEcpm.Hide();

            }

        }
        public override void showBigBannerAD(AdPosition pos)
        {
            try
            {
                if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
                {
                    return;
                }


                HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_MediumBanner_Show_MediumEcpm);
                if (MediumBannerMediumEcpm != null)
                {

                    this.MediumBannerMediumEcpm.Hide();
                    this.MediumBannerMediumEcpm.Show();
                    this.MediumBannerMediumEcpm.SetPosition(pos);
                }

            }
            catch (Exception e)
            {

                HHG_Logging.Log("Medium Banner Error: " + e);
            }
        }
        public override void hideBigBanner()
        {

            if (this.MediumBannerMediumEcpm != null)
            {
                HHG_Logging.Log("Admob:mediumBanner:Hide_M_Ecpm");
                this.MediumBannerMediumEcpm.Hide();
            }

        }

        #endregion

        #region MediumBannerCallBack Handlers

        //MediumBanner2
        private void BindMediumBannerEvents_M_Ecpm()
        {
            this.MediumBannerMediumEcpm.OnBannerAdLoaded += () =>
            {
                HHG_Logging.Log("Banner view loaded an ad with response : "
                    + this.MediumBannerMediumEcpm.GetResponseInfo());

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    bigBanner_Status = AdsLoadingStatus.Loaded;
                    HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_MediumBanner_Loaded_MediumEcpm);
                    loadBigBannerAD();
                    isMediumBannerLoaded = true;

                });
            };

            this.MediumBannerMediumEcpm.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                HHG_Logging.Log("Banner view failed to load an ad with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    bigBanner_Status = AdsLoadingStatus.NotLoaded;
                    HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_MediumBanner_NoInventory_MediumEcpm);

                    isMediumBannerLoaded = false;
                });
            };

            this.MediumBannerMediumEcpm.OnAdPaid += (AdValue adValue) =>
            {
                HHG_Logging.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            this.MediumBannerMediumEcpm.OnAdImpressionRecorded += () =>
            {
                HHG_Logging.Log("Banner view recorded an impression.");
            };

            this.MediumBannerMediumEcpm.OnAdClicked += () =>
            {
                HHG_Logging.Log("Banner view was clicked.");
            };

            this.MediumBannerMediumEcpm.OnAdFullScreenContentOpened += () =>
            {
                HHG_Logging.Log("Banner view full screen content opened.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_MediumBanner_Displayed_MediumEcpm);

                });
            };

            this.MediumBannerMediumEcpm.OnAdFullScreenContentClosed += () =>
            {
                HHG_Logging.Log("Banner view full screen content closed.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
            };
        }


        #endregion

        #region RewardedVideoCodeBlock
        public override void loadRewardVideoAD()
        {
            if (!isAdmobInitialized || checkRewardAD() || rewardADs_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (RewardVideo_High_Ecpm)
                {
                    RewardVideo_Unity += loadRewardVideoAD;
                    HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_LoadRewardedVideo_High_Ecpm);
                    var request = new AdRequest();
                    rewardADs_Status = AdsLoadingStatus.Loading;
                    RewardedAd.Load(ADMOB_ID.rewardedVideo, request, (RewardedAd ad, LoadAdError error) =>
                    {
                        if (error != null)
                        {
                            HHG_Logging.Log("Rewarded ad failed to load an ad with error : " + error);
                            return;
                        }

                        if (ad == null)
                        {
                            HHG_Logging.Log("Unexpected error: Rewarded load event fired with null ad and null error.");
                            return;
                        }

                        HHG_Logging.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
                        this.rewardBasedVideo = ad;
                        BindRewardedEvents_H_Ecpm();
                    });
                }
                else if (UnityRewarded)
                {
                    //Advertisement.Load(ADMOB_ID.Unity_RewardedVideo);
                }
            }
        }
        public override bool checkRewardAD()
        {
            if (this.rewardBasedVideo != null)
                return this.rewardBasedVideo.CanShowAd();
            else
                return false;
        }
        public override void showRewardVideo(RewardUserDelegate _delegate)
        {
            if (RewardVideo_High_Ecpm)
            {
                NotifyReward = _delegate;
                HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_ShowRewardedVideo_High_Ecpm);

                if (this.rewardBasedVideo != null)
                {
                    if (this.rewardBasedVideo.CanShowAd())
                    {

                        if (HHG_appOpenHandler.Instance)
                            HHG_appOpenHandler.Instance.AdShowing = true;

                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedVideo_WillDisplay_High_Ecpm);
                        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "Admob", "Rewarded_Vedio");
                        this.rewardBasedVideo.Show((Reward reward) =>
                        {
                            HHG_Logging.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                                                    reward.Amount,
                                                    reward.Type));
                        });
                    }
                }
                else
                {
                    ToastHelper.ShowToast("Ads Not AvailAble");
                }
            }
            else if (UnityRewarded)
            {
                if (HHG_appOpenHandler.Instance)
                    HHG_appOpenHandler.Instance.AdShowing = true;

                NotifyReward = _delegate;
                // Advertisement.Show(ADMOB_ID.Unity_RewardedVideo, this);
            }
            else
            {
                ToastHelper.ShowToast("Ads Not AvailAble");
            }
        }

        #endregion

        #region RewardedVideoEvents
        //***** Rewarded Events *****//
        private void BindRewardedEvents_H_Ecpm()
        {
            rewardBasedVideo.OnAdPaid += (AdValue adValue) =>
            {

            };

            rewardBasedVideo.OnAdImpressionRecorded += () =>
            {
                HHG_Logging.Log("Rewarded ad recorded an impression.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        RewardVideo_Unity -= loadRewardVideoAD;
                        rewardADs_Status = AdsLoadingStatus.Loaded;
                        UnityRewarded = false;
                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedVideo_Loaded_High_Ecpm);

                    }
                });
            };

            rewardBasedVideo.OnAdClicked += () =>
            {
                HHG_Logging.Log("Rewarded ad was clicked.");
            };

            rewardBasedVideo.OnAdFullScreenContentOpened += () =>
            {
                HHG_Logging.Log("Rewarded ad full screen content opened.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        RewardVideo_Unity -= loadRewardVideoAD;
                        rewardADs_Status = AdsLoadingStatus.NotLoaded;

                        if (NotifyReward != null)
                            NotifyReward();

                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedVideo_Displayed_High_Ecpm);
                    }
                });
            };

            rewardBasedVideo.OnAdFullScreenContentClosed += () =>
            {
                HHG_Logging.Log("Rewarded ad full screen content closed.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        RewardVideo_Unity -= loadRewardVideoAD;
                        rewardADs_Status = AdsLoadingStatus.NotLoaded;
                        HHG_Logging.Log("GR >> Admob:rad:Closed_H_Ecpm");
                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedVideo_Closed_High_Ecpm);
                        loadRewardVideoAD();
                    }
                });
            };

            rewardBasedVideo.OnAdFullScreenContentFailed += (AdError error) =>
            {
                HHG_Logging.Log("Rewarded ad failed to open full screen content with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        rewardADs_Status = AdsLoadingStatus.NoInventory;
                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedVideo_NoInventory_High_Ecpm);
                        RewardVideo_High_Ecpm = false;

                        UnityRewarded = true;
                        if (RewardVideo_Unity != null)
                        {
                            RewardVideo_Unity();
                        }
                    }
                });
            };
        }

        #endregion

        #region RewardedInterstial
        public override void loadRewardInt()
        {
            if (!isAdmobInitialized || checkRewardIntAD() || rewardInterstitial_Status == AdsLoadingStatus.Loading)
            {
                return;
            }

            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_LoadRewardedInterstitialAd_H_ECPM);
                var request = new AdRequest();
                rewardInterstitial_Status = AdsLoadingStatus.Loading;

                RewardedInterstitialAd.Load(ADMOB_ID.rewardInterstitial, request, (RewardedInterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null)
                    {
                        HHG_Logging.Log("Rewarded interstitial ad failed to load an ad with error : " + error);
                        return;
                    }

                    if (ad == null)
                    {
                        HHG_Logging.Log("Unexpected error: Rewarded interstitial load event fired with null ad and null error.");
                        return;
                    }

                    HHG_Logging.Log("Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());
                    rewardedInterstitialAd = ad;
                    RegisterEventHandlers(ad);
                });
            }
        }

        public override void showRewardInt(RewardUserDelegate _delegate)
        {
            NotifyReward = _delegate;
            HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_ShowRewardedInterstitialAd_H_ECPM);

            if (this.rewardedInterstitialAd != null)
            {
                if (rewardedInterstitialHighECPMLoaded)
                {

                    if (HHG_appOpenHandler.Instance)
                        HHG_appOpenHandler.Instance.AdShowing = true;

                    this.rewardedInterstitialAd.Show(userEarnedRewardCallback);
                    GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "Admob", "Rewarded_Inter");
                }
            }
            else
            {
                ToastHelper.ShowToast("Ads Not AvailAble");
            }
        }

        private void userEarnedRewardCallback(Reward reward)
        {

        }


        public override bool checkRewardIntAD()
        {
            if (this.rewardedInterstitialAd != null)
            {
                if (rewardedInterstitialHighECPMLoaded)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region RewardedInterstitialCallbackHandler

        ///////// Rewarded Interstitial High ECPM Callbacks //////////
        protected void RegisterEventHandlers(RewardedInterstitialAd ad)
        {
            rewardedInterstitialHighECPMLoaded = true;

            ad.OnAdPaid += (AdValue adValue) =>
            {

            };
            ad.OnAdImpressionRecorded += () =>
            {
                HHG_Logging.Log("Rewarded interstitial ad recorded an impression.");
            };
            ad.OnAdClicked += () =>
            {
                HHG_Logging.Log("Rewarded interstitial ad was clicked.");
            };
            ad.OnAdFullScreenContentOpened += () =>
            {
                HHG_Logging.Log("Rewarded interstitial ad has presented.");
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    {
                        rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;

                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedInterstitialAdDisplayed_H_ECPM);
                    }
                });
            };
            ad.OnAdFullScreenContentClosed += () =>
            {
                HHG_Logging.Log("Rewarded interstitial ad has dismissed presentation.");
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    {
                        rewardedInterstitialHighECPMLoaded = false;
                        rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;
                        HHG_Logger.HHG_LogSender(HHG_AdmobEvents.HHG_RewardedInterstitialAdClosed_H_ECPM);
                        NotifyReward();
                        loadRewardInt();
                    }
                });
            };
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    {
                        rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;
                        HHG_Logging.Log("Admob:riad:FailedToShow:HCPM");
                    }
                });
            };
        }

        #endregion

        #region UnityCallBack
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Enter1");
            // Optionally execute code if the Ad Unit successfully loads content.
            if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.Loaded;
                UnityAds = true;
                Interstitial_HighEcpm = false;


            }
            else if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
            {
                rewardADs_Status = AdsLoadingStatus.Loaded;
                RewardVideo_High_Ecpm = false;
                UnityRewarded = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId,  string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {ToString()} - {message}");
            if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.Loaded;
                UnityAds = true;
                Interstitial_HighEcpm = false;
            }
            else if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
            {
                rewardADs_Status = AdsLoadingStatus.Loaded;
                RewardVideo_High_Ecpm = false;
                UnityRewarded = true;
                Debug.Log("Ad_Failed");
            }
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string adUnitId,string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {ToString()} - {message}");
            if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.Loaded;
                UnityAds = false;
                Interstitial_HighEcpm = true;


            }
            else if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
            {
                rewardADs_Status = AdsLoadingStatus.Loaded;
                RewardVideo_High_Ecpm = true;

                UnityRewarded = false;

            }
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
        public void OnUnityAdsShowComplete(string adUnitId)
        {


            //  ADmobInterstial = true;
            if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.NotLoaded;
                Interstitial_HighEcpm = true;

                UnityAds = false;
                Debug.Log("Ad_completed");
            }
            else
            if (adUnitId.Equals(ADMOB_ID.Unity_RewardedVideo) )
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
                if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
                {
                    RewardVideo_High_Ecpm = true;

                    UnityRewarded = false;
                    NotifyReward();
                    Debug.Log("Ad_completed");
                }
                // Load another ad:
                //Advertisement.Load(ADMOB_ID.Unity_RewardedVideo, this);
            }
        }
        #endregion
    }
}

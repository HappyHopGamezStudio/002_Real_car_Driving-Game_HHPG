
using UnityEngine;

namespace HHG_Mediation
{
    public static class HHG_Logging
    {
        static public void Log(object message)
        {
            if (!HHG_Admob.Logs)
                UnityEngine.Debug.Log(message);
        }
    }
    public class HHG_Logger : MonoBehaviour
    {
        public static void HHG_LogSender(HHG_AdmobEvents Log)
        {
            switch (Log)
            {
                case HHG_AdmobEvents.HHG_Initializing:

                    HHG_LogEvent("HHG_AdmobInitializing");
                    break;
                case HHG_AdmobEvents.HHG_Initialized:

                    HHG_LogEvent("HHG_AdmobInitialized");
                    break;
                case HHG_AdmobEvents.HHG_LoadInterstitial_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_LoadRequest_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadInterstitial_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_LoadRequest_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadIntersitiatial_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_LoadRequest_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowInterstitial_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_ShowCall_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowInterstitial_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_ShowCall_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowIntersititial_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_ShowCall_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_WillDisplay_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_WillDisplay_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_WillDisplay_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_WillDisplay_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_WillDisplay_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_WillDisplay_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Displayed_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Displayed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstilial_Displayed_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Displayed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstital_Displayed_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Displayed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_NoInventory_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_NoInventery_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstilial_NoInventory_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_NoInventery_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_NoInventory_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_NoInventery_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Closed_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Closed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Closed_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Closed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Closed_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Closed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Loaded_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Loaded_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Loaded_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Loaded_M_Ecpm");

                    break;
                case HHG_AdmobEvents.HHG_Interstitial_Loaded_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_iAd_Loaded_L_Ecpm");

                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Load_HighEcpm:
                    HHG_LogEvent("HHG_Admob_AB_LoadRequest_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Load_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_AB_LoadRequest_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Load_LowEcpm:
                    HHG_LogEvent("HHG_Admob_AB_LoadRequest_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Show_HighEcpm:
                    HHG_LogEvent("HHG_Admob_AB_ShowCall_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Show_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_AB_ShowCall_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Show_LowEcpm:
                    HHG_LogEvent("HHG_Admob_AB_ShowCall_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Loaded_HighEcpm:
                    HHG_LogEvent("HHG_Admob_AB_Loaded_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBannner_Loaded_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_AB_Loaded_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Loaded_LowEcpm:
                    HHG_LogEvent("HHG_Admob_AB_Loaded_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_NoInventory_HighEcpm:
                    HHG_LogEvent("HHG_Admob_AB_NoInventory_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_NoInventory_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_AB_NoInventory_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_NoInventory_LowEcpm:
                    HHG_LogEvent("HHG_Admob_AB_NoInventory_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Displayed_HighEcpm:
                    HHG_LogEvent("HHG_Admob_AB_Displayed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Displayed_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_AB_Displayed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_SmallBanner_Displayed_LowEcpm:
                    HHG_LogEvent("HHG_Admob_AB_Displayed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Load_HighEcpm:
                    HHG_LogEvent("HHG_Admob_MB_LoadRequest_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Load_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_MB_LoadRequest_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Load_LowEcpm:
                    HHG_LogEvent("HHG_Admob_MB_LoadRequest_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Show_HighEcpm:
                    HHG_LogEvent("HHG_Admob_MB_ShowCall_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Show_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_MB_ShowCall_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Show_LowEcpm:
                    HHG_LogEvent("HHG_Admob_MB_ShowCall_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Loaded_HighEcpm:
                    HHG_LogEvent("HHG_Admob_MB_Loaded_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Loaded_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_MB_Loaded_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Loaded_LowEcpm:
                    HHG_LogEvent("HHG_Admob_MB_Loaded_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_NoInventory_HighEcpm:
                    HHG_LogEvent("HHG_Admob_MB_NoInventory_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_NoInventory_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_MB_NoInventory_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_NoInventory_LowEcpm:
                    HHG_LogEvent("HHG_Admob_MB_NoInventory_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Displayed_HighEcpm:
                    HHG_LogEvent("HHG_Admob_MB_Displayed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Displayed_MediumEcpm:
                    HHG_LogEvent("HHG_Admob_MB_Displayed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_MediumBanner_Displayed_LowEcpm:
                    HHG_LogEvent("HHG_Admob_MB_Displayed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadRewardedVideo_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_LoadRequest_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadRewardedVideo_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_LoadRequest_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadRewardedVideo_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_LoadRequest_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowRewardedVideo_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_ShowCall_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowRewardedVideo_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_ShowCall_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowRewardedVideo_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_ShowCall_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_WillDisplay_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_WillDisplay_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_WillDisplay_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_WillDisplay_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_WillDisplay_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_WillDisplay_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Displayed_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Displayed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Displayed_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Displayed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Displayed_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Displayed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_NoInventory_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_NoInventery_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_NoInventory_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_NoInventery_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_NoInventory_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_NoInventery_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Closed_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Closed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Closed_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Closed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Closed_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Closed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Loaded_High_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Loaded_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Loaded_Medium_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Loaded_M_Ecpm");

                    break;
                case HHG_AdmobEvents.HHG_RewardedVideo_Loaded_Low_Ecpm:
                    HHG_LogEvent("HHG_Admob_rAd_Loaded_L_Ecpm");

                    break;

                case HHG_AdmobEvents.HHG_LoadRewardedInterstitialAd_H_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_LoadRequest_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadRewardedInterstitialAd_M_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_LoadRequest_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_LoadRewardedInterstitialAd_L_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_LoadRequest_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowRewardedInterstitialAd_H_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_ShowCall_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowRewardedInterstitialAd_M_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_ShowCall_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_ShowRewardedInterstitialAd_L_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_ShowCall_L_Ecpm");
                    break;

                case HHG_AdmobEvents.HHG_RewardedInterstitialAdDisplayed_H_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_Displayed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialAdDisplayed_M_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_Displayed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialAdDisplayed_L_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_Displayed_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialNoInventory_H_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_NoInventery_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialNoInventory_M_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_NoInventery_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialNoInventory_L_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_NoInventery_L_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialAdClosed_H_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_Closed_H_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialAdClosed_M_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_Closed_M_Ecpm");
                    break;
                case HHG_AdmobEvents.HHG_RewardedInterstitialAdClosed_L_ECPM:
                    HHG_LogEvent("HHG_Admob_riAd_Closed_L_Ecpm");
                    break;



                default:
                    break;
            }
        }


        public static void HHG_LogEvent(string log)
        {
            HHG_Logging.Log("HHG_ " + log);
          //  Firebase.Analytics.FirebaseAnalytics.LogEvent(log);
            //FB.LogAppEvent(eventName);
        }

        public static void HHG_MissionOrLevelStartedEventLog(string GameMode, int LevelNumber)
        {
            HHG_Logging.Log("HHG_ LevelStarted_" + GameMode + "_Level_" + LevelNumber.ToString());

            //Firebase.Analytics.FirebaseAnalytics.LogEvent("HHG_LevelStarted_" + GameMode + "_Level_" + LevelNumber.ToString());

        }
        public static void HHG_MissionOrLevelFailEventLog(string GameMode, int LevelNumber)
        {
            HHG_Logging.Log("HHG_ LevelFail_" + GameMode + "_Level_Number_" + LevelNumber.ToString());

            //Firebase.Analytics.FirebaseAnalytics.LogEvent("HHG_LevelFail_" + GameMode + "_Level_" + LevelNumber.ToString());

        }

        public static void HHG_MissionOrLevelCompletedEventLog(string GameMode, int LevelNumber)
        {
            HHG_Logging.Log("HHG_ LevelComplete_" + GameMode + "_Level_" + LevelNumber);

            //Firebase.Analytics.FirebaseAnalytics.LogEvent("HHG_LevelComplete_" + GameMode + "_Level_" + LevelNumber);


        }
    }

}

using UnityEngine;
using System.Collections;

public class PrefsManager : MonoBehaviour
{
	public const string level = "LEVEL";
    static string GameMode = "GameMode";
	public const string selectedPlayer = "SELECTEDPlayer";
    public const string selectedPlayerDirt = "SELECTEDPlayerDirt";
    public const string coinsEarned = "COINSEARNED";
    public const string ShowAds = "ShowAds";
    public const string Sounds = "sounds";
    public const string Music = "Music";
	public const string unlockLevel = "UNLOCKLEVEL";
    public const string unlockLevelSnow = "UNLOCKLEVELSNOW";
    public const string unlockLevelDesert = "UNLOCKLEVELDESERT";
//    public const string gameMode = "Mode";
    public const string gameQuality = "Quality";
    public const string controlls = "Controls";
    
    public const string Modes = "selectMode";
    public const string PrivacyPolicy = "policy";
    public const string FirstTimeGame = "FirstTimeGame";
    public const string UnlockLastjeep = "UnlockLastjeep";
    public const string ComeForSelection = "ComeForSelection";
    public const string ComeForModeSelection = "ComeForModeSelection";
    public const string CurrentLevel = "CURRENT_LEVEL";
    public const string CurrentPlayer = "CURRENT_PLAYER";
    public const string UnlockedLevel = "UNLOCKED_LEVEL";
    public const string Health = "Health";
    static string TestDrivePlayer = "Testdrive";
    static string NosCount = "NosCount";
    static string LoadInt = "LoadInt";

    // Use this for initialization
    
    
    
    
    
    
    
    
    
    public static void Sethealth(string Name,float currentPlayer)
    {
        PlayerPrefs.SetFloat(Name+Health, currentPlayer);
    }
    public static float Gethealth(string Name)
    {
        return PlayerPrefs.GetFloat(Name+Health, 100);
    }

    
    
    
    
    public static int GetInterInt()
    {
        return PlayerPrefs.GetInt(LoadInt, 1);
        //return 1;
    }

    public static void SetInterInt(int quality)
    {
        PlayerPrefs.SetInt(LoadInt, quality);
    }
    
    /*
    public static void SetFuel(float currentPlayer)
    {
       // PlayerPrefs.SetFloat(FuelCapacity, currentPlayer);
    }
    public static float GetFuel()
    {
       // return PlayerPrefs.GetFloat(FuelCapacity, 100);
    }
    */

    public static void SetNosCount(int value)
    {
        PlayerPrefs.SetInt(NosCount, value);
    }
    public static int GetNosCount()
    {
        return PlayerPrefs.GetInt(NosCount, 2);
    }

    public static void SetPlayer(int currentPlayer)
    {
        PlayerPrefs.SetInt(CurrentPlayer, currentPlayer);
    }
    public static int GetPlayer()
    {
        return PlayerPrefs.GetInt(CurrentPlayer, 0);
    }
   

    public static void SetCurrentLevel(int currentPlayer)
    {
        PlayerPrefs.SetInt(CurrentLevel, currentPlayer);
    }
    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(CurrentLevel, 0);
    }
    public static void SetLockedLevel(int currentPlayer)
    {
        PlayerPrefs.SetInt(UnlockedLevel, currentPlayer);
    }
    public static int GetLockedLevel()
    {
        return PlayerPrefs.GetInt(UnlockedLevel, 0);
    }


    public static int GetComeForModeSelection()
    {
        return PlayerPrefs.GetInt(ComeForModeSelection, 0);
    }

    public static void SetComeForModeSelection(int PlayerVal)
    {
        PlayerPrefs.SetInt(ComeForModeSelection, PlayerVal);
    }

    /// player selection Handler
    public static int GetComeForSelection()
    {
        return PlayerPrefs.GetInt(ComeForSelection, 0);
    }

    public static void SetComeForSelection(int PlayerVal)
    {
        PlayerPrefs.SetInt(ComeForSelection, PlayerVal);
    }


    public static void UnLockAllVehicle(int totalVehicle)
    {

        for (int i = 1; i < totalVehicle; i++)
        {
            PlayerPrefs.SetInt("player" + i, 1);
        }
        SetLastJeepUnlock(totalVehicle - 1);
        Debug.Log("Call Unlock All vehicle");

    }


    /// player selection Handler
    public static int GetPlayerState(int playerNumber)
    {
        return PlayerPrefs.GetInt("player" + playerNumber, 0);
    }

    public static void SetPlayerState(int playerNumber, int PlayerVal)
    {
        PlayerPrefs.SetInt("player" + playerNumber, PlayerVal);
        SetLastJeepUnlock(playerNumber);
    }





    /// player selection Handler
    public static int GetLastJeepUnlock()
    {
        return PlayerPrefs.GetInt(UnlockLastjeep, 0);
    }

    public static void SetLastJeepUnlock(int PlayerVal)
    {
        PlayerPrefs.SetInt(UnlockLastjeep, PlayerVal);
    }



    public static int GetFirstTimeGame()
    {
        return PlayerPrefs.GetInt(FirstTimeGame, 0);
        //return 1;
    }

    public static void SetFirstTimeGame(int value)
    {
        PlayerPrefs.SetInt(FirstTimeGame, value);
    }
    public static int GetPrivacyPolicy()
    {
        return PlayerPrefs.GetInt(PrivacyPolicy, 0);
        //return 1;
    }

    public static void SetPrivacyPolicy(int value)
    {
        PlayerPrefs.SetInt(PrivacyPolicy, value);
    }


    public static int GetLevelMode()
    {
        return PlayerPrefs.GetInt(Modes, 1);
        //return 1;
    }

    public static void SetLevelMode(int quality)
    {
        PlayerPrefs.SetInt(Modes, quality);
    }


    public static int GetControlls()
    {
        return PlayerPrefs.GetInt(controlls, 0);
        //return 1;
    }

    public static void SetControlls(int quality)
    {
        PlayerPrefs.SetInt(controlls, quality);
    }
    
  



    public static float GetMusic ()
    {
        return PlayerPrefs.GetFloat (Music, 1);
    }

    public static void SetMusic (float AdValue)
    {
        PlayerPrefs.SetFloat (Music, AdValue);
    }

    public static float GetSound ()
	{
		return PlayerPrefs.GetFloat (Sounds, 1);
	}

	public static void SetSound (float AdValue)
	{
		PlayerPrefs.SetFloat (Sounds, AdValue);
	}


//    public static string GetGameMode()
//    {
//        return PlayerPrefs.GetString(gameMode, "free");
//       // return "free";
//    }
//
//    public static void SetGameMode(string gamemode)
//    {
//        PlayerPrefs.SetString(gameMode, gamemode);
//    }
    public static string GetGameMode()
    {

        return PlayerPrefs.GetString(GameMode, "Free");
    }
    public static void SetGameMode(string value)
    {

        PlayerPrefs.SetString(GameMode, value);
    }

    public static int GetGameQuality()
    {
        return PlayerPrefs.GetInt(gameQuality, 2);
        //return 1;
    }

    public static void SetGameQuality(int quality)
    {
        PlayerPrefs.SetInt(gameQuality, quality);
    }


    public static int GetLevelLocking ()
	{
		return PlayerPrefs.GetInt (UnlockedLevel, 1);
		//return 15;
	}
    public static int GetSnowLevelLocking()
    {
        return PlayerPrefs.GetInt(unlockLevelSnow, 1);
        //return 15;
    }

    public static void SetSnowLevelLocking(int AdValue)
    {
        PlayerPrefs.SetInt(unlockLevelSnow, AdValue);
    }

    public static int GetDesertLevelLocking()
    {
        return PlayerPrefs.GetInt(unlockLevelDesert, 1);
        //return 15;
    }

    public static void SetDesertLevelLocking(int AdValue)
    {
        PlayerPrefs.SetInt(unlockLevelDesert, AdValue);
    }
    public static void SetLevelLocking (int AdValue)
	{
		PlayerPrefs.SetInt (UnlockedLevel, AdValue);
	}

	public static int GetAdValue ()
	{
		return PlayerPrefs.GetInt (ShowAds, 0);
	}

	public static void SetAdValue (int AdValue)
	{
		PlayerPrefs.SetInt (ShowAds, AdValue);
	}



    public static int GetDirtSelectedPlayerValue()
    {
        return PlayerPrefs.GetInt(selectedPlayerDirt, 0);
        //return 4;
    }

    public static void SetDirtSelectedPlayerValue(int selectedPlayerVal)
    {
        PlayerPrefs.SetInt(selectedPlayerDirt, selectedPlayerVal);
    }




    public static int GetSelectedPlayerValue ()
	{
		return PlayerPrefs.GetInt (selectedPlayer, 0);
		//return 4;
	}

	public static void SetSelectedPlayerValue (int selectedPlayerVal)
	{
		PlayerPrefs.SetInt (selectedPlayer, selectedPlayerVal);	
	}

	public static int GetCoinsValue ()
	{
		return PlayerPrefs.GetInt (coinsEarned, 0);
	}

	public static void SetCoinsValue (int coinsValue)
	{
		PlayerPrefs.SetInt (coinsEarned, coinsValue);
	}


    public static bool isTestDrive = false;
    
    public static int GetTestDrive()
    {
        return PlayerPrefs.GetInt (TestDrivePlayer, 0);
        //return 4;
    }

    public static void SetTestDrive (int selectedTestPlayerVal)
    {
        PlayerPrefs.SetInt (TestDrivePlayer, selectedTestPlayerVal);	
    }

}

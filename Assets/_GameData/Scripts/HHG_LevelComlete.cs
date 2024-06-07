using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class HHG_LevelComlete : MonoBehaviour
{

	public Text allcoin,freecoin,rewardtext,timetocompleteLevel;
	public GameObject LOADING,nextbutton, starobj;
	public AudioSource audio;


	void OnEnable ()
	{
		if (PrefsManager.GetCurrentLevel () >= 46)
			nextbutton.SetActive (false);
	//	reward =LevelManager.instace.Reward[PrefsManager.GetCurrentLevel()-1];
		Destroy(audio);
		allcoin.text = PrefsManager.GetCoinsValue().ToString();
		if (PrefsManager.GetLevelMode() == 0)
		{
			Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
			if (PrefsManager.GetCurrentLevel() >= PrefsManager.GetLevelLocking())
			{
				PrefsManager.SetLevelLocking(PrefsManager.GetLevelLocking() + 1);

			}
			Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
		}
		else if (PrefsManager.GetLevelMode() == 1)
		{
			Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
			if ((PrefsManager.GetCurrentLevel()) >= PrefsManager.GetSnowLevelLocking())
			{
				PrefsManager.SetSnowLevelLocking(PrefsManager.GetSnowLevelLocking() + 1);

			}
			Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
		}
        else if (PrefsManager.GetLevelMode() == 2)
        {
	        Debug.Log("ThirdMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetDesertLevelLocking());
            if ((PrefsManager.GetCurrentLevel() ) >= PrefsManager.GetDesertLevelLocking())
            {
                PrefsManager.SetDesertLevelLocking(PrefsManager.GetDesertLevelLocking() + 1);

            }
        }



        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.LevelComplete);
		HHG_SoundManager.Instance.OffPlayTimmerSound() ;

	}




	public void NextButtonEvent ()
	{
		Destroy(starobj);
		//starobj.SetActive(false);
		LOADING.SetActive (true);
		PrefsManager.SetCurrentLevel (PrefsManager.GetCurrentLevel()+1);
		
		gameObject.SetActive (false);
        if (PrefsManager.GetLevelMode() == 1 && PrefsManager.GetCurrentLevel() == 37)
        {
            PrefsManager.SetComeForModeSelection(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
        else { 
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
        Time.timeScale = 1;
		//UIManagerObject.instance.EnableEndGameAd ();
	}

	public void ReplayButtonEvent ()
	{
		Destroy(starobj);
		LOADING.SetActive (true);
		PrefsManager.SetCurrentLevel (PrefsManager.GetCurrentLevel ());
		gameObject.SetActive (false);
	   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
        //UIManagerObject.instance.EnableEndGameAd ();
        Time.timeScale = 1;
    }

	public void RedirectHome()
	{
		Time.timeScale = 1;
		Destroy(starobj);
		LOADING.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
	

		//EnableEndGameAd ();
	}
}

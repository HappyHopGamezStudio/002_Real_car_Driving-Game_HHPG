using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HHG_TimeController : MonoBehaviour
{


    public Text timecounterText, TimeUptext, timeOnLevelComplete;
    public int[] timeToCompleteLevels;
    private float timeToCompleteLevel;
    public static bool isTimeOver;
    public bool isTimerOn = false;
    public static bool isGamePaused;

    public int timeForFreeCoin;

    bool checkIt, isplayerwarning = false;
    public static bool isAttackMode;

    int remainder;
    public GameObject getTimeButton;
    void Awake()
    {
        //		MoPubAds.hideBanner ();
    }

    public delegate void TimeOver();
    public static event TimeOver OnTimeOver;

    void Start()
    {
        remainder = PrefsManager.GetCurrentLevel() - 1;
        isAttackMode = false;
        //		GetComponent<AudioSource> ().loop = true;

        timeToCompleteLevel = timeToCompleteLevels[remainder];
        checkIt = false;

        if (PrefsManager.GetGameMode() == "free")
        {
            timeToCompleteLevel = timeForFreeCoin;
        }

        isTimeOver = false;
        isGamePaused = false;
        //		levelnum.text = "" + (GlobalScripts.CurrLevelIndex+1);
    }

    public void starton_failtime()
    {
        isAttackMode = false;
        timeToCompleteLevel = 10f;

        checkIt = false;


        isTimeOver = false;
        isGamePaused = false;
    }
    private float totalTime;
    int Minutes = 0;
    int Seconds = 0;
    int DivisionValue = 60;
    void Update()
    {

         Minutes = (int)Math.Abs(timeToCompleteLevel / DivisionValue);
        Seconds = (int)timeToCompleteLevel % DivisionValue;

        if (timeToCompleteLevel >= 0 && !isGamePaused)
        {
            timeToCompleteLevel -= Time.deltaTime;
            totalTime += Time.deltaTime;

        }
        if (timeToCompleteLevel <= timeToCompleteLevels[remainder] - 10)
        {
            isAttackMode = true;
            //Debug.Log ("Eemy can attack");
        }
        if (timeToCompleteLevel <= 15 && !isplayerwarning && PrefsManager.GetGameMode()!="free" && !isrewardAlready)
        {
            timecounterText.color = Color.red;
          //  timecounterText.GetComponent<Animator>().enabled = true;
            //if(audio!=null)
            //audio.enabled = true;
           // getTimeButton.SetActive(true);

           // if (!UIManagerObject.instance.isAnyPanel)
            {
                
              //  SoundManager.Instance.PlayTimmerSound();
              //  isplayerwarning = true;
            }
           // else
            {
                //isGamePaused = true;
            }
          
            //audio.Play();
        }

        //		Debug.Log ("time"+TimeController.isTimeOver+"-"+timeToCompleteLevel);
        if (timeToCompleteLevel < 0.0F && !isTimeOver)
        {


            //			GameObject.FindWithTag("GameController").GetComponent<MissionController>().onfailcondition();
            TimeUptext.gameObject.SetActive(true);
            TimeUptext.text = "Time's Up You Are Late ";
            //if (audio != null) 
            {
                //	audio.enabled = false;
                //audio.Stop();
                //Destroy(audio);

            }
            if (PrefsManager.GetGameMode() == "free")
            {
                HHG_UiManager.instance.ShowFail();

            }
            else
            {
                HHG_UiManager.instance.ShowFail();
            }

            HHG_SoundManager.Instance.OffPlayTimmerSound();


            isTimeOver = true;
          
        }
        timecounterText.text = "0" + Minutes.ToString() + ":";

        if (Seconds < 10)
        {

            timecounterText.text = timecounterText.text + "0" + Seconds.ToString();

        }
        else
        {
            timecounterText.text = timecounterText.text + Seconds.ToString();
        }
        //if (!UIManagerObject.instance.isCompleteLevel)
        {
            showseconds = (int)totalTime % DivisionValue;
            showmint = (int)totalTime / DivisionValue;
            if (showseconds < 10)
                valueShow = "0" + showseconds;
            else
                valueShow = showseconds + "";

            if (showmint > 0) ;
            //  timeOnLevelComplete.text = "0" + (int)Math.Abs(totalTime / 60) + " : " + valueShow;
            // else
            // timeOnLevelComplete.text = (int)Math.Abs(totalTime / 60) + " : " + valueShow;

        }
    }


    private int showseconds = 0;
    private int showmint = 0;
    string valueShow;
    IEnumerator Delay(float t)
    {




        yield return new WaitForSeconds(t);
        //	GameManager.instance.OnFail ();
        //		GameObject.FindWithTag ("MainCamera").GetComponent<GameDialogs> ().Dia_TimesUp ();



    }

    private bool isrewardAlready = false;
    private Color newcolor;
    public void TimeReward()
    {
        timeToCompleteLevel += 120f;
        HHG_SoundManager.Instance.OffPlayTimmerSound();
        isplayerwarning = false;
        isrewardAlready = true;
      
        timecounterText.GetComponent<Animator>().enabled = false;
       
        newcolor.a = 1f;
        newcolor = Color.black;
        timecounterText.color = newcolor;
        getTimeButton.SetActive(false);


    }

    public void TimeReward60Sec()
    {
        timeToCompleteLevel += 60f;
        HHG_SoundManager.Instance.OffPlayTimmerSound();
        isplayerwarning = false;
        isrewardAlready = true;

        timecounterText.GetComponent<Animator>().enabled = false;
       // UIManagerObject.instance.HideTimeUp();
        newcolor.a = 1f;
        newcolor = Color.black;
        timecounterText.color = newcolor;
        getTimeButton.SetActive(false);


    }

}

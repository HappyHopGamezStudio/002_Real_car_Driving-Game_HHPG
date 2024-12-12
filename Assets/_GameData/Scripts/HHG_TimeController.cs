using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HHG_TimeController : MonoBehaviour
{
  public Text timecounterText;
  public Text []TimeHowsave;
  private float elapsedTime;
  private bool isTiming;
  // public int[] timeToCompleteLevels;
    public float timeToCompleteLevel;
    public static bool isTimeOver;
    public bool isTimerOn = false;
    public static bool isGamePaused;

    public int timeForFreeCoin;

    bool checkIt, isplayerwarning = false;
    public static bool isAttackMode;

    int remainder;
    int Remainder;
    public GameObject TimerObject;
    public static HHG_TimeController Instance;
    public bool StartTimer = true;
    void Awake()
    {
        Instance = this;
        if (PrefsManager.GetGameMode() == "free")
        {
            TimerObject.SetActive(false);
            StartTimer = false;
            timeToCompleteLevel = timeForFreeCoin;
        }
        else  if (PrefsManager.GetLevelMode() == 0)
        {
            StartTimer = true;
            timeToCompleteLevel = timeForFreeCoin;
        }
        
    }


    public delegate void TimeOver();
    public static event TimeOver OnTimeOver;

    void Start()
    {
        elapsedTime = 0f;
        isTiming = true;
        isAttackMode = false;
        checkIt = false;
        if (PrefsManager.GetGameMode() == "free")
        {
            TimerObject.SetActive(false);
            StartTimer = false;
          //  timeToCompleteLevel = timeForFreeCoin;
        }
        else  if (PrefsManager.GetLevelMode() == 0)
        {
            isTimeOver = false;
        }
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
        if (!StartTimer)
        {
            return;
        }
        if (timeToCompleteLevel >= 0 && !isGamePaused)
        {
            timeToCompleteLevel -= Time.deltaTime;
            totalTime += Time.deltaTime;
        }
       // if (timeToCompleteLevel <= timeToCompleteLevels[remainder] - 10)
        {
            isAttackMode = true;
            //Debug.Log ("Eemy can attack");
        }
        if (timeToCompleteLevel <= 10 && !isplayerwarning && PrefsManager.GetGameMode() != "free" && !isrewardAlready)
        {
            timecounterText.color = Color.red;
        }

        //		Debug.Log ("time"+TimeController.isTimeOver+"-"+timeToCompleteLevel);
        if (timeToCompleteLevel < 0.0F && !isTimeOver)
        {
            if (PrefsManager.GetGameMode() == "free")
            {
               // UiManagerObject_EG.instance.MissionWasted();.
               HHG_UiManager.instance?.ShowFail();
                StartTimer = false;
                TimerObject.SetActive(false);
            }
            else
            {
                Debug.Log("here my bady");
                HHG_UiManager.instance?.ShowFail();
            }
            Debug.Log("Called Failed");
            HHG_SoundManager.Instance.OffPlayTimmerSound();
            isTimeOver = true;
        }
        timecounterText.text = FormatTime(timeToCompleteLevel);
        
        
        
        
        if (isTiming)
        {
            // Increment the elapsed time
            elapsedTimeMine += Time.deltaTime;
        }
       
        
        
        if (isTracking)
        {
            elapsedTimeMine += Time.deltaTime; // Increment elapsed time
        }

        // Format and display the time
        timeTexttrack.text = FormatTimeTrack(elapsedTimeMine);
        FOrfail.text = FormatTimeTrack(elapsedTimeMine);
        
        
    }
    
    
    
    
    
    public Text timeTexttrack,FOrfail; 
    public bool isTracking = false; 

    private float elapsedTimeMine = 0f;



    // Function to format time in min:sec:msec
    private string FormatTimeTrack(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 1000F) % 1000F);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    // Function to reset elapsed time
    public void ResetTime()
    {
        elapsedTime = 0f;
    }
    void ShowElapsedTime()
    {
        // Format elapsed time as minutes and seconds, then display on text
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        foreach (var time in TimeHowsave)
        {
            time.text = $"{minutes:00}:{seconds:00}";
        }
       
    }
    public void UpdateTimer(float time)
    {
        isTimeOver = false;
        timecounterText.color = Color.white;
        timeToCompleteLevel = time;
        StartTimer = true;
        TimerObject.SetActive(true);
        isTracking = true;
    }
    public void StopTimer()
    {
        isTimeOver = false;
        StartTimer = false;
        TimerObject.SetActive(false);
        isTiming = false;
        ShowElapsedTime();
        isTracking = false;
        ResetTime();
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

    private int intTime, minuts, seconds;
    //  private float fraction;
    private string timeText;
    string FormatTime(float time)
    {
        intTime = (int)time;
        minuts = intTime / 60;
        seconds = intTime % 60;
        // fraction = time * 1000;
        // fraction = (fraction % 1000);
        timeText = String.Format("{0:00}:{1:00}", minuts, seconds);
        return timeText;
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
        TimerObject.SetActive(false);


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
        TimerObject.SetActive(false);
        
    }
}

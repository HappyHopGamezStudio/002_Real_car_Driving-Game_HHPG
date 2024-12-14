using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText; // Reference to the UI Text component
    public AudioSource countdownAudioSource; // Reference to the AudioSource for playing sounds
    public AudioClip threeClip; // Sound for "3"
    public AudioClip twoClip; // Sound for "2"
    public AudioClip oneClip; // Sound for "1"
    public AudioClip goClip; // Sound for "Go"
    public Animator animator; // Reference to an Animator to play animations

    private int countdownValue = 3;

    void Start()
    {
        StartCoroutine(StartCountdown());
        if (HHG_GameManager.Instance.CurrentCar!=null)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().constraints = 
                RigidbodyConstraints.FreezeRotationX | 
                RigidbodyConstraints.FreezeRotationZ | 
                RigidbodyConstraints.FreezePositionX | 
                RigidbodyConstraints.FreezePositionZ;
            
            HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().engineTorque = 4100f;

        }
       
    }

    void Awake()
    {
        isonsmoke = true;
    }
    IEnumerator StartCountdown()
    {
        while (countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();
            PlaySoundAndAnimation(countdownValue);
            yield return new WaitForSeconds(1);
            countdownValue--;
        }
        
        countdownText.text = "GO!";
        PlaySoundAndAnimation(0);
        yield return new WaitForSeconds(1); // Wait for 1 second
        countdownText.gameObject.SetActive(false); // Hide the countdown text if necessary
    }

    void PlaySoundAndAnimation(int count)
    {
        switch (count)
        {
            case 3:
                countdownAudioSource.PlayOneShot(threeClip);
                animator.Play("ThreeAnimation");
                break;
            case 2:
                countdownAudioSource.PlayOneShot(twoClip);
                animator.Play("TwoAnimation");
                break;
            case 1:
                countdownAudioSource.PlayOneShot(oneClip);
                animator.Play("OneAnimation");
                break;
            case 0:
                countdownAudioSource.PlayOneShot(goClip);
                animator.Play("GoAnimation");
                //PrefsManager.SetGo(1);
                HHG_GameManager.Instance.isgo = true;
                Invoke(nameof(showpanel),1);
                break;
        }
    }

    private void showpanel()
    {
        HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha = 1; 
        transform.gameObject.SetActive(false);
        if (  HHG_GameManager.Instance.CurrentCar!=null)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.None;
        }
    }

    void NotPress()
    {
        isonsmoke = false;
        HHG_UiManager.instance.LeftForwadrace.GetComponent<RCC_UIController>().pressing = false;
    }
    private bool isonsmoke = false;
    private void Update()
    {
        if (isonsmoke)
        {
            HHG_UiManager.instance.LeftForwadrace.GetComponent<RCC_UIController>().pressing = true;
        }
       
    }

    private void OnDisable()
    {
      //  GT_LevelManager.instace.rcc_camera.ResetCamera ();
        PrefsManager.SetNosCount(3);
        RCC_MobileButtons.Manger.setNos(true);
        /*if (HHG_GameManager.Instance.CurrentCar != null)
        {
          //  HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().maxspeed *= speedBoostMultiplier;
            HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().engineTorque = 1500f;
           // HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().AddForce(Vector3.forward*5f);
        }*/

        Invoke(nameof(NotPress),3.5f);
    }
    
    private void OnEnable()
    {
        isonsmoke = true;
    }
}
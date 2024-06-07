using UnityEngine;
using System.Collections;


public class HHG_SoundManager : MonoBehaviour
{
    public static HHG_SoundManager Instance { get; private set; }
    int adsCounter = 0;
    public GameObject resource;
    public AudioClip BgSound, menuSound;
    public AudioClip click,  LevelComplete, levelFail, timer;

    public bool isDeletePrefs;
    // Use this for initialization
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (isDeletePrefs)
        {
            PlayerPrefs.DeleteAll();
        }

    }

    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();

    }

    public void PlayOneShotSounds(AudioClip clip)
    {
        resource.GetComponent<AudioSource>().clip = clip;
        resource.GetComponent<AudioSource>().Play();
    }

    public void PlayOneShotSoundsFail(AudioClip clip)
    {
        resource.GetComponent<AudioSource>().clip = clip;
        resource.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Stop();
    }

    public void PlayTimmerSound()
    {
        resource.GetComponent<AudioSource>().clip = timer;
        resource.GetComponent<AudioSource>().Play();
        resource.GetComponent<AudioSource>().volume = 0.5f;
        resource.GetComponent<AudioSource>().loop = true;

        GetComponent<AudioSource>().Stop();
    }
    
    public void OffPlayTimmerSound()
	{
		
		resource.GetComponent<AudioSource>().enabled = false;
		GetComponent<AudioSource>().enabled = false;
        resource.GetComponent<AudioSource>().volume = 0f;
        resource.GetComponent<AudioSource>().Stop();
	}

}

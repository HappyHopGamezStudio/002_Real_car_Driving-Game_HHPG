using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hornbtn : MonoBehaviour
{
    public AudioSource horn;
    // Start is called before the first frame update
    void Start()
    {
        horn.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playhorn()
    {
        horn.Play();
    }
    public void stophorn()
    {
        horn.Stop();
    }
   
   
}

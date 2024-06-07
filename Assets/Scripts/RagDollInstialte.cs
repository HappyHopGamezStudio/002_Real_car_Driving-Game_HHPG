using System;
using System.Collections;
using System.Collections.Generic;
using ITS.AI;
using UnityEngine;
using UnityEngine.Playables;

public class RagDollInstialte : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RagDoll;

    //public GameObject Cash;
    public Animator PAnimator = null;
    public bool VehicleBlast = false;
    public bool HumanAi = false;
    public AudioClip clip;

    
    public void InstiateNow()
    {
        Instantiate(RagDoll, transform.position, transform.rotation);
        if (gameObject.GetComponentInParent<TSTrafficAI>())
        {
            this.gameObject.GetComponentInParent<TSTrafficAI>().gameObject.SetActive(false);
        }
    }
    
    public void HitAnimation()
    {
        PAnimator.SetBool("ShootHit", true);
    }

    public void RunAnimation()
    {
        PAnimator.SetBool("Firsthit", true);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "TrafficCar")
        {
            if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving && HumanAi)
            {
                if (HumanAi)
                {
                    RagDoll.GetComponent<AudioSource>().PlayOneShot(clip);
                    InstiateNow();
                }
            }
        }
    }

}
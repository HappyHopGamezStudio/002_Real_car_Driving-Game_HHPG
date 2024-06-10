using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HHG_RestPosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstant.Tag_Player)
        {
            gameObject.SetActive(false);
            HHG_LevelManager.instace.LastPosition = other.transform.position;
            HHG_LevelManager.instace.LastRotion = other.transform.rotation;
        }
    }
    public async void OnTriggerStay(Collider other)
   {
       if (other.gameObject.tag == GameConstant.Tag_Player) 
       {
            gameObject.SetActive(false);       
            HHG_LevelManager.instace.LastPosition = other.transform.position;
            HHG_LevelManager.instace.LastRotion = other.transform.rotation; 
       }
   }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameConstant.Tag_Player)
        {
            gameObject.SetActive(true);
        }
    }
   
}

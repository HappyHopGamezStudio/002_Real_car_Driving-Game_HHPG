using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Forwater : MonoBehaviour
{
    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstant.Tag_Player)
        {
            if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving && !HHG_GameManager.Instance.CurrentCar.GetComponent<VehicleProperties>().TrafficCarAi)
            {
                await Task.Delay(200);
                HHG_GameManager.Instance.CurrentCar.transform.position = HHG_LevelManager.instace.LastPosition; 
                HHG_GameManager.Instance.CurrentCar.transform.rotation = HHG_LevelManager.instace.LastRotion;
                
                HHG_GameManager.Instance.CurrentCar. GetComponent<Rigidbody>().velocity=Vector3.zero; 
                HHG_GameManager.Instance.CurrentCar. GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
            }
            else if(HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving && HHG_GameManager.Instance.CurrentCar.GetComponent<VehicleProperties>().TrafficCarAi)
            {
                HHG_LevelManager.instace.rcc_camera.enabled = false;
                await Task.Delay(3000);
                HHG_UiManager.instance.Restart();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsTriger : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
   {
      //this forbike
      if (other.gameObject.tag == "HandleTrigger")
      {
        // GameControl.manager.getInBike.SetActive(true);
         HHG_GameControl.manager.getInVehicle.SetActive(false);
      //   GameManager.Instance.CurrentCar = other.GetComponentInParent<BikeControl>().gameObject;
        // GameManager.Instance.  mapPath = GameManager.Instance. CurrentCar.GetComponent<BikeControl>().mapPath;
      }
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         HHG_GameControl.manager.getInVehicle.SetActive(true);
        // GameControl.manager.getInBike.SetActive(false);
         HHG_GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
        // GameManager.Instance.  mapPath =  GameManager.Instance. CurrentCar.GetComponent<VehicleProperties>().MapPath;
      }
   }

   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.tag == "HandleTrigger")
      {
      //   GameControl.manager.getInBike.SetActive(true);
         HHG_GameControl.manager.getInVehicle.SetActive(false);
      //   GameManager.Instance.CurrentCar = other.GetComponentInParent<BikeControl>().gameObject;
       //  GameManager.Instance.  mapPath =  GameManager.Instance. CurrentCar.GetComponent<BikeControl>().mapPath;
      }
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         HHG_GameControl.manager.getInVehicle.SetActive(true);
     //    GameControl.manager.getInBike.SetActive(false);
         HHG_GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
        // GameManager.Instance.  mapPath =  GameManager.Instance. CurrentCar.GetComponent<VehicleProperties>().MapPath;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.tag == "HandleTrigger")
      {
      //    GameControl.manager.getInBike.SetActive(false);
       //  GameManager.Instance.mapPath =  null;
         HHG_GameManager.Instance.CurrentCar = null;
         Debug.Log("Car HandleTrigger");
      }
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         HHG_GameControl.manager.getInVehicle.SetActive(false);
         HHG_GameManager.Instance.CurrentCar = null;
      //   GameManager.Instance.mapPath =  null;
         Debug.Log("Car Handle");
      }
   }
}
   
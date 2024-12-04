using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedcheck : MonoBehaviour
{
  public float meterspeed = 100f;
  public float POrtalActiveRange;
  public Transform Player;

  

  private float distanceToPlayer,mappedDistance;
  private Vector3 newPosition;

  private void Update()
  {

    if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
    {
      Player = HHG_GameManager.Instance.TPSPlayer.transform;
    }
    else if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
    {
      Player = HHG_GameManager.Instance.CurrentCar.transform;
    }
        

    transform.LookAt(transform.position - Player.forward);
    distanceToPlayer = Vector3.Distance(transform.position, Player.position);
    newPosition = transform.eulerAngles;
    newPosition.y = Player.eulerAngles.y + 90f; 
    transform.eulerAngles = newPosition;
    if (distanceToPlayer <= POrtalActiveRange)
    {
      mappedDistance = Mathf.Clamp(POrtalActiveRange + distanceToPlayer, 0f, POrtalActiveRange);
      newPosition = transform.position;
      transform.position = newPosition;
    }
  }
}
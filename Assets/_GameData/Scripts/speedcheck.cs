using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedcheck : MonoBehaviour
{
  public float meterspeed = 100f;
  public float POrtalActiveRange;
  public Transform Player;



  private float distanceToPlayer, mappedDistance;
  private Vector3 newPosition;
  private bool isActive = false;

  private void Update()
  {
    distanceToPlayer = (transform.position - Player.position).sqrMagnitude;
    if (distanceToPlayer >= POrtalActiveRange)
    {

      transform.LookAt(transform.position - Player.forward);
      distanceToPlayer = Vector3.Distance(transform.position, Player.position);
      newPosition = transform.eulerAngles;
      newPosition.y = Player.eulerAngles.y + 90f;
      transform.eulerAngles = newPosition;


      mappedDistance = Mathf.Clamp(POrtalActiveRange + distanceToPlayer, 0f, POrtalActiveRange);
      newPosition = transform.position;
      transform.position = newPosition;

    }
    else
    {
      return;
    }
  }
}
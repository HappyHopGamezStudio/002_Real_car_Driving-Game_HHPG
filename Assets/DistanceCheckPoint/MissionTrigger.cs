using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MissionTrigger : MonoBehaviour
{
 
   
    public float POrtalActiveRange;
    public Transform Player;
    public RectTransform CanvasTransform;
   
   
   

    private bool fromOpenworld=false;
    // Start is called before the first frame update





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
            //Player = LevelManager_EG.instace.SelectedPlayer.transform;
            Player = HHG_GameManager.Instance.CurrentCar.transform;
        }
        

        CanvasTransform.LookAt(CanvasTransform.position + Player.forward);
        distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer <= POrtalActiveRange)
        {
            // Map the distance to a value between 0 and the portal active distance
            mappedDistance = Mathf.Clamp(POrtalActiveRange - distanceToPlayer, 0f, POrtalActiveRange);

            // Map the mapped distance to a value between the minimum and maximum canvas heights
            // Smoothly interpolate between current height and target height
            // Update the canvas position
            newPosition = CanvasTransform.anchoredPosition;
            CanvasTransform.anchoredPosition = newPosition;
        }
    }
}

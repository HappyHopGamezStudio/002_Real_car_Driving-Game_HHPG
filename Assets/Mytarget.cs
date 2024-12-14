using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Mytarget : MonoBehaviour
{

    public LookAtConstraint lookAtConstraint;
    public Transform newSource;





    // Method to start following the car
    public void OnEnable()
    {
        if (newSource==null)
        {
            if (HHG_GameManager.Instance.TpsStatus==PlayerStatus.ThirdPerson)
            {
                newSource = HHG_LevelManager.instace.SelectedPlayer.transform;
                AddSource(); 
            }
            else
            {
                newSource = HHG_GameManager.Instance.CurrentCar.gameObject.transform;
                AddSource(); 
            }
           
        }
    }

    // Method to stop following and return to the original position

    public void AddSource()
    {
        if (lookAtConstraint != null && newSource != null)
        {
            ConstraintSource source = new ConstraintSource
            {
                sourceTransform = newSource,
                weight = 1.0f // Full influence by default
            };

            lookAtConstraint.AddSource(source);
            Debug.Log("Source added: " + newSource.name);
        }
    }
}
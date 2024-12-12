using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateNow : MonoBehaviour
{
 
    public void UpdateButtonClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Healthbar;
    public Text Name;
    public HHG_LevelManager _LevelManager;
    void  Start()
    {
        _LevelManager = FindObjectOfType<HHG_LevelManager>();
        red=Color.red;
        green=Color.green;
        Healthbar.color=Color.green;
         UpdateName();
    }

    public async void UpdateName()
    {
        await Task.Delay(50);
        // Name.text = _LevelManager.CurrentChase.Name;
    }

    public Color red, green;
    // Update is called once per frame
    void Update()
    {
        if ( _LevelManager==null)
            return;
        
     //   if ( _LevelManager.CurrentChase.Hp !=Healthbar.fillAmount)
        {
           // Healthbar.fillAmount =  _LevelManager.CurrentChase.Hp ;
            if (Healthbar.fillAmount < 0.4f)
            {
                Healthbar.color=red;
            }
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
using UnityEngine.Serialization;

public class HHG_LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject[] Players;
    public int[] Reward;
    [FormerlySerializedAs("CurrentLevelProperties")] public HHG_LevelProperties currentHhgLevelProperties;
    public DrawMapPath mapPath;
    public GameObject SelectedPlayer,FreeMode,coinBar;
    public static HHG_LevelManager instace;
    public LineRenderer Line;
    public int CurrentLevel, coinsCounter;
   public HUDNavigationSystem system;
   public RCC_Camera rcc_camera;
   public RCC_DashboardInputs Canvas;
   public PlayerCamera_New Tpscamera;
   public HHG_OpenWorldManager hhgOpenWorldManager;
   public GameObject TpsPlayer;
   public DriftCanvasManager driftCanvasManagerNow;


   public AudioSource CoinSound;
   
    void Awake()
    {
        instace = this;
       
        if (PrefsManager.GetGameMode() != "free")
        {
            if (PrefsManager.GetLevelMode() == 0)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                currentHhgLevelProperties = Levels[CurrentLevel].GetComponent<HHG_LevelProperties>();
                SelectedPlayer.transform.position = currentHhgLevelProperties.PlayerPosition.position;
                SelectedPlayer.transform.rotation = currentHhgLevelProperties.PlayerPosition.rotation;
                currentHhgLevelProperties.gameObject.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1; 
            FreeMode.SetActive(true);
            coinBar.GetComponentInChildren<Text>().text = "" + PrefsManager.GetCoinsValue();
            coinBar.SetActive(true);
            SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
            SetTransform(hhgOpenWorldManager.TpsPosition, hhgOpenWorldManager.CarPostiom);
        }
        SelectedPlayer.SetActive(true);
        
        SelectedPlayer.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        SelectedPlayer.GetComponent<VehicleProperties>().ConeEffect.SetActive(false);
    }
    public void SetTransform(Transform playerposition, Transform defulcar)
    {
        TpsPlayer.transform.position = playerposition.position;
        TpsPlayer.transform.rotation = playerposition.rotation;

        Tpscamera.transform.position = playerposition.position;
        Tpscamera.transform.rotation = playerposition.rotation;
        
        SelectedPlayer.transform.position = defulcar.position;
        SelectedPlayer.transform.rotation = defulcar.rotation;
        
    }
public IEnumerator Start(){
    if (PrefsManager.GetLevelMode()!=1)
    {
        yield return new WaitForSeconds(0.5f);
        HHG_UiManager.instance.ShowObjective(currentHhgLevelProperties.LevelStatment);
    }
}

public void TaskCompleted()
    {
        currentHhgLevelProperties.Nextobjective();
    }
public async void Respawn()
{
    HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().ResetCarNow();
}
public Material[] CarEffect;
public float multiplaxer;
private float offset;

void Update()
{
       
    offset += Time.deltaTime * multiplaxer;
    foreach (var VARIABLE in CarEffect)
    {
        VARIABLE.mainTextureOffset=new Vector2(0, offset);
    }
       
}
}

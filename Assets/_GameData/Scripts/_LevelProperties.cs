using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class _LevelProperties : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform PlayerPosition;
    public Transform TpsPosition;
    public GameObject[] LevelObjective;
    public string LevelStatment;
    public float Times;
    public int LevelReward;
    public int currentobjective = 0;
    public bool isCutScene, isSetPosition;
    public GameObject Timeline;
    public PlayableDirector Director;
    public CheckpointController checkpointController;
    public bool IsCheckpoint, IsRace, Islap = false;
    public int CurrentLapValue = 0;
    public int LapValue = 0;
    public bool IsUsetime = false;

    async void StartMission()
    {
        if (isCutScene)
        {
            HHG_TimeController.Instance.TimerObject.SetActive(false);
            HHG_UiManager.instance.HideGamePlay();
//            HHG_UiManager.instance.CutScene.SetActive(true);
            HHG_LevelManager.instace.HUDNavigationCanvas.gameObject.SetActive(false);
            HHG_LevelManager.instace.coinBar.gameObject.SetActive(false);
            HHG_LevelManager.instace.JemBar.gameObject.SetActive(false);
            Timeline.SetActive(true);
            if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
            {
                HHG_GameManager.Instance.TpsCamera.gameObject.SetActive(false);
            }
            else
            {
                HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(false);
            }

            Time.timeScale = 1f;
            Director.Play();
            Invoke("HideTimeline", (float)Director.duration - 0.9f);
            HHG_UiManager.instance.HideGamePlay();
            if (checkpointController != null)
            {
                checkpointController.enabled = false;
                await Task.Delay(1000);
                HHG_GameManager.Instance.TrafficSpawn.GetComponent<TSTrafficSpawner>().trafficCarsParent.gameObject
                    .SetActive(false);
            }
            
            HHG_TimeController.Instance.TimerObject.SetActive(false);
        }
        else
        {
            if (LevelStatment != "")
            {
                // HHG_UiManager.instance.OpenWoldData.CallManager.GetComponent<CounDownFormission>().statemanent(LevelStatment);
                HHG_UiManager.instance.ObjectiveText.text = LevelStatment;
            }
            
            HHG_UiManager.instance.ShowObjective(HHG_LevelManager.instace.currentHhgLevelProperties.LevelStatment);

            if (checkpointController != null)
            {
                checkpointController.enabled = true;
                /*await Task.Delay(1000);
                HHG_GameManager.Instance.TrafficSpawn.GetComponent<TSTrafficSpawner>().trafficCarsParent.gameObject.SetActive(false);*/

            }
        }

    }



    public bool freemode = false;

    public void Nextobjective()
    {
        currentobjective++;
        if (currentobjective >= LevelObjective.Length)
        {
            HHG_UiManager.instance.ShowComplete();
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + LevelReward);
        }
        else
        {
            if (freemode)
            {
                foreach (GameObject objective in LevelObjective)
                    objective.SetActive(false);
                currentobjective--;
            }
            else
            {
                foreach (GameObject objective in LevelObjective)
                    objective.SetActive(false);
                LevelObjective[currentobjective].SetActive(true);
                //  if (GT_GameManager.Instance.mapPath != null)
                //    GT_GameManager.Instance.mapPath.Target = LevelObjective[currentobjective].transform.GetChild(0);
                if (freemode)
                    PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            }
            // HHG_UiManager.instace.mapPath.SetDestinationAtFirst(LevelObjective[currentobjective].transform.GetChild(0));
        }
    }



  async void OnEnable()
    {
        StartMission();
        //CutSceneStart();
        currentobjective = 0;

        if (LevelObjective.Length > 0) LevelObjective[currentobjective].SetActive(true);
        foreach (var speed in HHG_GameManager.Instance.SpeedCheck)
        {
            speed.gameObject.SetActive(false);
        }
        
        if (IsUsetime)
        {
            HHG_TimeController.Instance.UpdateTimer(Times);
        }

        if (Islap)
        {
            HHG_UiManager.instance.CheckPointBar.SetActive(false);
            HHG_UiManager.instance.Racebar.SetActive(false);
            HHG_UiManager.instance.LapBar.SetActive(true);
            CurrentLapValue =1;
        }
        else if (IsRace)
        {
            HHG_UiManager.instance. CheckPointBar.SetActive(false);
            HHG_UiManager.instance.Racebar.SetActive(false);
            HHG_UiManager.instance.LapBar.SetActive(false);
            if (HHG_GameManager.Instance.CurrentCar==null)
            {
                playerCar = HHG_LevelManager.instace.SelectedPlayer;
            }
            else
            {
                playerCar = HHG_GameManager.Instance.CurrentCar;
            }
          
            playerCar.GetComponent<CarSpriteController>().enabled = true;
            playerCar.GetComponent<CarSpriteController>().spriteRenderer.gameObject.SetActive(true);
            animator.gameObject.SetActive(true);
            StartRace();
            countdownValue = 3;
            StartCoroutine(StartCountdown());
            raceDirectionPoint = Firstpos;
            isonsmoke = true;
            await Task.Delay(1000);
            HHG_GameManager.Instance.TrafficSpawn.GetComponent<TSTrafficSpawner>().trafficCarsParent.gameObject.SetActive(false);

        }
        else if (IsCheckpoint)
        {
            HHG_UiManager.instance.CheckPointBar.SetActive(true);
            HHG_UiManager.instance.Racebar.SetActive(false);
            HHG_UiManager.instance.LapBar.SetActive(false);
        }
        HHG_UiManager.instance.CarMobile.SetActive(false);
        HHG_UiManager.instance.Getoutbutton.SetActive(false);
      
    }


    public void HideTimeline()
    {
        Logger.ShowLog("HideTimeLine Ok");
        //  HHG_UiManager.instance.CutScene.SetActive(false);
        HHG_UiManager.instance.ShowGamePlay();
        Timeline.SetActive(false);
        if (IsUsetime)
        {
            HHG_TimeController.Instance.TimerObject.SetActive(true);
        }
        HHG_LevelManager.instace.HUDNavigationCanvas.gameObject.SetActive(true);
        HHG_LevelManager.instace.coinBar.gameObject.SetActive(true);
        HHG_LevelManager.instace.JemBar.gameObject.SetActive(true);
        if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
        {
            HHG_GameManager.Instance.TpsCamera.gameObject.SetActive(true);
        }
        else
        {
            HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(true);
        }

        HHG_UiManager.instance.ShowObjective(HHG_LevelManager.instace.currentHhgLevelProperties.LevelStatment);
        if (checkpointController != null)
        {
            checkpointController.enabled = true;
        }
    }

    #region MyRegion

    public GameObject playerCar; // Reference to the player's car (assign in the Inspector)
public GameObject[] cars; // Array to hold all AI cars
public int minActiveCars = 4; // Minimum number of cars to activate
public int maxActiveCars = 5; // Maximum number of cars to activate
public Sprite[] positionSprites; // Array of sprites for positions 1 to 8

private Dictionary<Transform, SpriteRenderer> carSpriteRenderers = new Dictionary<Transform, SpriteRenderer>();
private List<int> carIndices = new List<int>();
private List<Transform> activeCarTransforms = new List<Transform>();

public Transform[] ActiveCars; // Dynamically populated array of active car Transforms

void StartRace()
{
    ActivateRandomCars(); // Activate random cars at the start
    InitializeSpriteRenderers();
}

void Update()
{
    if (!IsRace) return;

    if (IsRace)
    {
        UpdateRacePositions();
        if (isonsmoke)
        {
            HHG_UiManager.instance.LeftForwadrace.GetComponent<RCC_UIController>().pressing = true;
        }
    }
}

public void ActivateRandomCars()
{
    // Deactivate all AI cars first
    foreach (GameObject car in cars)
    {
        car.SetActive(false);
    }

    // Clear the active car list
    activeCarTransforms.Clear();

    // Add the player car to the active list
    if (playerCar != null)
    {
        playerCar.SetActive(true);
        activeCarTransforms.Add(playerCar.transform);
    }

    // Create a list of AI car indices
    carIndices.Clear();
    for (int i = 0; i < cars.Length; i++)
    {
        carIndices.Add(i);
    }

    // Shuffle the list of indices
    for (int i = 0; i < carIndices.Count; i++)
    {
        int randomIndex = Random.Range(0, carIndices.Count);
        int temp = carIndices[i];
        carIndices[i] = carIndices[randomIndex];
        carIndices[randomIndex] = temp;
    }

    // Determine the number of AI cars to activate
    int carsToActivate = Random.Range(minActiveCars, maxActiveCars + 1);

    // Activate the chosen AI cars and populate the activeCarTransforms list
    for (int i = 0; i < carsToActivate; i++)
    {
        GameObject car = cars[carIndices[i]];
        car.SetActive(true);
        activeCarTransforms.Add(car.transform);
       
       
        car.GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionZ;
        car.GetComponent<RCC_AICarController>().currentWaypoint = 4;
    }

    // Update the ActiveCars array with the current active cars
    ActiveCars = activeCarTransforms.ToArray();
}



public Transform Firstpos, Scepos;
public Transform raceDirectionPoint;

private void InitializeSpriteRenderers()
{
    // Initialize sprite renderers for each car (if needed for other purposes)
    foreach (Transform car in ActiveCars)
    {
        CarSpriteController carController = car.GetComponent<CarSpriteController>();
        if (carController != null)
        {
            // Optionally assign default sprite here, but the main sprite will be handled in UpdateRacePositions
        }
        else
        {
            Debug.LogWarning($"Car {car.name} is missing a CarSpriteController.");
        }
    }
}

private void UpdateRacePositions()
{
    // Define the race direction vector
    Vector3 raceDirection = (raceDirectionPoint.position - transform.position).normalized;

    // Sort cars based on their progress along the race direction
    List<Transform> sortedCars = new List<Transform>(ActiveCars);
    sortedCars.Sort((a, b) => CompareCarPositions(a, b, raceDirection));

    // Update each car's position sprite
    for (int i = 0; i < sortedCars.Count; i++)
    {
        Transform car = sortedCars[i];
        int position = i + 1; // Position starts at 1

        // Get the CarSpriteController for the car
        CarSpriteController carController = car.GetComponent<CarSpriteController>();

        if (carController != null)
        {
            // If a sprite exists for the current position, update the car's sprite
            if (position <= positionSprites.Length)
            {
                carController.UpdateSprite(positionSprites[position - 1]); // Position 1 uses index 0
            }
            else
            {
                Debug.LogWarning($"No sprite available for position {position}.");
            }
        }
    }
}

private int CompareCarPositions(Transform carA, Transform carB, Vector3 raceDirection)
{
    // Project each car's position onto the race direction
    float progressA = Vector3.Dot(carA.position, raceDirection);
    float progressB = Vector3.Dot(carB.position, raceDirection);

    // Compare their progress (higher progress means further ahead)
    return progressB.CompareTo(progressA);
}

  #endregion

    public Text countdownText; // Reference to the UI Text component
    public AudioSource countdownAudioSource; // Reference to the AudioSource for playing sounds
    public AudioClip threeClip; // Sound for "3"
    public AudioClip twoClip; // Sound for "2"
    public AudioClip oneClip; // Sound for "1"
    public AudioClip goClip; // Sound for "Go"
    public Animator animator; // Reference to an Animator to play animations

    private int countdownValue = 3;

  

  
    IEnumerator StartCountdown()
    {
        while (countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();
            PlaySoundAndAnimation(countdownValue);
            yield return new WaitForSeconds(1);
            countdownValue--;
        }
        
        countdownText.text = "GO!";
        PlaySoundAndAnimation(0);
        yield return new WaitForSeconds(1); // Wait for 1 second
        countdownText.gameObject.SetActive(false); // Hide the countdown text if necessary
    }

    void PlaySoundAndAnimation(int count)
    {
        switch (count)
        {
            case 3:
                countdownAudioSource.PlayOneShot(threeClip);
                animator.Play("ThreeAnimation");
                break;
            case 2:
                countdownAudioSource.PlayOneShot(twoClip);
                animator.Play("TwoAnimation");
                break;
            case 1:
                countdownAudioSource.PlayOneShot(oneClip);
                animator.Play("OneAnimation");
                break;
            case 0:
                countdownAudioSource.PlayOneShot(goClip);
                animator.Play("GoAnimation");
                //PrefsManager.SetGo(1);
                HHG_GameManager.Instance.isgo = true;
                Invoke(nameof(showpanel),1);
                break;
        }
    }

    private void showpanel()
    {
        HHG_UiManager.instance.ShowGamePlay();
        HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha = 1; 
        animator.gameObject.SetActive(false);
       foreach (var car in ActiveCars)
       {
           car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
       }

        OnGo();
    }

    void NotPress()
    {
        isonsmoke = false;
        HHG_UiManager.instance.LeftForwadrace.GetComponent<RCC_UIController>().pressing = false;
    }
    private bool isonsmoke = false;


    private void OnGo()
    {
      //  GT_LevelManager.instace.rcc_camera.ResetCamera ();
        PrefsManager.SetNosCount(3);
        RCC_MobileButtons.Manger.setNos(true);
        /*if (HHG_GameManager.Instance.CurrentCar != null)
        {
          //  HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().maxspeed *= speedBoostMultiplier;
            HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().engineTorque = 1500f;
           // HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().AddForce(Vector3.forward*5f);
        }*/

        Invoke(nameof(NotPress),3.5f);
    }
    
}

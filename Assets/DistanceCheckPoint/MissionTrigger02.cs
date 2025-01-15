using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum MissionType
{
    MegaRamp,
    Demolish,
    ColorCube,
    Racing,
    CheckPoint,
    PoliceEscape
    
}

[System.Serializable]

public class bannerdata
{
    public string BannerName;
    public Sprite icone;
    public Sprite BgSprite;
    public Color triggerColor;

}

public class MissionTrigger02:MonoBehaviour
{
    public MissionType Mission;
    public GameObject portal;
    public float POrtalActiveRange;
    public Transform Player;
    public RectTransform CanvasTransform;
    private float currentHeight;
    private float CurrentScale;
    private float targetHeight;
    private float targetScale;
    public Text BannerNameText;
    public Image BannerImage;
    public bannerdata RacingData;
    public bannerdata demolishData;
    public bannerdata ColorCubeData;
    public bannerdata MegaRampData;
    public bannerdata PoliceEscapeData;
    public bannerdata CheckPointData;
    public Image startPointImage;
    public LineRenderer LineRender_;
    public Image BannerBgImage;
    public ParticleSystem roundportal;

    private bool fromOpenworld=false;
    // Start is called before the first frame update
    void Start()
    {
        currentHeight = 7; // Start at max height
        targetHeight = currentHeight;
        ParticleSystem.MainModule mainModule = roundportal.main;
       
        switch (Mission)
        {
            case MissionType.ColorCube:
                BannerImage.sprite = ColorCubeData.icone;
                BannerNameText.text = ColorCubeData.BannerName.ToString();
                BannerBgImage.sprite = ColorCubeData.BgSprite;
                LineRender_.startColor = ColorCubeData.triggerColor;
                LineRender_.endColor = ColorCubeData.triggerColor;
                startPointImage.color=ColorCubeData.triggerColor;
                mainModule.startColor=new ParticleSystem.MinMaxGradient(ColorCubeData.triggerColor);
                BannerNameText.color = Color.black;
                break;
            case MissionType.Demolish:
                BannerImage.sprite = demolishData.icone;
                BannerNameText.text = demolishData.BannerName.ToString();
                BannerBgImage.sprite = demolishData.BgSprite;
                LineRender_.startColor = demolishData.triggerColor;
                LineRender_.endColor = demolishData.triggerColor;
                startPointImage.color=demolishData.triggerColor;
                mainModule.startColor=new ParticleSystem.MinMaxGradient(demolishData.triggerColor);
                break;
            case MissionType.Racing:
                BannerImage.sprite = RacingData.icone;
                BannerNameText.text = RacingData.BannerName.ToString();
                BannerBgImage.sprite = RacingData.BgSprite;
                LineRender_.startColor = RacingData.triggerColor;
                LineRender_.endColor = RacingData.triggerColor;
                startPointImage.color=RacingData.triggerColor;
                mainModule.startColor=new ParticleSystem.MinMaxGradient(RacingData.triggerColor);
                break;
            case MissionType.MegaRamp:
                BannerImage.sprite = MegaRampData.icone;
                BannerNameText.text = MegaRampData.BannerName.ToString();
                BannerBgImage.sprite = MegaRampData.BgSprite;
                LineRender_.startColor = MegaRampData.triggerColor;
                LineRender_.endColor = MegaRampData.triggerColor;
                startPointImage.color=MegaRampData.triggerColor;
                mainModule.startColor=new ParticleSystem.MinMaxGradient(MegaRampData.triggerColor);
                break;
            case MissionType.CheckPoint:
                BannerImage.sprite = CheckPointData.icone;
                BannerNameText.text = CheckPointData.BannerName.ToString();
                BannerBgImage.sprite = CheckPointData.BgSprite;
                LineRender_.startColor = CheckPointData.triggerColor;
                LineRender_.endColor = CheckPointData.triggerColor;
                startPointImage.color=CheckPointData.triggerColor;
                mainModule.startColor=new ParticleSystem.MinMaxGradient(CheckPointData.triggerColor);
                break;
            case MissionType.PoliceEscape:
                BannerImage.sprite = PoliceEscapeData.icone;
                BannerNameText.text = PoliceEscapeData.BannerName.ToString();
                BannerBgImage.sprite = PoliceEscapeData.BgSprite;
                LineRender_.startColor = PoliceEscapeData.triggerColor;
                LineRender_.endColor = PoliceEscapeData.triggerColor;
                startPointImage.color=PoliceEscapeData.triggerColor;
                mainModule.startColor=new ParticleSystem.MinMaxGradient(PoliceEscapeData.triggerColor);
                break;
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            portal.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivePortalAgain();
        }
    }


    public void ActivePortalAgain()
    {
        portal.SetActive(true);
        //UIManager.MissionTrigger_.DeActivatePanel();
        fromOpenworld = false;
    }

    private float distanceToPlayer,mappedDistance,canvasHeight,CanvasScale;
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
            canvasHeight = Mathf.Lerp(3.9374f, 0.15f, mappedDistance / POrtalActiveRange);
            CanvasScale = Mathf.Lerp(0.6f, 0.2f, mappedDistance / POrtalActiveRange);

            // Set the target height for smooth interpolation
            targetHeight = canvasHeight;
            targetScale = CanvasScale;

            // Smoothly interpolate between current height and target height
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, 10 /** Time.deltaTime*/);
            CurrentScale = Mathf.Lerp(CurrentScale, targetScale, 10 * Time.deltaTime);
            // Update the canvas position
            newPosition = CanvasTransform.anchoredPosition;
            if (currentHeight > 1)
            {
                newPosition.y = currentHeight;
            }

            CanvasTransform.anchoredPosition = newPosition;
            CanvasTransform.localScale = new Vector3(CurrentScale, CurrentScale, CurrentScale);
        }
    }
}

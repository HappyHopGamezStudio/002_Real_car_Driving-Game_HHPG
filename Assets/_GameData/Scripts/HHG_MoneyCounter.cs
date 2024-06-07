using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HHG_MoneyCounter : MonoBehaviour {
    public int numOfiteration;
    public float rate;
    public AudioSource coinsSource;
    public Text[] rewradMoneyText;
    public List<int> addCointList=new List<int>();
    private int tempMoney;
    public GameObject startsObj;
    public GameObject AllButton, DoubleRewardBtn;
    public AudioClip coinsCountSound;
    int reward;
    public bool isFail;
    // Use this for initialization
    private void OnEnable()
    {

        reward = HHG_LevelManager.instace.Reward[PrefsManager.GetCurrentLevel() - 1];
        HHG_LevelManager.instace.SelectedPlayer.SetActive(false);
        if (isFail)
            reward = reward - 800;
		PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + reward + (HHG_LevelManager.instace.coinsCounter));



		addCointList.Clear();
   //     addCointList.Add(LevelManager.instace.coinsCounter);
       addCointList.Add(reward);
       addCointList.Add(PrefsManager.GetCoinsValue());

        //StartOn();
        Time.timeScale = 1;
         Invoke("StartOn",0.5f);
        Invoke("ScoreCounter",0.5f);
       // Debug.Log("Start on  "+Time.timeScale);
    }
    void StartOn() {
        if(startsObj)
        startsObj.SetActive(true);
        //Debug.Log("Start on is working ");
    }
    void ScoreCounter()
    {
        StartCoroutine(AddCoins());
    }
  //  private int numIteration=3;
    public  IEnumerator AddCoins(){
    
        for (int i = 0; i < addCointList.Count; i++)
        {
            if (i==0) {
                numOfiteration = 3;
            }
            else {
                numOfiteration = 30;
            }
           // CounterPannel[i].SetActive(true);
            tempMoney = 0;

            //numOfiteration=
            // print("CallAdd Money");
            int conut = Mathf.FloorToInt(addCointList[i] / numOfiteration);
            int reminder = addCointList[i] % numOfiteration;
            //if (i==0) {
            //    numOfiteration = 3;
            //}
            //else {
            //    numOfiteration = 30;
            //}

            for (int j = 0; j < numOfiteration; j++)
            {
                coinsSource.PlayOneShot(coinsCountSound);
                tempMoney += conut;
                rewradMoneyText[i].text= tempMoney + "";
                yield return new WaitForSeconds(rate);
            }
            tempMoney += reminder;
            rewradMoneyText[i].text = tempMoney + "";
            coinsSource.Stop();
        }
        AllButton.SetActive(true);
      //  rewradMoneyText[0].text = LevelManager.instace.coinsCounter + "";
        rewradMoneyText[0].text = reward + "";
        rewradMoneyText[1].text = PrefsManager.GetCoinsValue() + "";
       addCointList.Clear();
        Time.timeScale = 0;
        Debug.Log("TImescale"+ Time.timeScale);
    }

    public void DoubleReward()
    {
        DoubleRewardBtn.SetActive(false);
        PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + reward);
        rewradMoneyText[1].text = PrefsManager.GetCoinsValue() + "";
        rewradMoneyText[0].text = reward * 2 + "";


    }

}

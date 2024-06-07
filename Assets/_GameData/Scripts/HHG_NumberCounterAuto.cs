using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HHG_NumberCounterAuto : MonoBehaviour {
    public int numOfiteration;
    public float rate;
    public AudioSource coinsSource;
    public Text[] rewradMoneyText;
    public List<int> addCointList=new List<int>();
    private int tempMoney;
    public AudioClip coinsCountSound;
	
    public int reward;
    // Use this for initialization
    private void OnChecking()
    {

		addCointList.Clear();
       //addCointList.Add(LevelManger.instance.coinsCounter);
       addCointList.Add(reward);
      // addCointList.Add(PrefsManager.GetCoinsValue());

    
        Time.timeScale = 1;
        // Invoke("StartOn",0.3f);
        Invoke("ScoreCounter",1f);
       // Debug.Log("Start on  "+Time.timeScale);
    }

	public void UpdateScore(int value) {
		addCointList.Clear();
		reward = value;
		addCointList.Add(value);
		Time.timeScale = 1;
		rewradMoneyText[0].text = "0";
		Invoke("ScoreCounter", 0.2f);
	}

    void StartOn() {

    }
    void ScoreCounter()
    {
        StartCoroutine(AddCoins());
    }
  //  private int numIteration=3;
    public  IEnumerator AddCoins(){
    
        for (int i = 0; i < addCointList.Count; i++)
        {
         
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
        
        rewradMoneyText[0].text =reward+"";
        //rewradMoneyText[0].text = reward + "";

       // print(GameData.Instance.GetCoins()+ "=GameData.Instance.GetCoins...."+ GlobalConstant.rewardCount+ "GlobalConstant.rewardCount");
       addCointList.Clear();
       
    }



}

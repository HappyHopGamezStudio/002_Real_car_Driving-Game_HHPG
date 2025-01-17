﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HHG_Mediation;
using UnityEngine;
using UnityEngine.UI;

public class HHG_MoneyCounter : MonoBehaviour
{
    public int numOfiteration;
    public float rate;
    public AudioSource coinsSource;
    public Text[] rewradMoneyText;
    public List<int> addCointList = new List<int>();
    private int tempMoney;
    public GameObject startsObj;
    public GameObject AllButton, DoubleRewardBtn,CoinAnimation;
    public AudioClip coinsCountSound;
    int reward;

    public bool isFail;

    // Use this for initialization
    private void OnEnable()
    {

        reward = HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.LevelReward;
        
        if (isFail)
            reward = reward - 800;
        PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + reward + (HHG_LevelManager.instace.coinsCounter));

        DoubleRewardBtn.SetActive(true);
        CoinAnimation.SetActive(false);
       // Contrinebtn.SetActive(false);
        addCointList.Clear();
        //     addCointList.Add(LevelManager.instace.coinsCounter);
        addCointList.Add(reward);
        addCointList.Add(PrefsManager.GetCoinsValue());

        //StartOn();
        Time.timeScale = 1;
        Invoke("StartOn", 0.5f);
        ScoreCounter();
        // Debug.Log("Start on  "+Time.timeScale);
    }

    void StartOn()
    {
        if (startsObj)
            startsObj.SetActive(true);
        //Debug.Log("Start on is working ");
    }

    void ScoreCounter()
    {
        StartCoroutine(AddCoins());
    }

    public float speed = 50f;

    //  private int numIteration=3;
    public IEnumerator AddCoins()
    {
        for (int i = 0; i < addCointList.Count; i++)
        {
            int targetMoney = addCointList[i]; // Final value to reach
            int currentMoney = 0; // Start at 0
            speed = 50f; // Speed of the counting (adjustable)

            tempMoney = 0;
            bool isPlayingSound = false; // Track if the sound is playing

            // Smoothly count from 0 to the target value
            while (currentMoney < targetMoney)
            {
                if (!isPlayingSound)
                {
                    coinsSource.PlayOneShot(coinsCountSound); // Play coin sound
                    isPlayingSound = true;
                }

                currentMoney += Mathf.CeilToInt(speed); // Increment by speed
                if (currentMoney > targetMoney)
                    currentMoney = targetMoney; // Clamp to target value

                rewradMoneyText[i].text = "$ " + currentMoney.ToString("N0"); // Update text
            
                // Stop the sound when the target money is reached
                if (currentMoney >= targetMoney && isPlayingSound)
                {
                    coinsSource.Stop();
                    isPlayingSound = false;
                }

                yield return new WaitForSeconds(0.02f); // Small delay for smooth effect
            }
        }

        AllButton.SetActive(true);

        // Final update to the reward text
        rewradMoneyText[0].text = "$ " + reward.ToString("N0");

        // Clear the coin list and stop the game time
        addCointList.Clear();
        Time.timeScale = 1;
        Debug.Log("TimeScale: " + Time.timeScale);
    }

    public void DoubleReward()
    {
        DoubleRewardBtn.SetActive(false);
        CoinAnimation.SetActive(true);
        PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + reward);
        // rewradMoneyText[1].text = PrefsManager.GetCoinsValue() + "";
        rewradMoneyText[0].text = "$ " + (reward * 2).ToString("N0");
   //     Contrinebtn.SetActive(true);
        // StartCoroutine(AddCoins());
        Invoke(nameof(offcoinAnmation),2f);
    }

    public void Claimreward()
    {
        CoinAnimation.SetActive(true);
        rewradMoneyText[0].text = "$ 0,000 " ;
        Invoke(nameof(offcoinAnmation),2f);
    }

    void offcoinAnmation()
    {
        CoinAnimation.SetActive(false);
    }

    private void OnDisable()
    {
        HHG_SoundManager.Instance.PlayBgSound();
    }
}

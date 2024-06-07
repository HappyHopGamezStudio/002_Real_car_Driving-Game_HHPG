/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace NiobiumStudios
{
	/**
     * The UI Logic Representation of the Daily Rewards
     **/
	public class DailyRewardsInterface : MonoBehaviour
	{
		public GameObject dailyRewardPanel;
		public GameObject dailyRewardPrefab;        // Prefab containing each daily reward

		[Header("Panel Debug")]
		public GameObject panelDebug;
		public bool isDebug;

		[Header("Panel Reward Message")]
		public GameObject panelReward;              // Rewards panel
		public GameObject paneldailyReward, AllRewardParent, Jeep1,Reward2x;              // Rewards panel
		public Text textReward, rewardCounter, Statments;                     // Reward Text to show an explanatory message to the player
		public Button buttonCloseReward;            // The Button to close the Rewards Panel

		[Header("Panel Reward")]
		public GameObject buttonClaimGo;            // Claim Button Game Object (To enable/disable)
		public GameObject buttonCloseGo;            // Close Button Game Object (To enable/disable)
		public Button buttonClaim;                  // Claim Button
		public Button buttonClose;                  // Close Button
		public Text textTimeDue;                    // Text showing how long until the next claim
		public GridLayoutGroup dailyRewardsGroup;   // The Grid that contains the rewards
		public ScrollRect scrollRect;               // The Scroll Rect
		public Image imageReward;                   // The image of the reward

		private bool readyToClaim;                  // Update flag
		public List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();

		void Awake()
		{
			dailyRewardPanel.SetActive(false);
		}

		void Start()
		{
			//InitializeDailyRewardsUI();
			panelDebug.SetActive(isDebug);
			buttonCloseGo.SetActive(false);

			buttonClaim.onClick.AddListener(() =>
			{
				DailyRewards.instance.ClaimPrize();
				readyToClaim = false;
				RickshwaObject.SetActive(false);
				UpdateUI();
			});

			buttonCloseReward.onClick.AddListener(() =>
			{
				var keepOpen = DailyRewards.instance.keepOpen;
				panelReward.SetActive(false);
				dailyRewardPanel.SetActive(keepOpen);
				HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
			});

			buttonClose.onClick.AddListener(() =>
			{
				dailyRewardPanel.SetActive(false);
				HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
			});
		}

		void OnEnable()
		{
			DailyRewards.onClaimPrize += OnClaimPrize;
			DailyRewards.onInitialize += OnInitialize;

			paneldailyReward.SetActive(true);
		}

		void OnDisable()
		{
			DailyRewards.onClaimPrize -= OnClaimPrize;
			DailyRewards.onInitialize -= OnInitialize;
		}

		// Initializes the UI List based on the rewards size
		private void InitializeDailyRewardsUI()
		{
			dailyRewardsUI.Clear();
			for (int i = 0; i < DailyRewards.instance.rewards.Count; i++)
			{
				int day = i + 1;
				var reward = DailyRewards.instance.GetReward(day);

				GameObject dailyRewardGo = Instantiate(dailyRewardPrefab) as GameObject;

				DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();
				dailyRewardUI.transform.SetParent(dailyRewardsGroup.transform);
				dailyRewardGo.transform.localScale = Vector2.one;

				dailyRewardUI.day = day;
				dailyRewardUI.reward = reward;
				dailyRewardUI.Initialize();

				dailyRewardsUI.Add(dailyRewardUI);
			}
		}

		public void UpdateUI()
		{
			DailyRewards.instance.CheckRewards();

			bool isRewardAvailableNow = false;

			var lastReward = DailyRewards.instance.lastReward;
			var availableReward = DailyRewards.instance.availableReward;

			foreach (var dailyRewardUI in dailyRewardsUI)
			{
				var day = dailyRewardUI.day;

				if (day == availableReward)
				{
					dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;

					isRewardAvailableNow = true;
				}
				else if (day <= lastReward)
				{
					dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;
				}
				else
				{
					dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
				}

				dailyRewardUI.Refresh();
			}

			buttonClaimGo.SetActive(isRewardAvailableNow);
			buttonCloseGo.SetActive(!isRewardAvailableNow);
			if (isRewardAvailableNow)
			{
				SnapToReward();
				textTimeDue.text = "You can claim your reward!";
			}
			readyToClaim = isRewardAvailableNow;
		}

		// Snap to the next reward
		public void SnapToReward()
		{
			Canvas.ForceUpdateCanvases();

			var lastRewardIdx = DailyRewards.instance.lastReward;

			// Scrolls to the last reward element
			if (dailyRewardsUI.Count <= lastRewardIdx)
			{
				lastRewardIdx++;
			}
			//var target = dailyRewardsUI[lastRewardIdx].GetComponent<RectTransform>();

			//var content = scrollRect.content;
			//dailyRewardsUI[lastRewardIdx].transform.GetChild(0).gameObject.SetActive(true);
			//dailyRewardsUI[lastRewardIdx].transform.GetChild(2).gameObject.SetActive(false);
		//	dailyRewardsUI[lastRewardIdx].GetComponent<TweenScale>().enabled = true;
			//content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

			//float normalizePosition = (float)target.GetSiblingIndex() / (float)content.transform.childCount;
			//scrollRect.verticalNormalizedPosition = normalizePosition;
			
		}
public GameObject RickshwaObject;
		void Update()
		{
			// Updates the time due
			if (!readyToClaim)
			{
				TimeSpan difference = (DailyRewards.instance.lastRewardTime - DailyRewards.instance.now).Add(new TimeSpan(0, 24, 0, 0));

				// If the counter below 0 it means there is a new reward to claim
				if (difference.TotalSeconds <= 0)
				{
					readyToClaim = true;
					UpdateUI();
					SnapToReward();
					return;
				}

				string formattedTs = string.Format("{0:D2}:{1:D2}:{2:D2}", difference.Hours, difference.Minutes, difference.Seconds);

				textTimeDue.text = string.Format("{0}", formattedTs);
			}


		}

        public bool isDebugPanels = false;

        // Delegate

        public int currentRewardPrice;

        private void OnClaimPrize(int day)
		{
			panelReward.SetActive(true);
			paneldailyReward.SetActive(false);
			var reward = DailyRewards.instance.GetReward(day);
			var unit = reward.unit;
			var rewardQt = reward.reward;
			imageReward.sprite = reward.sprite;

			textReward.text = reward.successMessage;

			if (reward.reward > 1)
			{
				textReward.gameObject.SetActive(false);
				AllRewardParent.SetActive(true);
				AllRewardParent.GetComponent<HHG_NumberCounterAuto>().UpdateScore(reward.reward);
                Statments.text= reward.successMessage;
                Jeep1.SetActive(false);
               
                Invoke("OnEffect",0.5f);
                Reward2x.SetActive(true);
                currentRewardPrice = reward.reward;
			}
			else {
				textReward.gameObject.SetActive(true);
				AllRewardParent.SetActive(false);

                if (day == 3){ Jeep1.SetActive(true); }
             
                Reward2x.SetActive(false);
            }
			//if (rewardQt > 0)
			//{
			//    textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);
			//}
			//else
			//{
			//    textReward.text = string.Format("You got {0}!", unit);
			//}
		}

		public void OnEffect() {
			AllRewardParent.transform.GetChild(0).gameObject.SetActive(true);
		}

        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                var showWhenNotAvailable = DailyRewards.instance.keepOpen;
                var isRewardAvailable = DailyRewards.instance.availableReward > 0;

                dailyRewardPanel.SetActive(showWhenNotAvailable || (!showWhenNotAvailable && isRewardAvailable));
                UpdateUI();

                SnapToReward();

                //DailyRewards.instance.ClaimPrize();
                //readyToClaim = false;
                //UpdateUI();


            }
        }

        // Resets player preferences
        public void OnResetClick()
        {
            DailyRewards.instance.Reset();
            DailyRewards.instance.lastRewardTime = System.DateTime.MinValue;
            readyToClaim = false;

            UpdateUI();
        }

        // Simulates the next day
        public void OnAdvanceDayClick()
        {
            DailyRewards.instance.now = DailyRewards.instance.now.AddDays(1);

            UpdateUI();
            DailyRewards.instance.ClaimPrize();
        }

        // Simulates the next hour
        public void OnAdvanceHourClick()
        {
            DailyRewards.instance.now = DailyRewards.instance.now.AddHours(1);

            UpdateUI();
        }

        public void Close2xReward() {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue()+currentRewardPrice);
            dailyRewardPanel.SetActive(false);
        }

    }



  




}
using System.Threading.Tasks;
using HHG_Mediation;
using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        public void VirtualSitOnbikeInput(bool virtualSprintState)
        {
            starterAssetsInputs.SitBikeInput(virtualSprintState);
        }
        public  async void ThrowInput(bool virtualSprintState)
        {
         
            HHG_UiManager.instance. AdBrakepanel.SetActive(true);
            await Task.Delay(1000);
            if (FindObjectOfType<HHG_AdsCall>())
            {
                FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
                PrefsManager.SetInterInt(1);
            }
         
            HHG_UiManager.instance.AdBrakepanel.SetActive(false);
            starterAssetsInputs.ThrowInput(virtualSprintState);
            await Task.Delay(2000);
            if (FindObjectOfType<HHG_AdsCall>())
            {
                if (PrefsManager.GetInterInt() != 5)
                {
                    FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
                }
            }
        }
    }

}

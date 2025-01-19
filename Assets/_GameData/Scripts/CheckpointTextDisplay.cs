using TMPro; // Import TextMeshPro namespace
using UnityEngine;
using UnityEngine.UI;

public class CheckpointTextDisplay : MonoBehaviour
{
    [SerializeField] private Text missionText; // Reference to the TextMeshProUGUI component

    private void OnEnable()
    {
        if (missionText == null)
        {
            Debug.LogError("Mission Text is not assigned in the Inspector.");
            return;
        }

        // Set the mission text with colored formatting
        string text = "<color=white>Pass through all the </color><color=Yellow>checkpoints</color><color=white>.</color>";
        missionText.text = text;
    }
}
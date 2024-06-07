using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public InputField nameInputField;
    public Text[] textsToUpdate;

    private const string playerNameKey = "Enter Name...";
    private string savedName;
    private void Start()
    {
      
        if (PlayerPrefs.HasKey(playerNameKey))
        { 
            savedName = PlayerPrefs.GetString(playerNameKey);
            nameInputField.text = savedName;
            UpdateTexts(savedName);
        }
    }

    public void SaveName()
    {
        string playerName = nameInputField.text;
        PlayerPrefs.SetString(playerNameKey, playerName);
        PlayerPrefs.Save();
        UpdateTexts(playerName);
    }

    private void UpdateTexts(string playerName)
    {
        // Update all texts with the player's name
        foreach (Text text in textsToUpdate)
        {
            text.text = "Hello, " + playerName + "!";
        }
    }
}

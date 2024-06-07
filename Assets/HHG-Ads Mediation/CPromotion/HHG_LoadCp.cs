using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class HHG_LoadCp : MonoBehaviour
{
    public int CpID;
    public RawImage YourRawImage;
    string link;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("removeads") == 1)
        {
            gameObject.SetActive(false);
        }

        YourRawImage.enabled = false;
        
        if (HHG_cpManager.instance._links[CpID].CpTexture != null)
        {
            YourRawImage.texture = HHG_cpManager.instance._links[CpID].CpTexture;
            YourRawImage.enabled = true;
            YourRawImage.GetComponent<Button>().interactable = true;
            YourRawImage.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(GetTextureOnline());
        }

        if (HHG_cpManager.instance._links[CpID].Link != null)
        {
            link = HHG_cpManager.instance._links[CpID].Link;
        }
        else
        {
            StartCoroutine(GetTextOnline());
        }
    }

    IEnumerator GetTextureOnline()
    {

        UnityWebRequest loadings = UnityWebRequestTexture.GetTexture(HHG_cpManager.instance._links[CpID].ImageUrl);
        yield return loadings.SendWebRequest();
        if (loadings.isDone)
        {
            if (!loadings.isNetworkError)
            {
                HHG_cpManager.instance._links[CpID].CpTexture = ((DownloadHandlerTexture)loadings.downloadHandler).texture != null ? ((DownloadHandlerTexture)loadings.downloadHandler).texture : ((DownloadHandlerTexture)loadings.downloadHandler).texture;
                YourRawImage.texture = HHG_cpManager.instance._links[CpID].CpTexture;
                YourRawImage.enabled = true;
                YourRawImage.GetComponent<Button>().interactable = true;
                YourRawImage.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    IEnumerator GetTextOnline()
    {
        for (int i = 0; i < HHG_cpManager.instance._links.Length; i++)
        {
            UnityWebRequest loadings = UnityWebRequest.Get(HHG_cpManager.instance._links[CpID].TextUrl);
            yield return loadings.SendWebRequest();
            if (loadings.isDone)
            {
                if (!loadings.isNetworkError)
                {
                    Debug.Log(loadings.downloadHandler.text);
                    HHG_cpManager.instance._links[CpID].Link = loadings.downloadHandler.text;
                }
            }
        }
    }


    public void OpenLink()
    {
        Debug.Log("link is " + HHG_cpManager.instance._links[CpID].Link);
        Application.OpenURL(HHG_cpManager.instance._links[CpID].Link);
       
    }
}

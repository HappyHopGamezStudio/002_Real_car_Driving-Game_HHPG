using UnityEngine;
using UnityEditor;
using HHG_Mediation;

[ExecuteInEditMode]
public class PluginCreation : EditorWindow
{
    [MenuItem("Elegant Games/ Create ElegantGames_AdmobManager")]
    public static void CreateAdsManager()
    {
        GameObject go = new GameObject("Elegant Games-Ads Manager");
        go.AddComponent<HHG_Admob>();
        Selection.activeObject = go;
    }

}

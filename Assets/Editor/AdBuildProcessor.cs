using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using HHG_Mediation;

public class AdBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {



        if (report.summary.platform != BuildTarget.Android)
            return;

        if (!report.summary.platformGroup.Equals(BuildTargetGroup.Android))
            return;

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        if (!EditorUserBuildSettings.buildAppBundle)
            return;

        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != 0)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(0);
            EditorSceneManager.OpenScene(path);
        }
        // Find all instances of your script in the project
        var scriptInstances = GameObject.FindObjectOfType<HHG_AdsCall>();
        // foreach (var instance in scriptInstances)
        {
            if (scriptInstances.testingMode || !scriptInstances.disbaleLogMode)
            {
                // Stop the build if testMode is true
                EditorUtility.DisplayDialog("Error", "Cannot build with test ADS or Logs Enable In AdmobManager.", "OK");
                throw new BuildFailedException("Build canceled due to test ads being enabled.");
            }
        }
        // Disable the testMode bool in each instance of your script
        //foreach (var instance in scriptInstances)
        //{
        //    instance.EnableTestModeAds = false;
        //}
    }


    public static void test()
    {
        throw new BuildPlayerWindow.BuildMethodException("Test Ads Are Enabled");
    }
}


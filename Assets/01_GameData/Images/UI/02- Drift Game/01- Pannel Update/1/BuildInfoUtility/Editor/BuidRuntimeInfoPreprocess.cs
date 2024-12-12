using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace BuildInfoUtility
{
	public class BuidRuntimeInfoPreprocess : IPreprocessBuildWithReport
	{
		public int callbackOrder => default;

		public void OnPreprocessBuild(BuildReport report)
		{
			BuidRuntimeInfo settings = BuidRuntimeInfo.Instance;
			if (settings != null)
			{
				settings.Version = PlayerSettings.bundleVersion;
				settings.BuildId = GetBuildId();
				EditorUtility.SetDirty(settings);
				AssetDatabase.SaveAssetIfDirty(settings);
			}
		}

		private int GetBuildId()
		{
#if UNITY_ANDROID
			return PlayerSettings.Android.bundleVersionCode;
#elif UNITY_IOS
			return PlayerSettings.iOS.buildNumber;
#else
			return default;
#endif
		}
	}
}
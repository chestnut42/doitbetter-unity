using UnityEditor;
using UnityEditor.Callbacks;

namespace Plugins.GameBoost
{
    public class GameBoostBuilder
    {
	    [PostProcessBuild(800)]
	    private static void OnPostProcessBuildPlayer(BuildTarget target, string pathToBuiltProject)
	    {
		    switch (target)
		    {
			    case BuildTarget.iOS:
				    PostProcessIosBuild(pathToBuiltProject);
				    break;
		    }
	    }

		private static void PostProcessIosBuild(string pathToBuiltProject)
		{
#if UNITY_IOS
			UnityEditor.iOS.Xcode.PBXProject project = new UnityEditor.iOS.Xcode.PBXProject();
			string pbxPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(pathToBuiltProject);
			project.ReadFromFile(pbxPath);

#if UNITY_2019_3_OR_NEWER
			string targetId = project.GetUnityFrameworkTargetGuid();
#else
			string targetId = project.TargetGuidByName(UnityEditor.iOS.Xcode.PBXProject.GetUnityTargetName());
#endif
			project.AddBuildProperty(targetId, "OTHER_LDFLAGS", "-ObjC");
			project.WriteToFile(pbxPath);
			GBLog.LogDebug("iOS post processor completed.");
#else
			GBLog.LogError("The active build target is not iOS");
#endif
		}
    }
}

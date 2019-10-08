using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AppBuilder
{

    [MenuItem("Build/Android向けビルド")]
    static void buildForAndroid()
    {
        bumpBuildNumberForAndroid();
        BuildReport buildReport = BuildPipeline.BuildPlayer(GetAllScenePaths(), "/tmp/todo-app.apk", BuildTarget.Android, BuildOptions.None);
        // // buildReport.resultを見て、戻り値を決定する
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            const int kSuccessCode = 0;
            Debug.Log("Success!!");
            Debug.Log(buildReport.summary);
//            EditorApplication.Exit(kSuccessCode);
        }
        else
        {
            const int kErrorCode = 1;
            Debug.Log("Error!!");
            Debug.Log(buildReport.summary);
//            EditorApplication.Exit(kErrorCode);
        }
    }

    [MenuItem("Build/iOS向けビルド")]
    static void buildForIOS()
    {
        bumpBuildNumberForIOS();
        // 出力パス。絶対パスで指定すること。また、最後にスラッシュを入れないこと。PostBuildProcess に渡る path が通常ビルドと異なってしまい、思わぬバグを引き起こすことがあります。
        string path = "/tmp/todo-app";
        BuildReport buildReport = BuildPipeline.BuildPlayer(GetAllScenePaths(), path, BuildTarget.iOS, BuildOptions.None);
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            const int kSuccessCode = 0;
            EditorApplication.Exit(kSuccessCode);
        }
        else
        {
            const int kErrorCode = 1;
            EditorApplication.Exit(kErrorCode);
        }
    }

    static void bumpBuildNumberForIOS()
    {
        string str = PlayerSettings.iOS.buildNumber;
        int num = int.Parse(str);
        num++;
        PlayerSettings.iOS.buildNumber = num + "";
    }

    static void bumpBuildNumberForAndroid()
    {
        PlayerSettings.Android.bundleVersionCode += 1;
    }

    static string[] GetAllScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
}
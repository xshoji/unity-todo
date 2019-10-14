using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AppBuilder
{
    private static int argumentCount = 1;

    [MenuItem("Build/Android向けビルド")]
    static void buildForAndroid()
    {
        bumpBuildNumberForAndroid();
        // 出力パス。絶対パスで指定すること。また、最後にスラッシュを入れないこと。PostBuildProcess に渡る path が通常ビルドと異なってしまい、思わぬバグを引き起こすことがあります。
        String outputPath = GetArgument(11);
        if (outputPath == null)
        {
            outputPath = "/tmp/todo-app";
        }
        outputPath = outputPath + ".apk";
        BuildReport buildReport = BuildPipeline.BuildPlayer(GetAllScenePaths(), outputPath, BuildTarget.Android, BuildOptions.None);
        // // buildReport.resultを見て、戻り値を決定する
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            PrintString("Success! Output : " + outputPath);
            PrintString(buildReport.summary.ToString());
        }
        else
        {
            PrintString("Error!");
            PrintString(buildReport.summary.ToString());
        }
    }

    [MenuItem("Build/iOS向けビルド")]
    static void buildForIOS()
    {
        bumpBuildNumberForIOS();
        String outputPath = GetArgument(11);
        if (outputPath == null)
        {
            outputPath = "/tmp/todo-app";
        }
        outputPath = outputPath + "_ios";
        BuildReport buildReport = BuildPipeline.BuildPlayer(GetAllScenePaths(), outputPath, BuildTarget.iOS, BuildOptions.None);
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            PrintString("Success! Output : " + outputPath);
            PrintString(buildReport.summary.ToString());
        }
        else
        {
            PrintString("Error!");
            PrintString(buildReport.summary.ToString());
        }
    }

    [MenuItem("Build/Mac向けビルド")]
    static void buildForMac()
    {
        String outputPath = GetArgument(11);
        if (outputPath == null)
        {
            outputPath = "/tmp/todo-app";
        }
        outputPath = outputPath + ".app";
        BuildReport buildReport = BuildPipeline.BuildPlayer(GetAllScenePaths(), outputPath, BuildTarget.StandaloneOSX, BuildOptions.None);
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            PrintString("Success! Output : " + outputPath);
            PrintString(buildReport.summary.ToString());
        }
        else
        {
            PrintString("Error!");
            PrintString(buildReport.summary.ToString());
        }
    }

    [MenuItem("Build/Windows向けビルド")]
    static void buildForWindows()
    {
        String outputPath = GetArgument(11);
        if (outputPath == null)
        {
            outputPath = "/tmp/todo-app";
        }
        outputPath = outputPath + ".exe";
        BuildReport buildReport = BuildPipeline.BuildPlayer(GetAllScenePaths(), outputPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            PrintString("Success! Output : " + outputPath);
            PrintString(buildReport.summary.ToString());
        }
        else
        {
            PrintString("Error!");
            PrintString(buildReport.summary.ToString());
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

    static void PrintString(string text) {
        System.Console.WriteLine(text + "\n");
        Debug.Log(text);
    }

    static String GetArgument(int index) {
        String[] arguments = Environment.GetCommandLineArgs();

        int i = 0;
        foreach (String argument in arguments)
        {
            PrintString(i.ToString());
            PrintString(argument);
            i++;
        }

        if (index < arguments.Length) {
            return arguments[index];
        }
        return null;
    }
}
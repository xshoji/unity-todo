//  PlayerPrefsResetter.cs
//  http://kan-kikuchi.hatenablog.com/entry/PlayerPrefsResetter
//
//  Created by kan.kikuchi on 2016.08.04.

using UnityEngine;
using UnityEditor;

/// <summary>
/// PlayerPrefsを初期化するクラス
/// </summary>
public static class PlayerPrefsResetter
{

	[MenuItem("Tools/Reset PlayerPrefs")]
	public static void ResetPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();

		Debug.Log("Reset PlayerPrefs");
	}

}
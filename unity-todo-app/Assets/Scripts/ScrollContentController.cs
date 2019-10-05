using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContentController : MonoBehaviour
{

    public void AddTask() {
        // > 【Unity】スクリプトからPrefabのインスタンスを作る方法 - Qiita
        // > https://qiita.com/2dgames_jp/items/8a28fd9cf625681faf87
        // プレハブを取得
        GameObject taskElementPrefab = (GameObject)Resources.Load("Prefabs/TaskElement");
        // プレハブからインスタンスを生成
        GameObject taskElementObj = Instantiate(taskElementPrefab) as GameObject;
        // あるオブジェクトの子として追加
        // > 【Unity】GameObjectをSetParentするとエラーになる件 - NinaLabo
        // > http://ninagreen.hatenablog.com/entry/2015/10/25/000444
        taskElementObj.transform.parent = transform;
        // > Unity - Scripting API： Transform.localScale
        // > https://docs.unity3d.com/ScriptReference/Transform-localScale.html?_ga=2.65667310.1187445378.1569860193-888141676.1564340563
        taskElementObj.transform.localScale = new Vector3(1, 1, 1);
    }

}

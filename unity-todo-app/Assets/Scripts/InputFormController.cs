using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFormController : MonoBehaviour
{

    public InputField inputField;
    public GameObject taskArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTask()
    {
        // > 【Unity】スクリプトからPrefabのインスタンスを作る方法 - Qiita
        // > https://qiita.com/2dgames_jp/items/8a28fd9cf625681faf87
        // プレハブを取得
        GameObject taskElementPrefab = (GameObject)Resources.Load("Prefabs/TaskElement");
        // プレハブからインスタンスを生成
        GameObject taskElementObject = Instantiate(taskElementPrefab) as GameObject;

        // formのtextをtextObjectに設定
        // > unity3d - C# Unity - How can I clear an InputField - Stack Overflow  
        // > https://stackoverflow.com/questions/37754617/c-sharp-unity-how-can-i-clear-an-inputfield
        // > UnityでGetComponent<Text>()でのエラー - conf t
        // > https://monaski.hatenablog.com/entry/2015/11/03/125957
        // > TextMesh Pro - From unity UI to TextMeshPro -Unity Forum
        // > https://forum.unity.com/threads/from-unity-ui-to-textmeshpro.463619/
        taskElementObject.transform.Find("TaskRawImage").transform.Find("TaskText").GetComponent<TextMeshProUGUI>().text = inputField.text;

        //        taskElementObject.GetComponent<Text>().text = inputField.text;
        inputField.Select();
        inputField.text = null;

        // あるオブジェクトの子として追加
        // > 【Unity】GameObjectをSetParentするとエラーになる件 - NinaLabo
        // > http://ninagreen.hatenablog.com/entry/2015/10/25/000444
        taskElementObject.transform.SetParent(taskArea.transform);
        // > Unity - Scripting API： Transform.localScale
        // > https://docs.unity3d.com/ScriptReference/Transform-localScale.html?_ga=2.65667310.1187445378.1569860193-888141676.1564340563
        taskElementObject.transform.localScale = new Vector3(1, 1, 1);
    }
}

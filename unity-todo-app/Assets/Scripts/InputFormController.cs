using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InputFormController : MonoBehaviour
{

    public InputField inputField;
    public GameObject taskArea;
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
        // タスク一覧取得
        var tasks = StorageManager.GetAll();
        if (tasks == null) {
            Debug.Log("tasks : null");
            tasks = new Dictionary<string, string>();
        }
        Debug.Log("tasks count : " + tasks.Count);
        // タスクに加えて保存
        var key = Convert.ToString((DateTime.UtcNow - UnixEpoch).TotalMilliseconds);
        tasks.Add(key, inputField.text);
        StorageManager.Save(tasks);

        // タスク一覧に加える
        taskArea.transform.GetComponent<ScrollContentController>().AddTask(key, inputField.text);

        // フォームをクリアする
        inputField.Select();
        inputField.text = null;
    }
}

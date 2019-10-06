using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollContentController : MonoBehaviour
{
    void Start() {
        DisplayTasks();
    }

    public void DisplayTasks() {
        var tasks = StorageManager.GetAll();
        Debug.Log("tasks initial count : " + tasks.Count);
        if (tasks == null) {
            return;
        }
        foreach (var task in tasks) {
            AddTask(task.Key, task.Value);
        }
    }

    public void AddTask(string key, string task)
    {
        // プレハブからインスタンスを生成
        GameObject taskElementPrefab = (GameObject)Resources.Load("Prefabs/TaskElement");
        GameObject taskElementObject = Instantiate(taskElementPrefab) as GameObject;
        taskElementObject.transform.GetComponent<TaskElementController>().Key = key;
        taskElementObject.transform.Find("TaskRawImage").transform.Find("TaskText").GetComponent<TextMeshProUGUI>().text = task;
        taskElementObject.transform.SetParent(transform);
        // > Unity - Scripting API： Transform.localScale
        // > https://docs.unity3d.com/ScriptReference/Transform-localScale.html?_ga=2.65667310.1187445378.1569860193-888141676.1564340563
        taskElementObject.transform.localScale = new Vector3(1, 1, 1);
    }
}

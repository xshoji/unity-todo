using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskElementController : MonoBehaviour
{
    public string Key;
    public void DeleteSelf() {

        // タスク一覧取得
        var tasks = StorageManager.GetAll();
        if (tasks == null)
        {
            Debug.Log("tasks : null");
            tasks = new Dictionary<string, string>();
        }
        Debug.Log("tasks count : " + tasks.Count);
        // タスクから削除して保存
        tasks.Remove(Key);
        StorageManager.Save(tasks);

        // 一覧からも削除
        Destroy(gameObject);
    }
}

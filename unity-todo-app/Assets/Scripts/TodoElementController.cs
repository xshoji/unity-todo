using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoElementController : MonoBehaviour
{
    public string Key;
    public void DeleteSelf() {

        // Todo一覧取得
        var todoList = StorageManager.GetAll();
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, string>();
        }
        Debug.Log("todoList count before deleting : " + todoList.Count);
        // Todoから削除して保存
        todoList.Remove(Key);
        StorageManager.Save(todoList);
        Debug.Log("todoList count after deleting : " + todoList.Count);

        // 一覧からも削除
        Destroy(gameObject);
    }
}

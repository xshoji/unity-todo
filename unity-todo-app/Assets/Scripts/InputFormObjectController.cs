using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InputFormObjectController : MonoBehaviour
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
        // Todo一覧取得
        var todoList = StorageManager.GetAll();
        if (todoList == null) {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, string>();
        }
        Debug.Log("todoList count : " + todoList.Count);
        // Todoに加えて保存
        var key = Convert.ToString((DateTime.UtcNow - UnixEpoch).TotalMilliseconds);
        todoList.Add(key, inputField.text);
        StorageManager.Save(todoList);

        // Todo一覧に加える
        taskArea.transform.GetComponent<TodoListContentController>().AddTodo(key, inputField.text);

        // フォームをクリアする
        inputField.Select();
        inputField.text = null;
    }
}

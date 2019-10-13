using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TodoListContentController : MonoBehaviour
{
    void Start() {
        DisplayTodoList();
    }

    public void DisplayTodoList() {
        var todoList = StorageManager.GetAll();
        Debug.Log("todolist initial count : " + todoList.Count);
        if (todoList == null) {
            return;
        }
        foreach (var todo in todoList) {
            AddTodo(todo.Key, todo.Value);
        }
    }

    public void AddTodo(string key, string todoText)
    {
        // プレハブからインスタンスを生成
        GameObject todoElementPrefab = (GameObject)Resources.Load("Prefabs/TodoElement");
        GameObject todoElementObject = Instantiate(todoElementPrefab) as GameObject;
        todoElementObject.transform.GetComponent<TodoElementController>().Key = key;
        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoText").GetComponent<Text>().text = todoText;
        todoElementObject.transform.SetParent(transform);
        // > Unity - Scripting API： Transform.localScale
        // > https://docs.unity3d.com/ScriptReference/Transform-localScale.html?_ga=2.65667310.1187445378.1569860193-888141676.1564340563
        todoElementObject.transform.localScale = new Vector3(1, 1, 1);
    }
}

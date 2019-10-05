using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskElementController : MonoBehaviour
{

    public void DeleteSelf() {
        Destroy(transform.parent.transform.parent.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FpsTextController : MonoBehaviour
{
    int frameCount;
    float prevTime;

    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
    }

    void Update()
    {
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            //Debug.LogFormat("{0}fps", frameCount / time);
            gameObject.transform.GetComponent<Text>().text = "FPS : " + frameCount.ToString();

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}

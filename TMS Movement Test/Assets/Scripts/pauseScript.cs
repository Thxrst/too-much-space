using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{
    public bool isPaused;
    public KeyCode keyCode;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            
            isPaused = !isPaused;
        }
        Time.timeScale = isPaused ? 0 : 1;
    }
    void OnGUI()
    {
        if(isPaused)
        GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
    }

}

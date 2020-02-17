using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // public stuff is for testing purposes:
    public GameObject canvas;
    private bool paused;

    void Awake()
    {

    }

    void Update()
    {

    }

    public void Pause()
    {
        print("pause was called");

        if (!paused)
        {
            canvas.SetActive(true);
            Time.timeScale = 0;
        } else if (paused)
        {
            canvas.SetActive(false);
            Time.timeScale = 1;
        }

        paused = !paused;
    }


}

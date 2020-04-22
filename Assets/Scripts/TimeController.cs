using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    //show static variables in inspector
    public float inspectorStartTime = 99;

    public static float currentTime = 0;
    public static float startTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimers(inspectorStartTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.gameOn)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public static void ResetTimers(float resetToAmount)
    {
        startTime = resetToAmount;
        currentTime = startTime;
    }
}

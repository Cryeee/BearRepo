using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    //show static variables in inspector
    public float inspectorTime;
    public float inspectorRoundTime;

    public static float time = 0;
    public static float roundTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimers(0);
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
    }

    public void ResetTimers(float resetToAmount)
    {
        time = resetToAmount;
        roundTime = resetToAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    float roundTimeLimitEditValue;
    public static float roundTimeLimit;

    public bool roundEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        roundTimeLimit = roundTimeLimitEditValue;

        if(roundTimeLimit < TimeController.roundTime && roundEnded == false)
        {
            roundEnded = true;
            TimeLimitReached();
        }
    }

    void TimeLimitReached()
    {

        Debug.Log("Round Ended!!!!!!!!!");

    }

}

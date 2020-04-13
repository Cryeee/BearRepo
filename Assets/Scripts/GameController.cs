using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    float roundTimeLimitEditValue;
    public static float roundTimeLimit;

    [SerializeField]
    float targetFoodAmountValue;
    public static float targetFoodAmount;

    public TMP_Text text;

    public bool roundEnded = false;
    public bool enoughEaten = false;

    private PlayerScript playerScript;

    public int mapID;
    public int amountNeededFor1Star;
    public int amountNeededFor2Stars;
    public int amountNeededFor3Stars;


    public bool skipStartCutsceneButton = true;
    public static bool skipCutscene = true;
    public float startCutsceneTime = 10;
    public static Action OnGameStart;
    public static bool gameOn = false;

    private void Awake()
    {
        // So we don't have to watch the start animation every time:
        skipCutscene = skipStartCutsceneButton;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerScript>();

        // Set skipCutsceneButton to true on inspector to skip start:
        if (!skipCutscene)
        {
            Invoke("GameStart", startCutsceneTime);
        }
        else
        {
            GameStart();
        }
    }

    void GameStart()
    {
        // Tell listeners that gameplay starts now:
        // Controls, collider, camera movement enable
        OnGameStart?.Invoke();

        // Tell UI elements to be visible
        // (Time- and Food UI scripts check this bool on update)
        gameOn = true;
    }


    // Update is called once per frame
    void Update()
    {
        roundTimeLimit = roundTimeLimitEditValue;
        targetFoodAmount = targetFoodAmountValue;

        if(roundTimeLimit < TimeController.roundTime && roundEnded == false)
        {
            roundEnded = true;
            CheckStars(playerScript.AmountOfFoodEaten);
            TimeLimitReached();
        }
        
        if(playerScript.AmountOfFoodEaten >= targetFoodAmount)
        {
            enoughEaten = true;
        }
    }

    void TimeLimitReached()
    {
        text.gameObject.SetActive(true);
        Debug.Log("Round Ended!!!!!!!!!");
        Invoke("RestartScene", 2);
    }

    public void RestartScene()
    {
        TimeController.ResetTimers(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CheckStars(float foodEaten)
    {
        int score = 0;

        if(foodEaten >= amountNeededFor1Star)
        {
            score = 1;
        }
        if (foodEaten >= amountNeededFor2Stars)
        {
            score = 2;
        }
        if (foodEaten >= amountNeededFor3Stars)
        {
            score = 3;
        }

        if(score > StaticScoreScript.starArray[mapID])
        {
            StaticScoreScript.starArray[mapID] = score;
        }
    }
}

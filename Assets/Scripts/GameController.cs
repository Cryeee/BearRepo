using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    //[SerializeField]
    //float roundTimeLimitEditValue = 0;
    //public static float roundTimeLimit;

    [SerializeField]
    float targetFoodAmount = 1;

    public static float targetFoodAmountValue;

    public float timeFor1Star;
    public float timeFor2Stars;
    public float timeFor3Stars;

    public static int stars = 0;

    public bool skipStartCutsceneButton = true;
    public static bool skipCutscene = true;
    public float startCutsceneTime = 10;
    public static Action OnGameStart;
    public static Action OnGameEnd;
    public static bool gameOn = false;

    private void Awake()
    {
        // So we don't have to watch the start animation every time:
        skipCutscene = skipStartCutsceneButton;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetFoodAmountValue = targetFoodAmount;

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

    void GameEnd()
    {
        gameOn = false;
        CountScore();
        OnGameEnd?.Invoke();
        Invoke("BackToMenu", 4f);
        //CheckStars(playerScript.AmountOfFoodEaten);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(PlayerScript.AmountOfFoodEaten >= targetFoodAmount && gameOn)
        {
            GameEnd();
        }
    }

    private void CountScore()
    {
        if(TimeController.time <= timeFor3Stars)
        {
            // 3 tähteä
            stars = 3;
        } else if (TimeController.time <= timeFor2Stars)
        {
            //2 tähteä
            stars = 2;
        } else if(TimeController.time <= timeFor1Star)
        {
            // 1 tähti
            stars = 1;
        } else if(TimeController.time > timeFor1Star)
        {
            // 0 tähteä
            stars = 0;
        }
    }

    //void TimeLimitReached()
    //{
    //    text.gameObject.SetActive(true);
    //    Debug.Log("Round Ended!!!!!!!!!");
    //    Invoke("RestartScene", 2);
    //}

    //public void RestartScene()
    //{
    //    TimeController.ResetTimers(0);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //void CheckStars(float foodEaten)
    //{
    //    int score = 0;

    //    if(foodEaten >= amountNeededFor1Star)
    //    {
    //        score = 1;
    //    }
    //    if (foodEaten >= amountNeededFor2Stars)
    //    {
    //        score = 2;
    //    }
    //    if (foodEaten >= amountNeededFor3Stars)
    //    {
    //        score = 3;
    //    }

    //    if(score > StaticScoreScript.starArray[mapID])
    //    {
    //        StaticScoreScript.starArray[mapID] = score;
    //    }
    //}
}

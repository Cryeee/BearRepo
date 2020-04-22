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
    public static int nextWeightGoal;

    public int weightFor1Star;
    public int weightFor2Stars;
    public int weightFor3Stars;

    private static int weight1;
    private static int weight2;
    private static int weight3;

    public static int stars = 0;

    public bool skipStartCutsceneButton = true;
    public static bool skipCutscene = true;
    public float startCutsceneTime = 10;
    public static Action OnGameStart;
    public static Action OnGameEnd;
    public static bool gameOn = false;

    private void Awake()
    {
        // So we don't have to watch the start animation every currentTime:
        skipCutscene = skipStartCutsceneButton;
    }

    // Start is called before the first frame update
    void Start()
    {

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
        nextWeightGoal = weightFor1Star;
        weight1 = weightFor1Star;
        weight2 = weightFor2Stars;
        weight3 = weightFor3Stars;

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

    public static void SetNewWeightGoal(float amountEaten)
    {
        if(amountEaten >= nextWeightGoal)
        {
            stars++;
            switch (stars)
            {
                case 1:
                    nextWeightGoal = weight2;
                    break;
                case 2:
                    nextWeightGoal = weight3;
                    break;
                case 3:
                    nextWeightGoal = weight3;
                    break;
                default:
                    nextWeightGoal = weight1;
                    break;
            }
        }
    }

    private void Update()
    {
        if (TimeController.currentTime <= 0 && gameOn)
        {
            GameEnd();
        }
    }

    private void CountScore()
    {
        if (PlayerScript.AmountOfFoodEaten >= weightFor3Stars)
        {
            // 3 tähteä
            stars = 3;
        }
        else if (PlayerScript.AmountOfFoodEaten >= weightFor2Stars)
        {
            //2 tähteä
            stars = 2;
        }
        else if (PlayerScript.AmountOfFoodEaten >= weightFor1Star)
        {
            // 1 tähti
            stars = 1;
        }
        else if (PlayerScript.AmountOfFoodEaten < weightFor1Star)
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

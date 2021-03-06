﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameController : MonoBehaviour
{
    [Header("Number of foods on this level:")]
    public int goldenBerries;
    public int cranberries;
    public int blueberries;
    public int mushrooms;
    public int glowingMushrooms;
    public int rabbits;
    public int fish;
    public int birds;

    public static int nextWeightGoal;

    public int weightFor1Star;
    public int weightFor2Stars;
    public int weightFor3Stars;

    public static int weight1;
    public static int weight2;
    public static int weight3;

    public static int stars = 0;

    //public bool skipStartCutsceneButton = true;
    public bool skipCutscene;
    public float startCutsceneTime = 10;
    public static Action OnGameStart;
    public static Action OnGameEnd;
    public static bool gameOn = false;

    private BearSkins bearSkins;
    private bool foundGoldenBerry;

    private void OnEnable()
    {
        GoldenBerry.OnPickedGoldenBerry += GoldenBerryFound;
    }

    private void GoldenBerryFound()
    {
        foundGoldenBerry = true;
    }

    private void Awake()
    {
        //// So we don't have to watch the start animation every currentTime:
        //skipCutscene = skipStartCutsceneButton;
        stars = 0;
        bearSkins = GetComponent<BearSkins>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerSkin();
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
        ResultScreen.StartFoodCounting();
        ResultScreen.lastScene = SceneManager.GetActiveScene().buildIndex;
        SetMaxFoods();
    }

    void SetMaxFoods()
    {
        ResultScreen.maxFoodValues[0] = cranberries;
        ResultScreen.maxFoodValues[1] = rabbits;
        ResultScreen.maxFoodValues[2] = mushrooms;
        ResultScreen.maxFoodValues[3] = glowingMushrooms;
        ResultScreen.maxFoodValues[4] = blueberries;
        ResultScreen.maxFoodValues[5] = fish;
        ResultScreen.maxFoodValues[6] = birds;

        ResultScreen.maxTotalCount = ResultScreen.maxFoodValues.Sum();
    }

    private void SetPlayerSkin()
    {
        bearSkins.SetSkin(BearSkins.currentSkin);
    }

    void GameEnd()
    {
        FindObjectOfType<AudioManager>().Play("TikTok");
        gameOn = false;
        CountScore();
        OnGameEnd?.Invoke();
        if(foundGoldenBerry)
        {
            ResultScreen.hasGoldenBerry = true;
        } else
        {
            ResultScreen.hasGoldenBerry = false;
        }
        Invoke("ToResultScreen", 3f);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void ToResultScreen()
    {
        SceneManager.LoadScene(4);
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

    private void OnDisable()
    {
        gameOn = false;
        GoldenBerry.OnPickedGoldenBerry -= GoldenBerryFound;
    }
}

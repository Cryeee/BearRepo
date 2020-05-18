using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class ResultScreen : MonoBehaviour
{
    public static int[] maxFoodValues;
    public static int[] foodCounter;
    public static int totalCount;
    public static int maxTotalCount;

    public TMP_Text[] textFields;
    public TMP_Text[] valueFields;
    public TMP_Text totalValue;

    List<FoodItem> foodItems = new List<FoodItem>();

    private BearSkins bearSkins;
    public Animator canvasAnimator;

    public Slider weightSlider;
    public float fillSpeed = 0.2f;
    public float targetValue;
    private bool runSlider;

    public static bool hasGoldenBerry;
    public GameObject goldenBerry;

    public static int lastScene;

    private PlayerData saveFile;

    public static void StartFoodCounting()
    {
        foodCounter = new int[7];
        maxFoodValues = new int[7];
    }

    private void Awake()
    {
        bearSkins = GetComponent<BearSkins>();
        SetResults();
        DisplayResults();
        DisplayStars();
        SetPlayerSkin();
        
        weightSlider.value = 0;
        Invoke("SetFatnessSlider", 1f);
    }


    private void Start()
    {
        // show cursor on result screen:
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SetPlayerFatness();
        SaveResults();
    }

    private void SetPlayerFatness()
    {
        GetComponent<BearSkins>().skinnyBear.SetBlendShapeWeight(3, PlayerScript.AmountOfFoodEaten / GameController.weight3 * 100);
    }

    private void Update()
    {
        if(runSlider)
        {
            weightSlider.value = Mathf.MoveTowards(weightSlider.value, targetValue, fillSpeed * Time.deltaTime);
        }

        Debug.Log(targetValue);
        
    }

    private void SetPlayerSkin()
    {
        bearSkins.SetSkin(BearSkins.currentSkin);
    }

    public void SetFatnessSlider()
    {
        float matka;
        float prosentti;
        float yli;

        switch (GameController.stars)
        {
            case 0:
                targetValue = PlayerScript.AmountOfFoodEaten / GameController.weight1 * 0.33f;
                break;
            case 1:
                // can't be over 0.66f
                //targetValue = 0.33f; TÄMÄ ON PROSENTTIMÄÄRÄ OSOITTAMAAN 1. TÄHTEÄ

                // välimatka 1. ja 2. tähden välillä
                matka = GameController.weight2 - GameController.weight1;

                // paljonko syötiin yli 2. tähteen vaaditavan määrän
                yli = PlayerScript.AmountOfFoodEaten - GameController.weight1;

                // paljonko ylisyöty osa on prosentteina 2. ja 3. tähden välimatkasta
                prosentti = (yli / matka) * 0.33f;

                // 1 tähti on jo, eli 33% + paljonko loppusyöntimäärä on koko sliderin prosenteista
                targetValue = prosentti + 0.33f;

                break;
            case 2:
                // can't be over 1
                //targetValue = 0.66f;
                // välimatka 2. ja 3. tähden välillä
                matka = GameController.weight3 - GameController.weight2;

                // paljonko syötiin yli 2. tähteen vaaditavan määrän
                yli = PlayerScript.AmountOfFoodEaten - GameController.weight2;

                // paljonko ylisyöty osa on prosentteina 2. ja 3. tähden välimatkasta
                prosentti = (yli / matka) * 0.33f;

                // 2 tähteä eli 66% + paljonko loppusyöntimäärä on koko sliderin prosenteista
                targetValue = prosentti + 0.66f;
                break;
            case 3:
                // jos saatiin 3 tähteä, aseta slideri sataan prosenttiin
                targetValue = 1;
                break;
        }

        runSlider = true;
    }


    public void SetResults()
    {
        foodItems.Add(new FoodItem("Cranberries", foodCounter[0]));
        foodItems.Add(new FoodItem("Rabbits", foodCounter[1]));
        foodItems.Add(new FoodItem("Mushrooms", foodCounter[2]));
        foodItems.Add(new FoodItem("Glowing mushrooms", foodCounter[3]));
        foodItems.Add(new FoodItem("Blueberries", foodCounter[4]));
        foodItems.Add(new FoodItem("Fish", foodCounter[5]));
        foodItems.Add(new FoodItem("Birds", foodCounter[6]));
    }

    private void DisplayResults()
    {
        int index = 0;

        // Go through all foods
        for (int i = 0; i < maxFoodValues.Length; i++)
        {
            // Was there any of these foods in the level?
            if (maxFoodValues[i] > 0)
            {
                // if yes, set first text to display collected cranberries:
                textFields[index].text = foodItems[i].foodName;

                valueFields[index].text = foodItems[i].amount + " / " + maxFoodValues[i];

                // add foods to total amount:
                totalCount += foodItems[i].amount;

                index++;
            }
            else
            {
                Debug.Log(maxFoodValues[i] + "  food not found on level");
            }
        }

        totalValue.text = totalCount.ToString() + " / " + maxTotalCount.ToString();
    }

    private void DisplayStars()
    {
        canvasAnimator.SetInteger("stars", GameController.stars);
        canvasAnimator.SetBool("goldenBerry", hasGoldenBerry);
        if(hasGoldenBerry)
        {
            goldenBerry.SetActive(true);
        }

        if(totalCount >= maxTotalCount)
        {
            canvasAnimator.SetInteger("stars", 4);
            StartCoroutine(playAudio("Star4", 3.3f));
            StartCoroutine(playAudio("Clap", 3.3f));
        }
        if (GameController.stars >= 1)
        {
            StartCoroutine(playAudio("Star1", 1.1f));
        }

        if (GameController.stars >= 2)
        {
            StartCoroutine(playAudio("Star2", 1.8f));
        }

        if (GameController.stars >= 3)
        {
            StartCoroutine(playAudio("Star3", 2.5f));
        }
    }

    IEnumerator playAudio(string clipName, float waitTIme)
    {
        yield return new WaitForSeconds(waitTIme);
        FindObjectOfType<AudioManager>().Play(clipName);
    }

    private void OnDisable()
    {
        Debug.Log("Disabled results");

        for (int i = 0; i < foodCounter.Length; i++)
        {
            foodCounter[i] = 0;
        }

        for (int i = 0; i < maxFoodValues.Length; i++)
        {
            maxFoodValues[i] = 0;
        }

        totalCount = 0;
        maxTotalCount = 0;
        targetValue = 0;
    }

    // If results were better than what were in saveFile, override them
    private void SaveResults()
    {
        saveFile = SaveLoadManager.Load();
        int level = lastScene - 1;

        // jos saatiin enemmän tähtiä kun save filessä, savee ne
        if(saveFile.stars[level] < GameController.stars)
        {
            saveFile.stars[level] = GameController.stars;

            // unlockkaa levelit jos tähdet riittää
            if (saveFile.stars.Sum() >= 4)
            {
                saveFile.unlockedLevels = 2;
            } else if(saveFile.stars.Sum() >= 2)
            {
                saveFile.unlockedLevels = 1;
            }
        }

        // jos ei vielä ole goldenberryä ja se saatiin, tallenna
        if(saveFile.goldenBerriesCollected[level] == false)
        {
            if(hasGoldenBerry)
            {
                saveFile.goldenBerriesCollected[level] = true;
            }

            // laske paljonko yhteensä kerätty golden berryjä:
            int berries = 0;
            for (int i = 0; i < 3; i++)
            {
                if(saveFile.goldenBerriesCollected[i] == true)
                {
                    berries++;
                }
            }

            // unlockkaa skinit berrien mukaan
            if(berries >= 3)
            {
                saveFile.unlockedSkins = 3;
            } else if(berries >= 2)
            {
                saveFile.unlockedSkins = 2;
            } else if(berries >= 1)
            {
                saveFile.unlockedSkins = 1;
            } else
            {
                saveFile.unlockedSkins = 0;
            }
        }

        // jos syötiin levelistä kaikki ekan kerran, tallenna tieto
        if(saveFile.allEatenOnLevel[level] == false)
        {
            int clearedLevels = 0;

            if(totalCount >= maxTotalCount)
            {
                saveFile.allEatenOnLevel[level] = true;
            }

            for (int i = 0; i < 3; i++)
            {
                if(saveFile.allEatenOnLevel[i] == true)
                {
                    clearedLevels++;
                }
            }

            // jos kaikki levelit clearattu, unlockkaa nalle puh skini
            if(clearedLevels == 3)
            {
                saveFile.unlockedSkins = 4;
            }
        }

        // Tallenna lopuksi tiedosto
        SaveLoadManager.Save(saveFile);
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("pilli");
        StartCoroutine(LoadSceneIE(0));
    }

    public void Replay()
    {
        FindObjectOfType<AudioManager>().Play("pilli");
        StartCoroutine(LoadSceneIE(lastScene));
    }

    private IEnumerator LoadSceneIE(int index)
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public static void StartFoodCounting()
    {
        foodCounter = new int[8];
        maxFoodValues = new int[8];
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
        foodItems.Add(new FoodItem("Golden Berries", foodCounter[7]));
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

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(lastScene);
    }
}

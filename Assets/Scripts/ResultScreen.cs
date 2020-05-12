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
        Invoke("SetFatness", 2f);
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

    public void SetFatness()
    {
        switch (GameController.stars)
        {
            case 0:
                targetValue = PlayerScript.AmountOfFoodEaten / GameController.weight1 * 0.33f;
                break;
            case 1:
                // can't be over 0.66f
                targetValue = 0.33f;
                break;
            case 2:
                // can't be over 1
                targetValue = 0.66f;
                break;
            case 3:
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
        // TODO: lataa edellinen scene
    }
}

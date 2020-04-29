using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultScreen : MonoBehaviour
{
    public static int[] maxFoodValues;
    public static int[] foodCounter;
    public static int totalCount;
    public static int maxTotalCount;

    public TMP_Text[] textFields;
    public TMP_Text[] valueFields;
    public TMP_Text totalValue;
    public GameObject[] stars;

    List<FoodItem> foodItems = new List<FoodItem>();

    public static void StartFoodCounting()
    {
        foodCounter = new int[8];
        maxFoodValues = new int[8];
    }

    private void OnEnable()
    {
        SetResults();
        DisplayResults();
        DisplayStars();
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

    void DisplayResults()
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

    void DisplayStars()
    {
        // Display as many stars as gamecontroller tells to:
        for (int i = 0; i < GameController.stars; i++)
        {
            stars[i].gameObject.SetActive(true);
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
    }
}

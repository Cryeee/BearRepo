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
    public TMP_Text total;
    private const string SPACE = ":    ";

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

        //if (maxFoodValues[0] > 0)
        //{
        //    foodItems.Add(new FoodItem("Cranberries", foodCounter[0]));
        //}
        //if (maxFoodValues[1] > 0)
        //{
        //    foodItems.Add(new FoodItem("Rabbits", foodCounter[1]));
        //}
        //if (maxFoodValues[2] > 0)
        //{
        //    foodItems.Add(new FoodItem("Mushrooms", foodCounter[2]));
        //}
        //if (maxFoodValues[3] > 0)
        //{
        //    foodItems.Add(new FoodItem("Glowing mushrooms", foodCounter[3]));
        //}
        //if (maxFoodValues[4] > 0)
        //{
        //    foodItems.Add(new FoodItem("Blueberries", foodCounter[4]));
        //}
        //if (maxFoodValues[5] > 0)
        //{
        //    foodItems.Add(new FoodItem("Fish", foodCounter[5]));
        //}
        //if (maxFoodValues[6] > 0)
        //{
        //    foodItems.Add(new FoodItem("Birds", foodCounter[6]));
        //}
        //if (maxFoodValues[7] > 0)
        //{
        //    foodItems.Add(new FoodItem("Golden Berries", foodCounter[7]));
        //}

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
                textFields[index].text = foodItems[i].foodName + SPACE + foodItems[i].amount
               + "/" + maxFoodValues[i];

                // add foods to total amount:
                totalCount += foodItems[i].amount;

                index++;
            }
            else
            {
                Debug.Log(maxFoodValues[i] + "  food not found on level");
            }
        }

        total.text = "Total" + SPACE + totalCount.ToString() + "/" + maxTotalCount.ToString();
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

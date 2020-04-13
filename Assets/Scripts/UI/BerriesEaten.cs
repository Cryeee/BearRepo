using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerriesEaten : MonoBehaviour
{
    public GameObject player;
    public Animator canvasAnimator;
    public bool canLevelUp;

    TMP_Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        uiText = gameObject.GetComponent<TextMeshProUGUI>();
        canLevelUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.gameOn)
        {
            //uiText.text = "Food Eaten: " + player.GetComponent<PlayerScript>().AmountOfFoodEaten + " / " + GameController.targetFoodAmount;
            uiText.text = 50 + player.GetComponent<PlayerScript>().AmountOfFoodEaten + " / " + GameController.targetFoodAmount + "kg";
        }

        // UI Bounce
        if(player.GetComponent<PlayerScript>().AmountOfFoodEaten >= 11 && player.GetComponent<PlayerScript>().AmountOfFoodEaten <= 20 && canLevelUp || player.GetComponent<PlayerScript>().AmountOfFoodEaten >= 50 && canLevelUp)
        {
            canvasAnimator.SetTrigger("LevelUp");
            canLevelUp = false;
        }
        if(player.GetComponent<PlayerScript>().AmountOfFoodEaten > 20 && player.GetComponent<PlayerScript>().AmountOfFoodEaten < 40)
        {
            canLevelUp = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum FoodType
{
    cranberry,
    rabbit,
    mushroom,
    glowingMushroom,
    blueberry,
    fish,
    bird,
    goldenBerry
}

public class PickUp : MonoBehaviour
{
	[Tooltip("1 means 1/10 of max fatness")]
	public float growAmount;

    public Sprite uiIcon;

    [Tooltip("For keeping track of picked up foods:")]
    public FoodType type;

    public ParticleSystem foodParticles;

    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Player") 
        {

            if(collision.gameObject.name == "Collider_spine" ||collision.gameObject.name == "pallokarhu")
            {
                if (type != FoodType.goldenBerry)
                {
                    // Tell ResultScreen that player picked a certain type of food
                    ResultScreen.foodCounter[(int)type] += 1;
                    //Debug.Log(ResultScreen.foodCounter[(int)type].ToString() + "  was picked up");
                    Debug.Log(collision.gameObject.name);
                }

                if (uiIcon == null)
                {
                    Debug.LogError("uiIcon not assigned!!");
                }

                //playerscript is in parent gameobject
                //collision.gameObject.GetComponent<PlayerScript>().AmountOfFoodEaten += growAmount;

                //Tell player to grow this amount:
                collision.GetComponentInParent<PlayerScript>().Grow(growAmount, uiIcon);
                //print(collision.gameObject.name + " collided with: " + gameObject.name);

                if (foodParticles != null)
                {
                    //particle effect
                    foodParticles.Play();
                }

                //sound effect for picking up
                if (type == FoodType.cranberry || type == FoodType.blueberry)
                {
                    FindObjectOfType<AudioManager>().Play("Berry");
                }
                if (type == FoodType.goldenBerry)
                {
                    FindObjectOfType<AudioManager>().Play("Cloudberry");
                }
                if (type == FoodType.fish)
                {
                    FindObjectOfType<AudioManager>().Play("Fish");
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("Nom");
                }

                //Debug.Log(ResultScreen.foodCounter[(int)type].ToString() + "  is the food count");
                Destroy(gameObject);
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float growAmount;
    public Sprite uiIcon;

    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Player") {

            if(uiIcon == null)
            {
                Debug.LogError("uiIcon not assigned!!");
            }

            //playerscript is in parent gameobject
            //collision.gameObject.GetComponent<PlayerScript>().AmountOfFoodEaten += growAmount;

            //Tell player to grow this amount:
            collision.GetComponent<PlayerScript>().Grow(growAmount, uiIcon);
            print(collision.gameObject.name + " collided with: " + gameObject.name);

            //sound effect for picking up
            FindObjectOfType<AudioManager>().Play("Nom");

            Destroy(gameObject);
        }
    }
}

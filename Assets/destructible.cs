﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public GameObject player;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player" && PlayerScript.inBallMode && player.GetComponent<Rigidbody>().velocity.y < -2)
           if (other.gameObject.tag == "Player" && PlayerScript.inBallMode && player.GetComponent<PlayerScript>().AmountOfFoodEaten >= 50)
            {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}

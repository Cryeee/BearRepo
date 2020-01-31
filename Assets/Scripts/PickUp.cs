﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Player") {
            
            //playerscript is in parent gameobject
            collision.gameObject.GetComponentInParent<PlayerScript>().amountOfBerriesEaten++;
            print("pickup collision");
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.AmountOfFoodEaten >= 50)
        {
            gameObject.GetComponent<Rigidbody>().mass = 2;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().mass = 20;
        }
    }
}

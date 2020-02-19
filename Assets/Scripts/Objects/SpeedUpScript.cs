using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedUpScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RollingMovement.turboOn = true;
            // punanen nuoli osottaa menosuuntaan:
            other.GetComponent<RollingMovement>().turboDirection = transform.right;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RollingMovement.turboOn = false;
        }
    }
}

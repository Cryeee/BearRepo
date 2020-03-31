using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public GameObject player;

   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && PlayerScript.inBallMode && player.GetComponent<Rigidbody>().velocity.y < -10)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}

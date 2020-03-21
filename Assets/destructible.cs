using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour
{
    public GameObject destroyedVersion;

   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

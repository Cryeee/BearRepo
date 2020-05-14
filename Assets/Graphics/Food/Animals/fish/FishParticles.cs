using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishParticles : MonoBehaviour
{
    public bool inWater;
    public ParticleSystem splashParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
            inWater = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
            inWater = false;
        }
    }
}

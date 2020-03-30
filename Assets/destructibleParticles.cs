using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleParticles : MonoBehaviour
{
    public ParticleSystem particles;

   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && PlayerScript.inBallMode)
        {
            particles.Play();
            Destroy(gameObject);
        }
    }
}

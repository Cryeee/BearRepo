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
            FindObjectOfType<AudioManager>().Play("Tree");
            particles.Play();
            Destroy(gameObject);
        }
    }
}

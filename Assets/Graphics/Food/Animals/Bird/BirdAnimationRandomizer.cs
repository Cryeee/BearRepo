using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimationRandomizer : MonoBehaviour
{
    public int randomNumber;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = Random.Range(1, 500);

        if (randomNumber < 3)
        {
            anim.SetTrigger("fly" + randomNumber);
        }

    }
}

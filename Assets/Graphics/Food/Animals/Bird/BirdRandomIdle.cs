using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRandomIdle : MonoBehaviour
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle8"))
        {
            randomNumber = Random.Range(1, 9);
            anim.Play("Idle" + randomNumber);

        }

    }
}

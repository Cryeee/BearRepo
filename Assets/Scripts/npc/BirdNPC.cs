using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdNPC : MonoBehaviour
{
    public GameObject fatBear;
    public GameObject skinnyBear;
    private GameObject player;
    public float fleeDistance = 4.0f;
    public float fleeTime = 1.0f;
    public float speed = 1f;
    bool spooked = false;
    private Animator animator;
    private Vector3 birdPosition;

    [Range(-0.05f, 0.05f)]
    public float x = 0.03f;
    [Range(-0.05f, 0.05f)]
    public float z = 0.0f;


    void Fly()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < fleeDistance && !spooked)
        {
            Invoke("goAway", fleeTime);
        }

        if (spooked == true)
        {
            // StartCoroutine(Wait());
            animator.SetBool("Fly", true);

            transform.position += new Vector3(x, 0.1f * speed, z);
        }
        else
        {
            animator.SetBool("Fly", false);
        }
    }

   /* IEnumerator Wait()
    {
        yield return new WaitForSeconds(fleeTime);
    }
    */
    void goAway()
    {
        spooked = true;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        birdPosition = transform.position;
    }

    void Update()
    {
        if (PlayerScript.inBallMode == false)
        {
            player = skinnyBear;
        }
        if (PlayerScript.inBallMode == true)
        {
            player = fatBear;
        }

        Fly();

        float comeBackDistance = Vector3.Distance(birdPosition, player.transform.position);

        if (spooked == true && comeBackDistance > 40)
        {
            spooked = false;
            transform.position = birdPosition;
            animator.SetBool("Fly", false);
        }
    }
}
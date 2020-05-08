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

            transform.position += new Vector3(0.03f, 0.1f * speed, 0f);
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

        if (transform.position.y > 25f)
        {
            Destroy(gameObject);
        }
    }
}
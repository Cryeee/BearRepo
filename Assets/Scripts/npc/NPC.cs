using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent navagent;
    public GameObject skinnyBear;
    public GameObject fatBear;
    private GameObject player;
    public float fleeDistance = 4.0f;
    private float speed;
    private Animator animator;
    public GameObject rabbitAnims;

    public Vector3[] patrolPoints;
    private int patrolPoint = 0;


    void Patrol()
    {
        GetComponent<NavMeshAgent>().speed = speed;
        //navagent.Resume();
        if (patrolPoints.Length > 0)
        {
            navagent.SetDestination(patrolPoints[patrolPoint]);
            if (transform.position.x == patrolPoints[patrolPoint].x && transform.position.z == patrolPoints[patrolPoint].z)
            {
                patrolPoint++;
                //Debug.Log("patrolpointtivaihtu");
            }
            if (patrolPoint >= patrolPoints.Length)
            {
                patrolPoint = 0;
            }
        }
    }

    void Awake()
    {
        animator = rabbitAnims.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        navagent = GetComponent<NavMeshAgent>();
        speed = GetComponent<NavMeshAgent>().speed;
        //animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.inBallMode == false) {
            player = skinnyBear;
        }
        if (PlayerScript.inBallMode == true)
        {
            player = fatBear;
        }


        float distance = Vector3.Distance(transform.position, player.transform.position);


        if (distance < fleeDistance)
        {
            GetComponent<NavMeshAgent>().speed = (speed * 3);
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPosition = transform.position + dirToPlayer;

            navagent.SetDestination(newPosition);
            patrolPoint = Random.Range(0, patrolPoints.Length);

        }
        else
        {
            Patrol();
        }
        if (navagent.velocity != Vector3.zero)
        {
            //Debug.Log("liikkuu");
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }
}

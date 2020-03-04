using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent navagent;
    public GameObject Player;
    public float fleeDistance = 4.0f;

    public Vector3[] patrolPoints;
    private int patrolPoint = 0;

    void Patrol()
    {
        //navagent.Resume();
        if(patrolPoints.Length > 0)
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


    // Start is called before the first frame update
    void Start()
    {
        navagent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Patrol();

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < fleeDistance)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPosition = transform.position + dirToPlayer;

            navagent.SetDestination(newPosition);
            patrolPoint = Random.Range(0, patrolPoints.Length);

        }
        else
        {
            Patrol();
        }
    }
}

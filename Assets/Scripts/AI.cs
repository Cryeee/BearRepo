using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent navagent;
    public GameObject Player;
    public float fleeDistance = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        navagent = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < fleeDistance)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPosition = transform.position + dirToPlayer;

            navagent.SetDestination(newPosition);
        }
    }
}

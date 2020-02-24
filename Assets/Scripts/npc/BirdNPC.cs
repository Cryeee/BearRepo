using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdNPC : MonoBehaviour
{
    public GameObject Player;
    public float fleeDistance = 4.0f;
    bool spooked = false;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < fleeDistance)
        {
            spooked = true;                   
        }

        if (spooked == true)
        {
            transform.position += new Vector3(0.03f, 0.1f, 0f);
        }

        if (transform.position.y > 15f)
        {
            Destroy(gameObject);
        }
    }
}

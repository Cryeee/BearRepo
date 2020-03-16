using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdNPC : MonoBehaviour
{
    public GameObject Player;
    public float fleeDistance = 4.0f;
    public float fleeTime = 1.0f;
    public float speed = 1f;
    bool spooked = false;
    

    void Fly()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < fleeDistance && !spooked)
        {
            Debug.Log("kumoituskaatamus");
            Invoke("goAway", 2f);
        }

        if (spooked == true)
        {
            StartCoroutine(Wait());
           

            transform.position += new Vector3(0.03f, 0.1f * speed, 0f);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(fleeTime);
    }
    
    void goAway()
    {
        spooked = true;
    }

    void Update()
    {
        Fly();

        if (transform.position.y > 25f)
        {
            Destroy(gameObject);
        }
    }
}

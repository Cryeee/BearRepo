using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingMovement : MonoBehaviour
{
    public int ballSpeed = 600;

    private Rigidbody RB;

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            RB.AddForce(camera.transform.forward * ballSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            RB.AddForce(-camera.transform.forward * ballSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            RB.AddForce(-camera.transform.right * ballSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RB.AddForce(camera.transform.right * ballSpeed);
            
        }
        
    }
}

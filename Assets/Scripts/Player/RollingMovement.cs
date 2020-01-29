using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingMovement : MonoBehaviour
{
    //rolling speed
    public int ballSpeed;

    //variable used to show velocity in Inspector
    public Vector3 velocity;

    //Vector for movement
    public Vector3 movementVector;

    private Rigidbody RB;

    public GameObject cameraObj;

    void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        //no input = no addforce 
        movementVector = Vector3.zero;

        //movement inputs
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.z = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x = 1;   
        }

        //limit maximum speed/velocity
        if(RB.velocity.x < -5) {
            RB.velocity = new Vector3(-5, RB.velocity.y, RB.velocity.z);
        }
        if(RB.velocity.x > 5) {
            RB.velocity = new Vector3(5, RB.velocity.y, RB.velocity.z);
        }
        if(RB.velocity.z < -5) {
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -5);
        }
        if(RB.velocity.z > 5) {
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, 5);
        }

        //show velocity in Inspector
        velocity = RB.velocity;
        
    }
    void FixedUpdate() 
    {
        
        RB.AddForce(movementVector.normalized * ballSpeed);

        

    }
}
/*
//move in cameras Axis
if (Input.GetKey(KeyCode.W))
        {
            RB.AddForce(cameraObj.transform.forward * ballSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            RB.AddForce(-cameraObj.transform.forward * ballSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            RB.AddForce(-cameraObj.transform.right * ballSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RB.AddForce(cameraObj.transform.right * ballSpeed);
            
        }
*/

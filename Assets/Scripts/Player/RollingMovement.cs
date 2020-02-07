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

    private Vector3 directionVector3;

    private float Xinput;
    private float Yinput;

    private Rigidbody RB;

    public GameObject cameraObj;

    private PlayerInputs playerInputs;

    public Vector3 faceDir;

    public float childY;

    public GameObject Parent;


    void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponentInChildren<Rigidbody>();
        playerInputs = GetComponent<PlayerInputs>();

    }

    void Update()
    {
        Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;
        //movementVector.Set(playerInputs.MoveInput.x, 0, playerInputs.MoveInput.y);

        movementVector = (cameraObj.transform.right * Xinput + cameraObj.transform.forward * Yinput);
        movementVector.y = 0;
        
        

        childY = GetComponentInChildren<Transform>().position.y;
        if(Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(new Vector3(0, 1, 0), Space.World);
            //Parent.transform.Rotate(new Vector3(0, 1, 0), Space.World);
            //Parent.transform.rotation = Quaternion.Euler(0.0f, (Parent.transform.rotation.y + 1) * 5, 0);
            //Parent.transform.rotation.SetFromToRotation(Parent.transform.position, transform.forward);

            Parent.transform.rotation = Quaternion.LookRotation(transform.position - Parent.transform.position, Vector3.up);


        }

        //Parent.transform.rotation = gameObject.transform.rotation;

        faceDir = transform.rotation.eulerAngles;
        //limit maximum speed/velocity
        /*
        if (RB.velocity.x < -5) {
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
        }*/

        //show velocity in Inspector
        velocity = RB.velocity;
        
    }
    void FixedUpdate() 
    {
        
        //RB.AddForce(movementVector.normalized * ballSpeed);

        RB.AddForce(movementVector.normalized * ballSpeed);
        //RB.velocity = movementVector.normalized * ballSpeed;



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

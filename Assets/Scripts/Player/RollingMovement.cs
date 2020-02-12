using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollingMovement : MonoBehaviour
{
    //rolling speed
    public int ballSpeed;

    public bool canJump = true;

    public float jumpForce;
    //variable used to show velocity in Inspector
    public Vector3 velocity;

    public float velocityLimit;

    //Vector for movement
    public Vector3 movementVector;

    private Vector3 directionVector3;

    private float Xinput;
    private float Yinput;

    private Rigidbody RB;

    public GameObject cameraObj;

    private InputHandler playerInputs;

    public Vector3 faceDir;

    private InputManager inputManager;

    // value of WASD/Left Stick
    public Vector2 moveInput;

    public Vector2 cameraInput;


    


    void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponent<InputHandler>();
    }

    void Update()
    {
        Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;
        //movementVector.Set(playerInputs.MoveInput.x, 0, playerInputs.MoveInput.y);

        movementVector = (cameraObj.transform.right * Xinput + cameraObj.transform.forward * Yinput);
        movementVector.y = 0;
        

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

        // limit max velocity
        if(RB.velocity.x > velocityLimit) {
            RB.velocity = new Vector3(velocityLimit, RB.velocity.y, RB.velocity.z);
        }
        if(RB.velocity.x < -velocityLimit) {
            RB.velocity = new Vector3(-velocityLimit, RB.velocity.y, RB.velocity.z);
        }

        if(RB.velocity.z > velocityLimit) {
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, velocityLimit);
        }
        if(RB.velocity.z < -velocityLimit) {
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -velocityLimit);
        }
    }
    
    void FixedUpdate() 
    {
        RB.AddForce(movementVector.normalized * ballSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}


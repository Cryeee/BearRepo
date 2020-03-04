using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollingMovement : MonoBehaviour
{
    float distToGround;

    //rolling speed
    public int ballSpeed;

    public bool canJump = true;
    public bool jumped = false;

    //bool to invoke cjeckjumping invoke only once
    bool invokeOnlyOnce = true;


    public float jumpForce;
    //variable used to show velocity in Inspector
    public Vector3 velocity;

    public float velocityLimit;

    //Vector for movement
    public Vector3 movementVector;

    private Vector3 directionVector3;

    private float Xinput;
    private float Yinput;

    public Rigidbody RB;

    public GameObject cameraObj;

    private InputHandler playerInputs;

    public Vector3 faceDir;

    private InputManager inputManager;

    // value of WASD/Left Stick
    public Vector2 moveInput;

    public Vector2 cameraInput;

    public static bool turboOn;
    public Vector3 turboDirection;
    public float turboSpeed = 150;



    void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponent<InputHandler>();

        
    }
    public void CheckJumping()
    {
        if (!IsGrounded() && !jumped)
        {
            if (invokeOnlyOnce)
            {
                invokeOnlyOnce = false;
                Invoke("LedgeDelay", 0.5f);

            }
        }
        else
        {
            canJump = IsGrounded();
            invokeOnlyOnce = true;

        }
    }
    private void LedgeDelay()
    {
        print("asd omnta kertaa tää tulee");
        canJump = false;
        invokeOnlyOnce = true;
    }

    void Update()
    {
        canJump = IsGrounded();

        //for groundcheck
        distToGround = GetComponent<Collider>().bounds.extents.y;
        //CheckJumping();
        //canJump = IsGrounded();

        print("isgrounded:   " + IsGrounded());
        //print("distToGround:   " + distToGround);
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

        if (!turboOn)
        {
            // limit max velocity
            if (RB.velocity.x > velocityLimit)
            {
                RB.velocity = new Vector3(velocityLimit, RB.velocity.y, RB.velocity.z);
            }
            if (RB.velocity.x < -velocityLimit)
            {
                RB.velocity = new Vector3(-velocityLimit, RB.velocity.y, RB.velocity.z);
            }

            if (RB.velocity.z > velocityLimit)
            {
                RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, velocityLimit);
            }
            if (RB.velocity.z < -velocityLimit)
            {
                RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -velocityLimit);
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void OnDrawGizmos()
    {
        
    }

    void FixedUpdate() 
    {

        RB.AddForce(movementVector.normalized * ballSpeed);

        if (turboOn)
        {
            RB.AddForce(turboDirection * turboSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            
            jumped = false; // kivien pällä hyppiminen kusee tällä !!!
        }
    }

    public void Jump()
    {

        if (canJump)
        {
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z); // fixes megajumps
            RB.AddForce(0, jumpForce, 0);
            canJump = false;
            jumped = true;
        }
    }
}


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

    public Vector3 accelerationVector;

    private float acceleration;

    public ParticleSystem puffParticles;

    private bool playedParticles = false;

    public Animator ballAnim;

    public static bool pressedJumpButton;

    public ParticleSystem splashParticles;

    void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponentInParent<InputHandler>();
        gameObject.SetActive(false);
    }

    public void CheckJumping()
    {
        if (!IsGrounded() && !jumped)
        {
            if (invokeOnlyOnce)
            {
                //canJump = true;
                invokeOnlyOnce = false;
                Invoke("LedgeDelay", 0.3f); // TODO ei aika base

            }
        }
        else if (IsGrounded() && jumped)
        {
            jumped = false;
        }
        else
        {
            canJump = IsGrounded();
            invokeOnlyOnce = true;
            //jumped = false;

        }
    }
    private void LedgeDelay()
    {
        print("asd omnta kertaa tää tulee");
        canJump = false;
        invokeOnlyOnce = true;
        pressedJumpButton = false;
        //jumped = false;
    }

    void Update()
    {
        //particle when second fattness level
        if (PlayerScript.AmountOfFoodEaten >= 50 && !playedParticles)
        {
            puffParticles.Play();
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z); // fixes megajumps
            RB.AddForce(0, jumpForce/2, 0);
            ballAnim.SetTrigger("XL");
            playedParticles = true;

        }

        //canJump = IsGrounded();
        //print("magnitude * 0.3:      -->" + 0.3f * RB.velocity.magnitude);

        //for groundcheck
        distToGround = GetComponent<Collider>().bounds.extents.y;
        CheckJumping();
        //canJump = IsGrounded();

        //print("isgrounded:   " + IsGrounded());
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

        

        /*
        if(movementVector != Vector3.zero)
        {
            accelerationVector += movementVector * 0.1f * Time.deltaTime;
        } else if (movementVector == Vector3.zero)
        {
            accelerationVector = accelerationVector / 2;
        }
        */

        

        //print("acceleration vector:    " + accelerationVector);
        //accelerationVector = accelerationVector / 2;

    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        //return Physics.CapsuleCast(transform.position, new Vector3(transform.position.x -distToGround - 0.1f, transform.position.x, transform.position.z), 1, -Vector3.up, distToGround + 0.1f);
        
    }

    private void OnDrawGizmos()
    {
        
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(gameObject.transform.position, -Vector3.up);
        
    }

    void FixedUpdate() 
    {

        //RB.AddForce(accelerationVector.normalized * ballSpeed);
        //RB.velocity = accelerationVector * ballSpeed;
        //RB.velocity = accelerationVector;
        
        RB.AddForce(movementVector.normalized * ballSpeed);
        



        //RB.AddForce(movementVector.normalized * ballSpeed);

        //RB.AddForce(new Vector3(movementVector.x + 0.01f * movementVector.x, movementVector.y, movementVector.z + 0.01f * movementVector.z) * ballSpeed);

        //RB.velocity = new Vector3(RB.velocity.x * 10 * movementVector.x, 0, RB.velocity.z * 10 * movementVector.z);

        if (turboOn)
        {
            RB.AddForce(turboDirection * turboSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            
            //jumped = false; // kivien pällä hyppiminen kusee tällä !!!
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
            pressedJumpButton = true;
        }
    }

    public void Fatten()
    {
        ballAnim.SetTrigger("Chomp");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
        }
    }


}


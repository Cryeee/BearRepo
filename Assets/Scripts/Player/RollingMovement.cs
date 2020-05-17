using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollingMovement : MonoBehaviour
{
    public GameObject cameraObj;
    public ParticleSystem puffParticles;
    public Animator ballAnim;
    public ParticleSystem splashParticles;

    //rolling speed 
    public int ballSpeed;

    public float jumpForce;
    public bool canJump = true;
    public bool jumped = false;

    //bool to invoke cjeckjumping invoke only once 
    private bool invokeOnlyOnce = true;
    private float distToGround;

    //variable used to show velocity in Inspector 
    public Vector3 velocity;
    public float velocityLimit;

    //Vector for movement 
    public Vector3 movementVector;
    private float Xinput;
    private float Yinput;

    // value of WASD/Left Stick 
    public Vector2 moveInput;
    public Vector2 cameraInput;

    private Rigidbody RB;
    private InputHandler playerInputs;

    public static bool turboOn;
    public Vector3 turboDirection;
    public float turboSpeed = 150;

    private bool playedParticles = false;
    public static bool pressedJumpButton;

    public bool inWater;
    public ParticleSystem waterparticles;

    //UI animation
    public Animator canvasAnimator;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
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
        //print("asd omnta kertaa tää tulee"); 
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
            RB.AddForce(0, jumpForce / 2, 0);
            ballAnim.SetTrigger("XL");
            playedParticles = true;

        }
        //for groundcheck 
        distToGround = GetComponent<Collider>().bounds.extents.y;
        CheckJumping();

        Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;

        movementVector = (cameraObj.transform.right * Xinput + cameraObj.transform.forward * Yinput);
        movementVector.y = 0;

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
        //water particles
        if (inWater && (Mathf.Abs(RB.velocity.x) > 2 || Mathf.Abs(RB.velocity.z) > 2))
        {
            waterparticles.Play();
        }
        else
        {
            waterparticles.Stop();
        }

    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


    void FixedUpdate()
    {
        //TODO : MAKE BALL LESS HEAVY
        //RB.AddForce(movementVector.normalized * ballSpeed, ForceMode.Force);

        RB.AddForce(movementVector.normalized * ballSpeed);

        if (turboOn)
        {
            RB.AddForce(turboDirection * turboSpeed, ForceMode.Force);
        }
    }

    public void Jump()
    {

        if (canJump && MenuController.paused == false)
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
        canvasAnimator.SetTrigger("Ate");

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
            inWater = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
            inWater = false;
        }
    }
}



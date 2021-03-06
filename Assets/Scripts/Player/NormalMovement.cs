﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 4;

    [SerializeField]
    private float rotationSpeed = 3;

	[SerializeField]
	private float runSpeed = 6;

	[SerializeField]
	private float jumpForce = 13;

	[SerializeField]
	private Collider groundCheckCollider = null;

    //Vector for movement
    public Vector3 movementVector;
    public Vector3 rotationVector;
	Quaternion faceRotation;

	// value of WASD/Left Stick
	public Vector2 moveInput;
    public Vector2 cameraInput;
    private float Xinput;
    private float Yinput;
	private float speed;
	private float fatnessAmount = 0;

	private InputHandler playerInputs;
	private GameObject cameraObj;
	private Rigidbody rb;
	private Animator animator;

    //water particle systems
    public ParticleSystem splashParticles;
    public ParticleSystem waterparticles;
    public bool inWater;

    public Animator canvasAnimator;

	#region Jump Stuff

	bool doCheck;

    public bool IsGrounded()
	{
		//Debug.DrawRay(groundCheckCollider.bounds.center,
		//	Vector3.down * (groundCheckCollider.bounds.extents.y + 0.1f));

		//return Physics.Raycast(groundCheckCollider.bounds.center, Vector3.down,
		//	groundCheckCollider.bounds.extents.y + 0.1f);

		if(Physics.Raycast(groundCheckCollider.bounds.center, Vector3.down,
		 	groundCheckCollider.bounds.extents.y + 0.1f) == true
			|| Physics.Raycast(groundCheckCollider.bounds.max, Vector3.down,
		 	groundCheckCollider.bounds.extents.y + 0.1f) == true
			|| Physics.Raycast(groundCheckCollider.bounds.min, Vector3.down,
		 	groundCheckCollider.bounds.extents.y + 0.1f) == true)
		{
			return true;
		} else
		{
			return false;
		}
	}

	public void Jump()
	{
		if(IsGrounded())
		{
			rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
			//rb.AddForce(new Vector3(rb.velocity.x, jumpForce, rb.velocity.z));
			animator.SetBool("LandBool", false);
			animator.SetTrigger("Jump");
			Invoke("StartCheckingLand", 0.1f);
            //StartCoroutine(LandCheck());

            if (!inWater)
            {
                FindObjectOfType<AudioManager>().Play("Jump");
            }
            if (inWater)
            {
                //FindObjectOfType<AudioManager>().Play("WaterJump");
            }
        }
	}

	private void StartCheckingLand()
	{
		doCheck = true;
	}

	//IEnumerator LandCheck()
	//{
	//	yield return new WaitForSeconds(0.01f);
	//	while (true)
	//	{
	//		if (IsGrounded())
	//		{
	//			Debug.Log("ländäs");
	//			animator.SetTrigger("Land");
	//			break;
	//		}
	//		yield return new WaitForEndOfFrame();
	//	}
	//}

	#endregion


	void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponentInParent<InputHandler>();
		animator = GetComponent<Animator>();
		cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
		speed = walkSpeed;
    }

    void Update()
    {
		Debug.DrawRay(groundCheckCollider.bounds.max,
			Vector3.down * (groundCheckCollider.bounds.extents.y + 0.1f));

		Debug.DrawRay(groundCheckCollider.bounds.min,
			Vector3.down * (groundCheckCollider.bounds.extents.y + 0.1f));

		Debug.DrawRay(groundCheckCollider.bounds.center,
			Vector3.down * (groundCheckCollider.bounds.extents.y + 0.1f));


		Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;

		//for getting a movement vector without y force:

		Vector3 forward = cameraObj.transform.forward;
		Vector3 right = cameraObj.transform.right;
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();

		movementVector = right * Xinput + forward * Yinput;

		SetAnimations();

        //water particles
        if (inWater)
        {
            waterparticles.Play();
        }
        else
        {
            waterparticles.Stop();
        }

		if(doCheck)
		{
			if (IsGrounded())
			{
				Debug.Log("ländäs");
				animator.SetBool("LandBool", true);
				doCheck = false;
			}
		}
	}

    private void FixedUpdate()
    {
		// Get the velocity
		Vector3 horizontalMove = rb.velocity;
		// Don't use the vertical velocity
		horizontalMove.y = 0;
		// Calculate the approximate distance that will be traversed
		float distance = horizontalMove.magnitude * Time.fixedDeltaTime;
		// Normalize horizontalMove since it should be used to indicate direction
		horizontalMove.Normalize();

		RaycastHit hit;

		// Check if the body's current velocity will result in a collision
		if (!IsGrounded() && rb.SweepTest(horizontalMove, out hit, distance, QueryTriggerInteraction.Ignore))
		{
			// If so, stop the movement
			rb.velocity = new Vector3(0, -10, 0);
		} 
		else
		{
			MovementVelocity();
			Rotation();
		}
    }

    private void MovementVelocity()
    {
		rb.velocity = movementVector.normalized * speed + new Vector3(0.0f, rb.velocity.y, 0.0f);
	}

    private void Rotation()
    {
		if (movementVector != Vector3.zero)
		{
			faceRotation = Quaternion.LookRotation(movementVector);
		}

		rb.rotation = Quaternion.Slerp(rb.rotation, faceRotation, rotationSpeed * Time.deltaTime);
	}

	private void SetAnimations()
	{
		if (movementVector != Vector3.zero)
		{
			// Jos tatti pohjassa, juoksuanimaatio
			if(Mathf.Approximately(movementVector.sqrMagnitude, 1f))
			{
				animator.SetBool("Run", true);
				animator.SetBool("Walk", false);
				speed = runSpeed;
			}
			else
			{
				animator.SetBool("Walk", true);
				animator.SetBool("Run", false);
				speed = walkSpeed;
			}
		} else
		{
			animator.SetBool("Run", false);
			animator.SetBool("Walk", false);
			speed = walkSpeed;
		}

		float direction = Vector3.SignedAngle(movementVector, transform.forward, Vector3.up);

		// Pään kääntyminen:
		if (direction > 5)
		{
			animator.SetFloat("Direction", 0f, 0.2f, Time.deltaTime);
		} else if (direction > -5 && direction < 5)
		{
			animator.SetFloat("Direction", 0.5f, 0.2f, Time.deltaTime);
		} else if (direction < -5)
		{
			animator.SetFloat("Direction", 1f, 0.2f, Time.deltaTime);
		}
	}

	public void Fatten(float amount)
	{
		animator.SetTrigger("Chomp");
        canvasAnimator.SetTrigger("Ate");
        fatnessAmount += amount;
		if (fatnessAmount < 1)
		{
			animator.SetFloat("Fatness", fatnessAmount);
		} else if (fatnessAmount >= 1)
		{
			animator.SetTrigger("Ball");
			Jump();
		}
		
	}

	public void ChangeToBall()
	{
		GetComponentInParent<PlayerScript>().TurnToBall(transform.position);
		gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Grow");
    }

    //water particles
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
            inWater = true;
			FindObjectOfType<AudioManager>().Play("WaterJump");
		}
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splashParticles.Play();
            inWater = false;
			FindObjectOfType<AudioManager>().Play("WaterJump");
		}
    }
}

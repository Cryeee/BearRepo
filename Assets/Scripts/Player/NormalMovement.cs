using System.Collections;
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

	private GameController gameController;
	private InputHandler playerInputs;
	private GameObject cameraObj;
	private Rigidbody RB;
	private Animator animator;

	#region Jump Stuff
	
	public bool IsGrounded()
	{
		Debug.DrawRay(groundCheckCollider.bounds.center,
			Vector3.down * (groundCheckCollider.bounds.extents.y + 0.1f));

		return Physics.Raycast(groundCheckCollider.bounds.center, Vector3.down,
			groundCheckCollider.bounds.extents.y + 0.1f);

	}

	public void Jump()
	{
		if(IsGrounded())
		{
			RB.velocity = new Vector3(RB.velocity.x, jumpForce, RB.velocity.z);
			animator.SetTrigger("Jump");
		}
	}
	#endregion


	void Start()
    {
        RB = GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponentInParent<InputHandler>();
		animator = GetComponent<Animator>();
		cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		speed = walkSpeed;
    }

    void Update()
    {
		Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;

		// for getting a movement vector without y force:
		Vector3 forward = cameraObj.transform.forward;
		Vector3 right = cameraObj.transform.right;
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();

		movementVector = right * Xinput + forward * Yinput;
		SetAnimations();
	}

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
			RB.velocity = movementVector.normalized * speed + new Vector3(0.0f, RB.velocity.y, 0.0f);
	}

    private void Rotation()
    {
		#region failure
		////TÖK TÖK ROTAATIO:
		//if (movementVector != Vector3.zero)
		//{
		//	targetRotation = Quaternion.LookRotation(, transform.InverseTransformDirection(transform.up));
		//}

		//if (movementVector != Vector3.zero)
		//{
		//	targetRotation = Quaternion.LookRotation(RB.velocity);
		//}

		// LUISUVA ROTAATIO:
		//Vector3 rotation = new Vector3(0, Xinput * rotationSpeed, 0);
		//RB.AddRelativeTorque(rotation);

		//Quaternion deltaRotation = Quaternion.Euler(RB.velocity * rotationSpeed * Time.deltaTime);
		//RB.MoveRotation(RB.rotation * deltaRotation);
		//RB.Speed
		#endregion

		if (movementVector != Vector3.zero)
		{
			faceRotation = Quaternion.LookRotation(movementVector);
		}

		RB.rotation = Quaternion.Slerp(RB.rotation, faceRotation, rotationSpeed * Time.deltaTime);
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

		// Pään kääntyminen, kusee 180 asteella:
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
		fatnessAmount += amount;
		if (fatnessAmount < 1)
		{
			animator.SetFloat("Fatness", fatnessAmount);
		} else if (fatnessAmount > 1)
		{
			GetComponentInParent<PlayerScript>().TurnToBall(transform.position);
			gameObject.SetActive(false);
		}
		
	}
}

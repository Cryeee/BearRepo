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

	#region Components
	private GameObject cameraObj;
	private Rigidbody RB;
	private InputHandler playerInputs;
	private Animator animator;
	#endregion

	#region Jump Stuff
	public bool canJump = true;
	public bool jumped = false;
	float distToGround;

	//bool to invoke cjeckjumping invoke only once
	bool invokeOnlyOnce = true;
	public float jumpForce;

	public void CheckJumping()
	{
		if (!IsGrounded() && !jumped)
		{
			if (invokeOnlyOnce)
			{
				//canJump = true;
				invokeOnlyOnce = false;
				Invoke("LedgeDelay", 0.3f); // TODO ei aika based


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
			//jumped = false;
	}

	public bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.4f);
		//return Physics.CapsuleCast(transform.position, new Vector3(transform.position.x -distToGround - 0.1f, transform.position.x, transform.position.z), 1, -Vector3.up, distToGround + 0.1f);

	}

	public void Jump()
	{

		if (canJump)
		{
			//RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z); // fixes megajumps
			//RB.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			RB.velocity = new Vector3(RB.velocity.x, jumpForce, RB.velocity.z);
			animator.SetTrigger("Jump");
			canJump = false;
			jumped = true;
		}
	}
	#endregion


	// Start is called before the first frame update
	void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponent<InputHandler>();
		animator = GetComponent<Animator>();
		cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
		speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
		distToGround = GetComponent<Collider>().bounds.extents.y;
		CheckJumping();

		Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;

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
		//RB.velocity = movementVector.normalized * speed;
		RB.velocity = movementVector.normalized * speed + new Vector3(0.0f, RB.velocity.y, 0.0f);
		//RB.AddForce(movementVector.normalized * speed, ForceMode.Impulse);
	}

    private void Rotation()
    {
		////TÖK TÖK ROTAATIO:
		//if (movementVector != Vector3.zero)
		//{
		//	targetRotation = Quaternion.LookRotation(, transform.InverseTransformDirection(transform.up));
		//}
		if (movementVector != Vector3.zero)
		{
			faceRotation = Quaternion.LookRotation(movementVector);
		}

		//if (movementVector != Vector3.zero)
		//{
		//	targetRotation = Quaternion.LookRotation(RB.velocity);
		//}

		RB.rotation = Quaternion.Slerp(RB.rotation, faceRotation, rotationSpeed * Time.deltaTime);



		// LUISUVA ROTAATIO:
		//Vector3 rotation = new Vector3(0, Xinput * rotationSpeed, 0);
		//RB.AddRelativeTorque(rotation);

		//Quaternion deltaRotation = Quaternion.Euler(RB.velocity * rotationSpeed * Time.deltaTime);
		//RB.MoveRotation(RB.rotation * deltaRotation);
		//RB.Speed
	}

	private void SetAnimations()
	{
		if (movementVector != Vector3.zero)
		{
			animator.SetBool("Walk", true);
			Debug.Log(movementVector);
		} else
		{
			animator.SetBool("Walk", false);
		}
	}

	public void Run()
	{
		speed = runSpeed;
		rotationSpeed += 2;
		animator.SetBool("Run", true);
		animator.SetBool("Walk", false);
	}

	public void Walk()
	{
		speed = walkSpeed;
		rotationSpeed -= 2;
		animator.SetBool("Walk", true);
		animator.SetBool("Run", false);
	}

	public void Fatten(float amount)
	{
		fatnessAmount += amount;
		if (fatnessAmount < 1)
		{
			animator.SetFloat("Fatness", fatnessAmount);
		} else if (fatnessAmount > 1)
		{
			TurnToBall();
		}
		
	}

	private void TurnToBall()
	{
		GameObject ball = transform.Find("pallokarhu").gameObject;
		ball.SetActive(true);
		this.enabled = false;

	}
}

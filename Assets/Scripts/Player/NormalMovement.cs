using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : MonoBehaviour
{
    //public GameObject cameraObj;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    //Vector for movement
    public Vector3 movementVector;

    public Vector3 rotationVector;

    // value of WASD/Left Stick
    public Vector2 moveInput;
    public Vector2 cameraInput;
    private float Xinput;
    private float Yinput;

    private Rigidbody RB;
    private InputHandler playerInputs;
    private Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        //get RigidBody from childObject
        RB = this.GetComponent<Rigidbody>();
        playerInputs = gameObject.GetComponent<InputHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        Xinput = playerInputs.MoveInput.x;
        Yinput = playerInputs.MoveInput.y;

        movementVector = Camera.main.transform.right * Xinput + Camera.main.transform.forward * Yinput;
        movementVector.y = 0;
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        RB.velocity = movementVector.normalized * speed;
    }

    private void Rotation()
    {
        ////TÖK TÖK ROTAATIO:
        //if(movementVector != Vector3.zero)
        //{
        //    RB.MoveRotation(Quaternion.LookRotation(RB.velocity * rotationSpeed, transform.up));
        //}

        if (movementVector != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(RB.velocity);
        }

        RB.rotation = Quaternion.Slerp(RB.rotation, targetRotation, rotationSpeed * Time.deltaTime);



        // LUISUVA ROTAATIO:
        //Vector3 rotation = new Vector3(0, Xinput * rotationSpeed, 0);
        //RB.AddRelativeTorque(rotation);

        //Quaternion deltaRotation = Quaternion.Euler(RB.velocity * rotationSpeed * Time.deltaTime);
        //RB.MoveRotation(RB.rotation * deltaRotation);
        //RB.Speed
    }
}

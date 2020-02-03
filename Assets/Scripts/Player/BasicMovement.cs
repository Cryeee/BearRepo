using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private PlayerInputs playerInputs;
    private Rigidbody rb;
    private Vector3 moveDir;

    void Start()
    {
        // Access script that handles input
        playerInputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // make player move on z-axis instead of y:
        moveDir = new Vector3(playerInputs.MoveInput.x, 0, playerInputs.MoveInput.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.TransformDirection(moveDir)  * speed * Time.deltaTime);
    }
}

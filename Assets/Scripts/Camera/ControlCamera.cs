using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    public CinemachineFreeLook CmFreelookCamera;
    public float startCutsceneRotateSpeed = 0.3f;

    [Header("Time in seconds before rotating starts")]
    public float stayStillTime;
    public float acceleration;
    public float deacceleration;

    public bool rotateClockwise;

    private Transform player;
    private float percentage = 0;
    private float X;
    private float Y;
    private float Z;
    private float radius;
    private Vector3 center;
    private bool rotate;
    private bool slowDown;

    void Start()
    {
        // if debug setting for skipping start animation is not on, 
        // disable cinemachine
        if (!GameController.skipCutscene)
        {
            // Orbit player
            player = GameObject.FindGameObjectWithTag("Player").transform;

            // Override cinemachine controls
            CmFreelookCamera.GetComponent<CinemachineFreeLook>().enabled = false;

            // Get orbit point but ignore player's Y
            center = new Vector3(player.position.x, transform.position.y, player.position.z);

            // Calculate radius as float
            radius = Vector3.Distance(transform.position, center);

            Y = 0;
            X = radius;
            Z = radius;

            // put camera to starting position:
            CalculatePosition();

            // start rotating after time
            Invoke("StartRotating", stayStillTime);
        }   
    }

    void Update()
    {

        // Rotate camera around player before player can start playing:
        if (!GameController.gameOn && rotate)
        {
            CalculatePosition();
        }
    }

    private void CalculatePosition()
    {
        float angle = Mathf.Sin(percentage);

        // Stop rotating after 180 degrees:
        if (angle < 0 || acceleration < 0)
        {
            rotate = false;
            return;
        }

        Debug.Log(Mathf.Sin(percentage).ToString());
        percentage += Time.deltaTime * startCutsceneRotateSpeed * acceleration;

        // if rotated a quarter
        if (angle >= 0.99f || slowDown)
        {
            slowDown = true;
            acceleration -= (Time.deltaTime * deacceleration);
            deacceleration = Time.deltaTime * 0.1f;
        }
        else if (angle < 0.99f)
        {
            // if not rotated a quarter
            acceleration += Time.deltaTime * 0.1f;
        }

            float x = -Mathf.Cos(percentage) * X;
        float z;

        if (rotateClockwise)
        {
            z = Mathf.Sin(percentage) * Z;
        }
        else
        {
            z = -Mathf.Sin(percentage) * Z;
        }

        Vector3 pos = new Vector3(x, Y, z);
        //Vector3 newPos = pos + center;
        transform.position = pos + center;
        transform.LookAt(player);
    }

    private void StartRotating()
    {
        rotate = true;
    }

    // Enable CinemachineBrain when player can actually start playing:
    private void EnableCinemachine()
    {
        CmFreelookCamera.GetComponent<CinemachineFreeLook>().enabled = true;
    }

    // Subscribe & unsubscribe to OnGameStart event:
    private void OnEnable()
    {
        GameController.OnGameStart += EnableCinemachine;
    }

    private void OnDisable()
    {
        GameController.OnGameStart -= EnableCinemachine;
    }
}

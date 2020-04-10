using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    public CinemachineFreeLook CmFreelookCamera;
    public float startCutsceneRotateSpeed = 0.3f;
    public bool rotateClockwise;

    private Transform player;
    private float percentage = 0;
    private float X;
    private float Y;
    private float Z;
    private float radius;
    private Vector3 center;

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
        }   
    }

    void Update()
    {

        // Rotate camera around player before player can start playing:
        if (!GameController.gameOn)
        {
            // Stop rotating after 180 degrees:
            if (Mathf.Sin(percentage) < 0)
            {
                return;
            }

            percentage += Time.deltaTime * startCutsceneRotateSpeed;
            float x = -Mathf.Cos(percentage) * X;
            float z;

            if (rotateClockwise)
            {
                z = Mathf.Sin(percentage) * Z;
            } else
            {
                z = -Mathf.Sin(percentage) * Z;
            }
           
            Vector3 pos = new Vector3(x, Y, z);
            transform.position = pos + center;
            transform.LookAt(player);
        }
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

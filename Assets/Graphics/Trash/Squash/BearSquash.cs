﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSquash : MonoBehaviour
{
    public GameObject player;
    public GameObject playerparent;
    public GameObject BearTargerParentPos;
    public GameObject BearArmature;
    public Vector3 Offset;
    public bool canSquash;
    public bool canJumpSquish;

    public ParticleSystem rollingPuffParticles;
    public ParticleSystem landingParticles;
    public ParticleSystem splashParticles;

    public bool up;

    public float playerYVelocity;

    public SphereCollider sphere;

    public float foodEaten;

    // Start is called before the first frame update
    void Start()
    {
        canSquash = false;
        canJumpSquish = true;
        foodEaten = playerparent.GetComponent<PlayerScript>().AmountOfFoodEaten / 110;
    }

    // Update is called once per frame
    void Update()
    {
        //So that collider won't grow bigger than player model
        if(playerparent.GetComponent<PlayerScript>().sizeIncrease <= 1)
        {
            sphere.radius = 0.9f - foodEaten + (playerparent.GetComponent<PlayerScript>().AmountOfFoodEaten / 110);
        }
       

        //particle system
        if (!player.GetComponent<RollingMovement>().canJump)
        {
            rollingPuffParticles.Play();
        }

        if (player.GetComponent<Rigidbody>().velocity.y < 0)
        {
            up = false;
        }
        else
        {
            up = true;
        }

        GetComponent<Animator>().SetLayerWeight(1, Mathf.Abs(playerYVelocity)/10);

        transform.rotation = Quaternion.identity;
        BearTargerParentPos.transform.position = player.transform.position - Offset;
        transform.position = BearTargerParentPos.transform.position;
        BearArmature.transform.rotation = player.transform.rotation;

        if (player.GetComponent<RollingMovement>().canJump && canSquash && !up)
            {
            playerYVelocity = player.GetComponent<Rigidbody>().velocity.y;
            GetComponent<Animator>().SetTrigger("Squash");
            landingParticles.Play();
            canSquash = false;
        }
        if (!player.GetComponent<RollingMovement>().canJump && player.GetComponent<Rigidbody>().velocity.y < -0.1)
        {
            canSquash = true;
        }

        if (!player.GetComponent<RollingMovement>().canJump && canJumpSquish && up && Input.GetButton("Jump"))
        {
            GetComponent<Animator>().SetTrigger("Jump Squish");
            canJumpSquish = false;
        }

        if (player.GetComponent<RollingMovement>().canJump && !up)
        {
            canJumpSquish = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            splashParticles.Play();
        }
    }
}

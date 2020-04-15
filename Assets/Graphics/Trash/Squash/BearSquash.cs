using System.Collections;
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

    public bool up;

    public float playerYVelocity;

    public SphereCollider sphere;

    public float foodEaten;

    // References for scripts & components
    private PlayerScript playerScript;
    private RollingMovement rollingMovement;
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        playerScript = playerparent.GetComponent<PlayerScript>();
        rollingMovement = player.GetComponent<RollingMovement>();
        rb = player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canSquash = false;
        canJumpSquish = true;
        foodEaten = PlayerScript.AmountOfFoodEaten / 110;
    }

    // Update is called once per frame
    void Update()
    {
        //So that collider won't grow bigger than player model
        if (playerScript.sizeIncrease <= 1)
        {
            sphere.radius = 0.9f - foodEaten + (PlayerScript.AmountOfFoodEaten / 110);
        }


        //particle system
        if (!rollingMovement.canJump)
        {
            rollingPuffParticles.Play();
        }

        if (rb.velocity.y < 0)
        {
            up = false;
        }
        else
        {
            up = true;
        }

        animator.SetLayerWeight(1, Mathf.Abs(playerYVelocity) / 10);

        transform.rotation = Quaternion.identity;
        BearTargerParentPos.transform.position = player.transform.position - Offset;
        transform.position = BearTargerParentPos.transform.position;
        BearArmature.transform.rotation = player.transform.rotation;

        if (rollingMovement.canJump && canSquash && !up)
        {
            playerYVelocity = rb.velocity.y;
            animator.SetTrigger("Squash");
            landingParticles.Play();
            canSquash = false;
        }
        if (!rollingMovement.canJump && rb.velocity.y < -0.1)
        {
            canSquash = true;
        }

        if (!rollingMovement.canJump && canJumpSquish && up && RollingMovement.pressedJumpButton)
        {
            animator.SetTrigger("Jump Squish");
            canJumpSquish = false;
        }

        if (rollingMovement.canJump && !up)
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



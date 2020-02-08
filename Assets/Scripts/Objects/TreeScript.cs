using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public bool bounce;
    public float bounceAmount = 200;

    public Material bounceOnMaterial;
    public Material bounceOffMaterial;
    RollingMovement movementScript;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bounce) {
            meshRenderer.material = bounceOnMaterial;
        } else if (!bounce) {
            meshRenderer.material = bounceOffMaterial;
        }
    }
    void OnCollisionEnter(Collision collision) {

        //if collided object is player and Bounce bool true
        if(collision.gameObject.tag == "Player" && bounce) {
            movementScript = collision.gameObject.GetComponent<RollingMovement>();
            Rigidbody RB = collision.gameObject.GetComponent<Rigidbody>();

            print("eka: " + RB.velocity);
            // bounce player opposite direction of movement
            RB.AddForce(-movementScript.velocity.x * bounceAmount,
              movementScript.velocity.y,
              -movementScript.velocity.z * bounceAmount);
              
            print("toka: " + RB.velocity);

        }
    }
}

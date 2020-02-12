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

        //print("player pos:   " + player.transform.position);

    }
    void OnCollisionEnter(Collision collision) {

        //if collided object is player and Bounce bool true
        if(collision.gameObject.tag == "Player" && bounce) {
            movementScript = collision.gameObject.GetComponent<RollingMovement>();
            Rigidbody RB = collision.gameObject.GetComponent<Rigidbody>();

            //print("onko tää sama:  " + movementScript.gameObject.transform.position);

            // bounce player opposite direction of movement
            /*
            RB.AddForce(-movementScript.velocity.x * bounceAmount,
              movementScript.velocity.y,
              -movementScript.velocity.z * bounceAmount);
              */
           
            /*
            print(Vector3.Reflect(gameObject.transform.position, new Vector3(movementScript.gameObject.transform.position.x,
                movementScript.gameObject.transform.position.y,
                movementScript.gameObject.transform.position.z)).normalized);
            
            RB.AddForce(Vector3.Reflect(gameObject.transform.position,new Vector3(-movementScript.velocity.x * bounceAmount,
              movementScript.velocity.y,
              -movementScript.velocity.z * bounceAmount)));
              */
            
            
            //RB.AddForce(Vector3.Reflect(gameObject.transform.position, new Vector3(1,0,0) * bounceAmount));
              


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    public float movementSpeed;

    public float drag;

    public float dragLimit;

    public GameObject childObject;

    public Vector3 movementVector;

    public Vector3 rotateVector;
    public float velocityZ;
    public float velocityX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Drag();

        if(Input.GetKey(KeyCode.I)) {
            velocityZ += movementSpeed;
        }

        if(Input.GetKey(KeyCode.K)) {
            velocityZ -= movementSpeed;
        }

        if(Input.GetKey(KeyCode.L)) {
            velocityX += movementSpeed;
        }

        if(Input.GetKey(KeyCode.J)) {
            velocityX -= movementSpeed;
        }

        LimitVelocity();

        

        //-velocityX to flip rotation
        rotateVector.Set(velocityZ, 0, -velocityX);

        movementVector.Set(velocityX, 0, velocityZ);
        transform.Translate(movementVector  * Time.deltaTime);

        //childObject.transform.Rotate(velocityZ,0,velocityX);

        //childObject.transform.RotateAround(transform.position, rotateVector, 5);






        //childObject.transform.rotation = Quaternion.LookRotation(transform.forward);
        //childObject.transform.Rotate(transform.position.z,0,0);
        //childObject.transform.RotateAround(this.transform.position,rotateVector,1);
    }

    public void LimitVelocity() {
        if(velocityZ < -5) {
            velocityZ = -5;
        }
        if(velocityZ > 5) {
            velocityZ = 5;
        }

        if(velocityX < -5) {
            velocityX = -5;
        }
        if(velocityX > 5) {
            velocityX = 5;
        }
    }

    public void Drag() {
        if(velocityZ > dragLimit) {
            velocityZ -= drag;
        } else {
            //velocityZ = 0;
        }

        if(velocityZ < -dragLimit) {
            velocityZ += drag;
        } else {
            //velocityZ = 0;
        }

        if(velocityX > dragLimit) {
            velocityX -= drag;
        } else {
            //velocityX = 0;
        }

        if(velocityX < -dragLimit) {
            velocityX += drag;
        } else {
            //velocityX = 0;
        }

        if(velocityX < 0 && velocityX > -0.2f) {
            velocityX = 0;
        }
        if(velocityX > 0 && velocityX < 0.2f) {
            velocityX = 0;
        }
        if(velocityZ < 0 && velocityZ > -0.2f) {
            velocityZ = 0;
        }
        if(velocityZ > 0 && velocityZ < 0.2f) {
            velocityZ = 0;
        }
    }
}

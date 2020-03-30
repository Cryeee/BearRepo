using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNPC : MonoBehaviour
{
    public GameObject center;
    public float orbitSpeed;
    [SerializeField] private float height = 1f;
    [SerializeField] private float swimSpeed = 0.05f;
    [SerializeField] private float frequency = 0.1f;

    void Orbit()
    {
        transform.RotateAround(center.transform.position, Vector3.right, orbitSpeed * Time.deltaTime);
    }

    void Swim()
    {
        float x = transform.position.x;
        float y = Mathf.Sin(Time.time * frequency) *height; 
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }

    void Move()
    {
        
        transform.position += new Vector3(0f, 0f, swimSpeed);
        
    }

    void TurnAround()
    {
        swimSpeed = (-1 * swimSpeed);
        Debug.Log("saatana");
    }

    private void Start()
    {
        InvokeRepeating("TurnAround", 5.0f, 10.0f);
    }

    private void Update()
    {
        //Orbit();
        Swim();
        Move();
        
    }

}
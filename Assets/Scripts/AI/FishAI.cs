using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public GameObject cube;
    public float speed;

    void Orbit()
    {
        transform.RotateAround(cube.transform.position, Vector3.right, speed * Time.deltaTime);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Orbit();
    }

}
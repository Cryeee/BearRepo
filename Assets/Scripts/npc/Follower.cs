using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 1.0f;
    public float distanceTravelled;
    public Animator animator;

    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //animator.SetBool("jump", true);
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}

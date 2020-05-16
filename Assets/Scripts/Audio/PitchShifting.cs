using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchShifting : MonoBehaviour
{
    private AudioSource audioSource;
    public float minPitch;
    public float maxPitch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }
}
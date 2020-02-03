using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //player object
    public GameObject target;
    public int distance = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Camera position
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z - distance);
    }
}

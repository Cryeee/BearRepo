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
        RaycastHit hit = new RaycastHit();
        //Physics.Raycast(transform.position, Vector3.forward, out hit, 2.60f);
        
        if(Physics.SphereCast(gameObject.transform.position,0.5f, target.transform.position, out hit, 10)) {
            print("point: " + hit.point);
            Debug.DrawLine(gameObject.transform.position, hit.point, Color.red, 1.0f, false);
            gameObject.transform.position = hit.point;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gameObject.transform.position, target.transform.position);

    }
}

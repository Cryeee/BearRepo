using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSquash : MonoBehaviour
{
    public GameObject player;
    public GameObject BearTargerParentPos;
    public GameObject BearArmature;
    public Vector3 Offset;
    public bool canSquash;

    public bool up;

    // Start is called before the first frame update
    void Start()
    {
        canSquash = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(player.GetComponent<Rigidbody>().velocity.y < 0)
        {
            up = false;
        }
        else
        {
            up = true;
        }


        transform.rotation = Quaternion.identity;
        BearTargerParentPos.transform.position = player.transform.position - Offset;
        transform.position = BearTargerParentPos.transform.position;
        BearArmature.transform.rotation = player.transform.rotation;

        if (player.GetComponent<RollingMovement>().canJump && canSquash && !up)
        {
            GetComponent<Animator>().SetTrigger("Squash");
            canSquash = false;
        }
        if(!player.GetComponent<RollingMovement>().canJump)
        {
            canSquash = true;
        }
    }
}

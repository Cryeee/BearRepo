using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour
{
    public GameObject player;
    public float playerYPos;
    public Light spotLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        playerYPos = player.transform.position.y;

        if(playerYPos < -20)
        {
            spotLight.intensity = Mathf.Lerp(spotLight.intensity, 1000, 0.02f);

        }
        else
        {
            spotLight.intensity = 0;
        }
    }
}

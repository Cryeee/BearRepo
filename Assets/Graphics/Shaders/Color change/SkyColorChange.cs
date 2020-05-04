using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColorChange : MonoBehaviour
{
    public Color groundStartColor;
    public Color groundEndColor;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientGroundColor = groundStartColor;
    }

    // Update is called once per frame
    void Update()
    {
        t = 1 - (TimeController.currentTime / TimeController.startTime);
        RenderSettings.ambientGroundColor = Color.Lerp(groundStartColor, groundEndColor, t);
    }
}

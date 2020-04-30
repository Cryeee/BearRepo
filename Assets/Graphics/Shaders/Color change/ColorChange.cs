using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Material colorChangeMaterial;
    public float hue;
    public float t;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t = 1 - (TimeController.currentTime/ TimeController.startTime);
        hue = Mathf.Lerp(0, 1, t);
        colorChangeMaterial.SetFloat("_ammount", hue);
    }
}

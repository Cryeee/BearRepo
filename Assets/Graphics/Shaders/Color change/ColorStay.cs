using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStay : MonoBehaviour
{
    public Material colorChangeMaterial;
    public float hue;

    // Start is called before the first frame update
    void Start()
    {
        colorChangeMaterial.SetFloat("_ammount", hue);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

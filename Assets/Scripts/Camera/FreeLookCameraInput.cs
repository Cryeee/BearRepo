using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCameraInput : MonoBehaviour
{
    private PlayerInputs controls;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
        controls = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputs>();
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Cam X")
        {
            Debug.Log(controls.CameraInput.x);
            return controls.CameraInput.x;
        }
        else if (axisName == "Cam Y")
        {
            return controls.CameraInput.y;
        }

        return 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCameraInput : MonoBehaviour
{
    // THIS CLASS MAKES CINEMACHINE CAMERA USE NEW INPUT SYSTEM
    // AND WORK ON CONTROLLER AS WELL

    public InputHandler controls;

    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Cam X")
        {
            //Debug.Log(controls.CameraInput.x);
            return controls.CameraInput.x;
        }
        else if (axisName == "Cam Y")
        {
            return controls.CameraInput.y;
        }

        return 0;
    }


}

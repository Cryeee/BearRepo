using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldenBerry : MonoBehaviour
{
    public static Action OnPickedGoldenBerry;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            OnPickedGoldenBerry?.Invoke();
        }
    }
}

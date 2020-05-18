using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NavigateMenus : MonoBehaviour
{
    Mouse mouse;
    Gamepad pad;


    public EventSystem eventSystem;
    public GameObject firstSelected;
    private GameObject current;

    // Start is called before the first frame update
    void Start()
    {
        pad = Gamepad.current;
        current = firstSelected;
        if(pad != null)
        {
            SetSelected(firstSelected);
        }

        test();
    }

    void test()
    {
        InputSystem.onDeviceChange +=
       (device, change) =>
       {
           switch (change)
           {
               case InputDeviceChange.Added:
                   // New Device
                   pad = Gamepad.current;
                   SetSelected(current);
                    break;
               case InputDeviceChange.Reconnected:
                   pad = Gamepad.current;
                   SetSelected(current);
                   break;
               case InputDeviceChange.Disconnected:
                   pad = null;
                   SetSelected(null);
                   break;
               default:
                    // See InputDeviceChange reference for other event types.
                    break;
           }
       };
    }


    public void SetSelected(GameObject next)
    {
        current = next;

        if(pad != null)
        {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(next);
        }

    }

}

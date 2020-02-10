using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIinputs : MonoBehaviour, InputManager.IUIActions
{
    // Input mappings:
    private InputManager controls;

    private void Awake()
    {
        // InputManager is set in UnityEditor, this object
        // controls it
        controls = new InputManager();

        // This object listens to Player Actions -map's actions
        controls.UI.SetCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnableControls()
    {
        controls.Enable();
    }

    #region Interface-methods (don't touch)
    public void OnMove(InputAction.CallbackContext context)
    {
       
    }
    #endregion
}

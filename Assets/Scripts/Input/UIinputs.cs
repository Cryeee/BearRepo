using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIinputs : MonoBehaviour, InputManager.IUIActions
{
    // Input mappings:
    public InputManager controls;

    private void Awake()
    {
        // InputManager is set in UnityEditor, this object
        // controls it
        controls = new InputManager();

        // This object listens to Player Actions -map's actions
        controls.UI.SetCallbacks(this);

        //Enables controls
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Interface-methods (don't touch)

    public void OnNavigate(InputAction.CallbackContext context)
    {
        
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
       
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        
    }
    #endregion
}

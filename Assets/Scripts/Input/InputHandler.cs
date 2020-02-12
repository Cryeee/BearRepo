using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour, InputManager.IUIActions, InputManager.IPlayerActions
{
    // Input mappings:
    public InputManager controls;

    RollingMovement rollingMovement;

    // value of WASD/Left Stick
    public Vector2 MoveInput
    {
        get;
        private set;
    }

    public Vector2 CameraInput
    {
        get;
        private set;
    }

    private void Awake()
    {
        // InputManager is set in UnityEditor, this object
        // controls it
        controls = new InputManager();

        // This object listens to Player Actions -map's actions
        controls.Player.SetCallbacks(this);
        controls.UI.SetCallbacks(this);

        //Enables controls
        controls.Enable();

        
        rollingMovement = GetComponent<RollingMovement>();
    }

    void Update()
    {
        // read value from keyboard/controller
        MoveInput = controls.Player.Walking.ReadValue<Vector2>();
        CameraInput = controls.Player.Camera.ReadValue<Vector2>();
    }

    #region Interface-methods (don't touch)
    public void OnCamera(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            rollingMovement.Jump();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {

    }

    public void OnPoint(InputAction.CallbackContext context)
    {

    }

    public void OnSelect(InputAction.CallbackContext context)
    {

    }

    public void OnWalking(InputAction.CallbackContext context)
    {

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        
    }
    #endregion
}

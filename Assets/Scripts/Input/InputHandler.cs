using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour, InputManager.IUIActions, InputManager.IPlayerActions
{
    // Input mappings:
    public InputManager inputManager;

    RollingMovement rollingMovement;
	NormalMovement normalMovement;

    MenuController menuController;
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
        inputManager = new InputManager();

        // This object listens to Player Actions -map's actions
        inputManager.Player.SetCallbacks(this);
        inputManager.UI.SetCallbacks(this);

        //Enables controls
        inputManager.Enable();
        
        rollingMovement = GetComponentInChildren<RollingMovement>();
		normalMovement = GetComponentInChildren<NormalMovement>();

        menuController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuController>();
    }

    void Update()
    {
        // read value from keyboard/controller
        MoveInput = inputManager.Player.Walking.ReadValue<Vector2>();
        CameraInput = inputManager.Player.Camera.ReadValue<Vector2>();
    }

    #region Interface-methods (don't touch)
    public void OnCamera(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(rollingMovement != null)
            {
                rollingMovement.Jump();

            } else if (normalMovement != null)
			{
				normalMovement.Jump();
			}
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
        if (context.performed)
        {
            if(menuController != null)
            {
                menuController.Pause();
            }
            
        }
    }

    public void OnSUbmit(InputAction.CallbackContext context)
    {

    }

	public void OnRun(InputAction.CallbackContext context)
	{
		//if(context.started)
		//{
		//	if (normalMovement != null)
		//	{
		//		normalMovement.Run();
		//	}
		//} else if (context.canceled)
		//{
		//	if (normalMovement != null)
		//	{
		//		normalMovement.Walk();
		//	}
		//}
	}
	#endregion
}

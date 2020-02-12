using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, InputManager.IPlayerActions
{
    // Input mappings:
    private InputManager controls;

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

        // Enables controls
        controls.Enable();
    }

    void Update()
    {
        // read value from keyboard/controller
        MoveInput = controls.Player.Walking.ReadValue<Vector2>();
        CameraInput = controls.Player.Camera.ReadValue<Vector2>();
    }

    #region Interface-methods (don't touch):
    public void OnWalking(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnCamera(InputAction.CallbackContext context)
    {

    }
    #endregion
}

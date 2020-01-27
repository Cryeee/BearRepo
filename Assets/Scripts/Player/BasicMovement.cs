using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour, InputManager.IPlayerActions
{
    [SerializeField] float speed;

    // Input mappings:
    private InputManager controls;


    private void Awake()
    {
        // Luodaan inputtia kontrolloiva olio
        controls = new InputManager();

        // Kerrotaan DefaultInputs:lle, että tämä olio kuuntelee
        // Player action map:iin liittyviä actioneita
        controls.Player.SetCallbacks(this);
    }

    private void Start()
    {
        // Ota kontrollit käyttöön
        controls.Enable();
    }

    void Update()
    {
        Vector2 moveInput = controls.Player.Walking.ReadValue<Vector2>();
        transform.Translate(moveInput * speed * Time.deltaTime);
    }

    #region InputHandling

    public void OnWalking(InputAction.CallbackContext context)
    {
        
    }

    #endregion
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;

    private MessageBus _mBus;

    private InputAction _attack;
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _dash;
    private InputAction _slide;
    private InputAction _use;

    private void OnEnable()
    {
        _inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        _inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        Debug.Log("player init");

        Cursor.lockState = CursorLockMode.Locked;

        _inputActions.FindActionMap("Player").Enable();

        _attack = InputSystem.actions.FindAction("Attack");
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _jump = InputSystem.actions.FindAction("Jump");
        _dash = InputSystem.actions.FindAction("Dash");
        _slide = InputSystem.actions.FindAction("Slide");
        _use = InputSystem.actions.FindAction("Use");
    }

    private void Update()
    {
        // Debug.Log(_move.ReadValue<Vector2>());
        // Debug.Log(_look.ReadValue<Vector2>());

        if (_attack.WasPressedThisFrame())
        {
            Debug.Log("attack");
        }

        if (_jump.WasPressedThisFrame())
        {
            GameBootstrap.MessageBus.Publish(new Player_JumpMessage());
        }

        if (_dash.WasPressedThisFrame())
        {
            Debug.Log("dash");
        }

        if (_slide.WasPressedThisFrame())
        {
            Debug.Log("slide");
        }

        if (_use.WasPressedThisFrame())
        {
            Debug.Log("use");
        }
    }
}

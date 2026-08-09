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
        //GameBootstrap.MessageBus.Publish(new Player_LookMessage(_look.ReadValue<Vector2>()));

        // Debug.Log(_move.ReadValue<Vector2>());
        // Debug.Log(_look.ReadValue<Vector2>());

        if (_attack.WasPressedThisFrame())
        {
            GameBootstrap.MessageBus.Publish(new Player_AttackMessage());
        }

        if (_jump.WasPressedThisFrame())
        {
            GameBootstrap.MessageBus.Publish(new Player_JumpMessage());
        }

        if (_dash.WasPressedThisFrame())
        {
            GameBootstrap.MessageBus.Publish(new Player_DashMessage());
        }

        if (_slide.WasPressedThisFrame())
        {
            GameBootstrap.MessageBus.Publish(new Player_SlideMessage());
        }

        if (_use.WasPressedThisFrame())
        {
            GameBootstrap.MessageBus.Publish(new Player_UseMessage());
        }
    }
}

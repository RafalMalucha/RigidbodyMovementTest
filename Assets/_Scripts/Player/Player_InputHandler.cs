using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _attack;
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _dash;
    private InputAction _slide;
    private InputAction _use;
    private InputAction _grappleHook;
    private InputAction _melee;

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
        _grappleHook = InputSystem.actions.FindAction("GrappleHook");
        _melee = InputSystem.actions.FindAction("Melee");
    }

    private void Update()
    {
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_LookMessage(_look.ReadValue<Vector2>()));
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_MoveMessage(_move.ReadValue<Vector2>()));

        if (_attack.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_AttackMessage());
        }

        if (_jump.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_JumpMessage());
        }

        if (_dash.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_DashMessage());
        }

        if (_slide.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_SlideMessage());
        }

        if (_use.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_UseMessage());
        }

        if (_grappleHook.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_GrappleHookMessage());
        }

        if (_melee.WasPressedThisFrame())
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_MeleeMessage());
        }
    }
}

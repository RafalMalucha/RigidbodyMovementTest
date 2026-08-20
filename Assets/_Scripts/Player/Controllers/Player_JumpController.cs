using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_JumpController : MonoBehaviour
{
    [Header("Player_JumpController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _extraAirborneJumps;

    private Player_State _currentState;
    private Player_Modifier _currentModifier;
    private Player_StateModifierValues _currentStateModifierValues;
    private bool _isGrounded;
    private int _currenlyAvailableAirborneJumps;

    void OnEnable()
    {
        Debug.Log("jump controller enabled");
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierMessage>(OnPlayerStateModifierMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierValuesMessage>(OnPlayerStateModifierValuesMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        Jump();
    }

    void OnPlayerIsGroundedMessageReceived(Player_IsGroundedMessage message)
    {
        _isGrounded = message.IsGrounded;
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
        if (_currentState == Player_State.Airborne)
        {
            _currenlyAvailableAirborneJumps = _extraAirborneJumps;
        }
    }

    void OnPlayerStateModifierMessageReceived(Player_StateModifierMessage message)
    {
        _currentModifier = message.Player_Modifier;
    }

    void OnPlayerStateModifierValuesMessageReceived(Player_StateModifierValuesMessage message)
    {
        _currentStateModifierValues = message.Player_StateModifierValues;
        _jumpForce = _currentStateModifierValues.GetJumpForce();
    }

    private void Jump()
    {
        Debug.Log("player jump behavior");

        if (_currentState == Player_State.Grounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        if (_currentState == Player_State.Airborne && _currenlyAvailableAirborneJumps > 0)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _currenlyAvailableAirborneJumps -= 1;
        }
    }
}

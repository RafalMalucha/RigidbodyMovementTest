using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _extraAirborneJumps;

    private Player_State _player_State;
    private bool _isGrounded;
    private int _currenlyAvailableAirborneJumps;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
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
        _player_State = message.Player_State;
        if(_player_State == Player_State.Airborne)
        {
            _currenlyAvailableAirborneJumps = _extraAirborneJumps;
        }

    }

    private void Jump()
    {
        Debug.Log("player jump behavior");

        if (_player_State == Player_State.Grounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        if (_player_State == Player_State.Airborne && _currenlyAvailableAirborneJumps > 0)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _currenlyAvailableAirborneJumps -= 1;
        }
    }
}

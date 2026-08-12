using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveForce;

    private Player_State _player_State;
    private Vector2 _moveInput;
    private float _stateMovePenalty = 1f;

    void OnEnable()
    {
        Debug.Log("move controller enabled");
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnPlayerMoveMessageReceived(Player_MoveMessage message)
    {
        _moveInput = message.MoveInput;
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _player_State = message.Player_State;

        if (_player_State != Player_State.Airborne)
            _stateMovePenalty = 1f;

        if (_player_State == Player_State.Airborne)
            _stateMovePenalty = 0.25f;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = _moveInput.y * transform.forward + _moveInput.x * transform.right;
        movement.y = 0f;

        if (movement.magnitude > 1f)
            movement.Normalize();

        Vector3 horizontalForce = _stateMovePenalty * _moveForce * movement;
        _rigidbody.AddForce(horizontalForce, ForceMode.Force);

        Vector3 velocity = _rigidbody.linearVelocity;
        Vector3 hVelocity = new Vector3(velocity.x, 0f, velocity.z);

        hVelocity = Vector3.ClampMagnitude(hVelocity, 15f);

        _rigidbody.linearVelocity = new Vector3(hVelocity.x, velocity.y, hVelocity.z);
    }
}

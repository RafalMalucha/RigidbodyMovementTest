using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;

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

        if (_player_State == Player_State.Grounded)
            _stateMovePenalty = 1f;

        if (_player_State == Player_State.Airborne)
            _stateMovePenalty = 0.1f;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = _moveInput.y * transform.forward + _moveInput.x * transform.right;
        //Debug.Log(movement);

        if (movement.magnitude > 1f)
            movement.Normalize();

        _rigidbody.AddForce(_stateMovePenalty * _moveSpeed * movement);
        _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, 15f);
        //Debug.Log(_rigidbody.linearVelocity);
    }
}

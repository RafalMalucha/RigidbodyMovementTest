using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveForce;
    [SerializeField] private PhysicsMaterial _pMaterial;

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

        switch (_player_State)
        {
            case Player_State.Grounded:
                _stateMovePenalty = 1f;
                break;
            case Player_State.Airborne:
                _stateMovePenalty = 0.25f;
                break;
            case Player_State.Sliding:
                _stateMovePenalty = 0f;
                break;
            default:
                _stateMovePenalty = 1f;
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = _moveInput.y * transform.forward + _moveInput.x * transform.right;
        movement.y = 0f;

        if (GetGroundNormal(out Vector3 groundNormal))
        {
            float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);

            if (slopeAngle <= 46f && slopeAngle >= 5f)
            {
                movement = Vector3.ProjectOnPlane(movement, groundNormal);
                Debug.LogWarning("on slope");
                GameBootstrap.MessageBus.Publish(new Player_OnSlopeMessage(true));
                // _rigidbody.AddForce(new Vector3(0f, -_rigidbody.linearVelocity.y, 0f), ForceMode.Force);
            }
            else
            {
                GameBootstrap.MessageBus.Publish(new Player_OnSlopeMessage(false));
            }
        }

        if(_rigidbody.linearVelocity.y > 25f)
        {
            _rigidbody.AddForce(new Vector3(0f, -_rigidbody.linearVelocity.y, 0f), ForceMode.Impulse);
        }

        if (movement.magnitude > 1f)
            movement.Normalize();

        Vector3 horizontalForce = _stateMovePenalty * _moveForce * movement;
        _rigidbody.AddForce(horizontalForce, ForceMode.Force);

        Vector3 velocity = _rigidbody.linearVelocity;
        Vector3 hVelocity = new Vector3(velocity.x, 0f, velocity.z);

        hVelocity = Vector3.ClampMagnitude(hVelocity, 15f);

        _rigidbody.linearVelocity = new Vector3(hVelocity.x, velocity.y, hVelocity.z);
    }

    private bool GetGroundNormal(out Vector3 groundNormal)
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.35f, Color.red);

        if (Physics.Raycast(
            transform.position,
            Vector3.down,
            out RaycastHit hit,
            0.4f,
            LayerMask.GetMask("Level")))
        {
            groundNormal = hit.normal;
            return true;
        }

        groundNormal = Vector3.up;
        return false;
    }
}

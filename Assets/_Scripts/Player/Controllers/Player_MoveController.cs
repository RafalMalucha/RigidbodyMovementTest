using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MoveController : MonoBehaviour
{
    [Header("Player_MoveController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private PhysicsMaterial _pMaterial;

    private Player_State _currentState;
    private Player_Modifier _currentModifier;
    private Player_StateModifierValues _currentStateModifierValues;
    private Vector2 _moveInput;
    private float _stateMovePenalty = 1f;

    void OnEnable()
    {
        Debug.Log("move controller enabled");
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierMessage>(OnPlayerStateModifierMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierValuesMessage>(OnPlayerStateModifierValuesMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnPlayerMoveMessageReceived(Player_MoveMessage message)
    {
        _moveInput = message.MoveInput;
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;

        switch (_currentState)
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
            case Player_State.WallRunning:
                _stateMovePenalty = 0f;
                break;
            case Player_State.MonkeyBar:
                _stateMovePenalty = 0f;
                break;
            default:
                _stateMovePenalty = 1f;
                break;
        }
    }

    void OnPlayerStateModifierMessageReceived(Player_StateModifierMessage message)
    {
        _currentModifier = message.Player_Modifier;
    }

    void OnPlayerStateModifierValuesMessageReceived(Player_StateModifierValuesMessage message)
    {
        _currentStateModifierValues = message.Player_StateModifierValues;
        _moveForce = _currentStateModifierValues.GetMoveForce();
        _maxMoveSpeed = _currentStateModifierValues.GetMaxMoveSpeed();
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
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_OnSlopeMessage(true));
                // _rigidbody.AddForce(new Vector3(0f, -_rigidbody.linearVelocity.y, 0f), ForceMode.Force);
            }
            else
            {
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_OnSlopeMessage(false));
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

        if(_currentState != Player_State.Sliding)
        {
            hVelocity = Vector3.ClampMagnitude(hVelocity, _maxMoveSpeed);
        }

        if(_currentState == Player_State.WallRunning)
        {
            velocity.y = 0f;
        }

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

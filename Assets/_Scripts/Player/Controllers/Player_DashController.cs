using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_DashController : MonoBehaviour
{
    [Header("Player_DashController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    private Player_State _currentState;
    private Player_Modifier _currentModifier;
    private Player_StateModifierValues _currentStateModifierValues;
    private Vector2 _moveInput;
    private float _lastDashTime;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lastDashTime = 0f;
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_DashMessage>(OnPlayerDashMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierMessage>(OnPlayerStateModifierMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierValuesMessage>(OnPlayerStateModifierValuesMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_DashMessage>(OnPlayerDashMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
    }

    void OnPlayerDashMessageReceived(Player_DashMessage message)
    {
        Dash();
    }

    void OnPlayerMoveMessageReceived(Player_MoveMessage message)
    {
        _moveInput = message.MoveInput;
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
    }

    void OnPlayerStateModifierMessageReceived(Player_StateModifierMessage message)
    {
        _currentModifier = message.Player_Modifier;
    }

    void OnPlayerStateModifierValuesMessageReceived(Player_StateModifierValuesMessage message)
    {
        _currentStateModifierValues = message.Player_StateModifierValues;
        _dashForce = _currentStateModifierValues.GetDashForce();
    }

    void Dash()
    {
        if (_moveInput == new Vector2(0f, 0f))
        {
            if(_currentState == Player_State.Grounded || _currentState == Player_State.Airborne)
            {
                StartCoroutine(DashCoroutine(Vector3.forward));
            }
        }
        else
        {
            if (_currentState == Player_State.Grounded || _currentState == Player_State.Airborne)
            {
                StartCoroutine(DashCoroutine(new Vector3(_moveInput.x, 0f, _moveInput.y)));
            }
        }
    }

    IEnumerator DashCoroutine(Vector3 direction)
    {
        if (Time.time < _lastDashTime + _dashCooldown)
            yield break;

        _lastDashTime = Time.time;

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_DashStartMessage());

        float dashStartTime = Time.time;

        if (_lastDashTime + _dashCooldown > dashStartTime)
        {
            while (Time.time < dashStartTime + _dashDuration)
            {
                if(_currentState == Player_State.WallRunning)
                {
                    Debug.Log("end dash");
                    GameBootstrap.PlayerControllerMessageBus.Publish(new Player_DashFinishMessage());
                    yield return new WaitForFixedUpdate();
                    break;
                }
                _rigidbody.AddForce(transform.rotation * direction * _dashForce, ForceMode.Impulse);

                yield return new WaitForFixedUpdate();
            }
        }

        yield return new WaitForFixedUpdate();

        Vector3 tempVelocityHelper = _rigidbody.linearVelocity;
        _rigidbody.linearVelocity = new Vector3(tempVelocityHelper.x, 0f, tempVelocityHelper.z);

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_DashFinishMessage());
        yield return new WaitForFixedUpdate();
    }
}

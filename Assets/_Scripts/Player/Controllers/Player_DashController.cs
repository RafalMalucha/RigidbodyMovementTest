using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_DashController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    private Vector2 _moveInput;
    private float _lastDashTime;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lastDashTime = 0f;
        GameBootstrap.MessageBus.Subscribe<Player_DashMessage>(OnPlayerDashMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_DashMessage>(OnPlayerDashMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnPlayerDashMessageReceived(Player_DashMessage message)
    {
        Dash();
    }

    void OnPlayerMoveMessageReceived(Player_MoveMessage message)
    {
        _moveInput = message.MoveInput;
    }

    void Dash()
    {
        if (_moveInput == new Vector2(0f, 0f))
        {
            StartCoroutine(DashCoroutine(Vector3.forward));
        }
        else
        {
            StartCoroutine(DashCoroutine(new Vector3(_moveInput.x, 0f, _moveInput.y)));
        }
    }

    IEnumerator DashCoroutine(Vector3 direction)
    {
        if (Time.time < _lastDashTime + _dashCooldown)
            yield break;

        _lastDashTime = Time.time;

        GameBootstrap.MessageBus.Publish(new Player_DashStartMessage());

        float dashStartTime = Time.time;

        if (_lastDashTime + _dashCooldown > dashStartTime)
        {
            while (Time.time < dashStartTime + _dashDuration)
            {
                _rigidbody.AddForce(transform.rotation * direction * _dashForce, ForceMode.Impulse);
                yield return new WaitForFixedUpdate();
            }
        }

        yield return new WaitForFixedUpdate();
        Debug.Log("end dash");
        GameBootstrap.MessageBus.Publish(new Player_DashFinishMessage());
        yield return new WaitForFixedUpdate();
    }
}

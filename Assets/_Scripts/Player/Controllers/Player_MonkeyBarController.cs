using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MonkeyBarController : MonoBehaviour
{
    [Header("Player_MonkeyBarController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _monkeyBarSwingDuration;

    private Player_State _currentState;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MonkeyBarEnterMessage>(OnPlayerMonkeyBarEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MonkeyBarExitMessage>(OnPlayerMonkeyBarExitMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MonkeyBarEnterMessage>(OnPlayerMonkeyBarEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MonkeyBarExitMessage>(OnPlayerMonkeyBarExitMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
    }

    void OnPlayerMonkeyBarEnterMessageReceived(Player_MonkeyBarEnterMessage message)
    {
        MonkeyBar(message.BarPosition);
    }

    void OnPlayerMonkeyBarExitMessageReceived(Player_MonkeyBarExitMessage message)
    {

    }

    void MonkeyBar(Vector3 barPosition)
    {
        if (_currentState == Player_State.Airborne || _currentState == Player_State.MonkeyBar)
        {
            Debug.LogWarning("Start monkey bar");
            StartCoroutine(MonkeyBarCoroutine(barPosition));
        }
    }

    IEnumerator MonkeyBarCoroutine(Vector3 barPosition)
    {
        _rigidbody.linearVelocity = Vector3.zero;

        Vector3 start = transform.position;
        //Vector3 mid = start + transform.rotation * Vector3.forward * 1.5f + 0.35f * barPosition.y * Vector3.down;
        Vector3 mid = start + transform.rotation * Vector3.forward * 1.5f + 1.0f * Vector3.down;
        Vector3 end = start + transform.rotation * Vector3.forward * 4f;

        // Calculate control point so the curve passes exactly through mid at t = 0.5
        Vector3 control = 2f * mid - 0.5f * start - 0.5f * end;

        float elapsed = 0f;

        while (elapsed < _monkeyBarSwingDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / _monkeyBarSwingDuration);
            float u = 1f - t;

            Vector3 bezierCurvePosition = u * u * start + 2f * u * t * control + t * t * end;

            _rigidbody.MovePosition(bezierCurvePosition);

            yield return null;
        }

        _rigidbody.MovePosition(end);

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_MonkeyBarExitMessage());

        _rigidbody.AddForce(Vector3.up * 90f, ForceMode.Impulse);
        _rigidbody.AddForce(transform.rotation * Vector3.forward * 175f, ForceMode.Impulse);
    }
}

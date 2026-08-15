using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_LookController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Rigidbody _rigidbody;

    private Player_State _playerState;
    private Vector2 _lookDelta;
    private float _cameraRotationX = 0f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        GameBootstrap.MessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_LookMessage>(OnPlayerLookMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunLimitRotationMessage>(OnPlayerWallrunLimitRotationMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_LookMessage>(OnPlayerLookMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_WallrunLimitRotationMessage>(OnPlayerWallrunLimitRotationMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _playerState = message.Player_State;
    }

    void OnPlayerLookMessageReceived(Player_LookMessage message)
    {
        _lookDelta = message.LookDelta;
    }

    void OnPlayerWallrunLimitRotationMessageReceived(Player_WallrunLimitRotationMessage message)
    {
        StartCoroutine(LerpRotateToWallrunDirection(message.WallrunDirection));
    }

    void OnPlayerWallrunExitMessageReceived(Player_WallrunExitMessage message)
    {

    }

    private void FixedUpdate()
    {
        Look();
    }

    private void Look()
    {
        if (_playerState == Player_State.WallRunning)
            return;

        float hLook = _lookDelta.x * Time.fixedDeltaTime * 25f;
        float vLook = _lookDelta.y * Time.fixedDeltaTime * 25f;

        _cameraRotationX -= vLook;
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -90f, 90f);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotationX, 0f, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, hLook, 0f));
    }

    IEnumerator LerpRotateToWallrunDirection(Vector3 wallrunDirection)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(wallrunDirection);

        float duration = 0.1f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        transform.rotation = targetRotation;
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_LookController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Rigidbody _rigidbody;
    private Vector2 _lookDelta;
    private float _cameraRotationX = 0f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_LookMessage>(OnPlayerLookMessageReceived);
    }

    void OnPlayerLookMessageReceived(Player_LookMessage message)
    {
        _lookDelta = message.LookDelta;
    }

    private void FixedUpdate()
    {
        Look();
    }

    void Look()
    {
        float hLook = _lookDelta.x * Time.fixedDeltaTime * 25f;
        float vLook = _lookDelta.y * Time.fixedDeltaTime * 25f;

        _cameraRotationX -= vLook;
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -90f, 90f);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotationX, 0f, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, hLook, 0f));
    }
}

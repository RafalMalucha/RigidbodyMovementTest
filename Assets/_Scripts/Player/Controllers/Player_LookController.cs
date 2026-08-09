using UnityEngine;

public class Player_LookController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    private float cameraRotationX = 0f;

    void Awake()
    {
        GameBootstrap.MessageBus.Subscribe<Player_LookMessage>(OnPlayerLookMessageReceived);
    }

    void OnPlayerLookMessageReceived(Player_LookMessage message)
    {
        Look(message.LookDelta);
    }

    void Look(Vector2 lookDelta)
    {
        float hLook = lookDelta.x * Time.deltaTime * 25f;
        float vLook = lookDelta.y * Time.deltaTime * 25f;

        cameraRotationX -= vLook;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);

        _playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        transform.Rotate(Vector3.up * hLook);
    }
}

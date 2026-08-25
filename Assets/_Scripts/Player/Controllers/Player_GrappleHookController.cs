using UnityEngine;

public class Player_GrappleHookController : MonoBehaviour
{
    [Header("Player_GrappleHookController Setup")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _grappleDistance;
    [SerializeField] private LayerMask interactableLayer;

    private Ray _grappleRay;
    private RaycastHit _raycastHit;

    void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_GrappleHookMessage>(OnPlayerGrappleHookMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_GrappleHookMessage>(OnPlayerGrappleHookMessageReceived);
    }

    void OnPlayerGrappleHookMessageReceived(Player_GrappleHookMessage message)
    {
        GrappleHook();
    }

    void GrappleHook()
    {
        Debug.Log("player Grapple behavior");
        _grappleRay = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

        if (Physics.Raycast(_grappleRay, out _raycastHit, _grappleDistance, interactableLayer))
        {
            if (_raycastHit.collider.CompareTag("Grappable"))
            {
                //start grapple coroutine
            }
        }
    }

    private void Update()
    {
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _grappleDistance, Color.pink);
    }
}

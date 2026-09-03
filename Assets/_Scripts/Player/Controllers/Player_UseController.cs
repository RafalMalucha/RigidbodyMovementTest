using UnityEngine;

public class Player_UseController : MonoBehaviour
{
    [Header("Player_UseController Setup")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _useDistance;
    [SerializeField] private LayerMask _interactableLayer;

    private Ray _useRay;
    private RaycastHit _raycastHit;

    void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_UseMessage>(OnPlayerUseMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_UseMessage>(OnPlayerUseMessageReceived);
    }

    void OnPlayerUseMessageReceived(Player_UseMessage message)
    {
        Use();
    }

    void Use()
    {
        Debug.Log("player Use behavior");
        _useRay = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

        if (Physics.Raycast(_useRay, out _raycastHit, _useDistance, _interactableLayer))
        {
            if (_raycastHit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                Debug.Log(_raycastHit.collider.GetInstanceID());
                interactable.Interact();
            }
        }
    }

    private void Update()
    {
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _useDistance, Color.red);
    }
}

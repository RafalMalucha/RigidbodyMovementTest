using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_GrappleHookController : MonoBehaviour
{
    [Header("Player_GrappleHookController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _grappleDistance;
    [SerializeField] private float _grappleSpeed;
    [SerializeField] private LayerMask interactableLayer;

    private Ray _grappleRay;
    private RaycastHit _raycastHit;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_GrappleEnterMessage());
                StartCoroutine(GrappleCoroutine(_raycastHit.collider.transform.position));
            }
        }
    }

    IEnumerator GrappleCoroutine(Vector3 grappleTargetPoint)
    {
        Vector3 grappleStartPoint = transform.position;
        float grappleDuration = Vector3.Distance(grappleStartPoint, grappleTargetPoint) / _grappleSpeed;

        float elapsed = 0f;

        while (elapsed < grappleDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / grappleDuration);

            transform.position = Vector3.Lerp(grappleStartPoint,grappleTargetPoint,t);

            yield return null;
        }

        transform.position = grappleTargetPoint;

        Vector3 tempVelocityHelper = _rigidbody.linearVelocity;

        _rigidbody.linearVelocity = new Vector3(tempVelocityHelper.x, 0f, tempVelocityHelper.z);
        _rigidbody.AddForce(transform.forward * 1500f, ForceMode.Impulse);
        _rigidbody.AddForce(_playerCamera.transform.forward * 150f, ForceMode.Impulse);
        _rigidbody.AddForce(transform.up * 1f, ForceMode.Impulse);

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_GrappleExitMessage());
        yield return null;
    }

    private void Update()
    {
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _grappleDistance, Color.pink);
    }
}

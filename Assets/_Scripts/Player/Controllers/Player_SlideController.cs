using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_SlideController : MonoBehaviour
{
    [Header("Player_SlideController Setup")]
    [Space]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PhysicsMaterial _pMaterial;
    [SerializeField] private Vector2 _pMaterialNormalValues;
    [SerializeField] private Vector2 _pMaterialSlideValues;

    [Header("Modified on slide")]
    [Space]
    [SerializeField] private CapsuleCollider _playerCapsuleCollider;
    [SerializeField] private BoxCollider _playerBoxCollider;
    [SerializeField] private GameObject _playerBody;
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private GameObject _mainCamera;

    [Header("Main Camera Values")]
    [Space]
    [SerializeField] private Vector3 _mainCameraDefaultPosition;
    [SerializeField] private Vector3 _mainCameraSlidePosition;

    [Header("Capsule Collider Values")]
    [Space]
    [SerializeField] private float _capsuleDefaultHeight;
    [SerializeField] private float _capsuleSlideHeight;

    [Header("Box Collider Values")]
    [Space]
    [SerializeField] private Vector3 _boxDefaultSize;
    [SerializeField] private Vector3 _boxSlideSize;

    [Header("Player Body Values")]
    [Space]
    [SerializeField] private Vector3 _bodyDefaultSize;
    [SerializeField] private Vector3 _bodySlideSize;

    [Header("Ground Check Values")]
    [Space]
    [SerializeField] private Vector3 _groundCheckDefaultPosition;
    [SerializeField] private Vector3 _groundCheckSlidePosition;

    private bool _isSliding = false;
    private Player_State _currentState;

    private GameObject _weaponHolderGameObject;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weaponHolderGameObject = GameObject.Find("WeaponHolder");
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
    }

    void OnPlayerSlideMessageReceived(Player_SlideMessage message)
    {
        if (!_isSliding)
        {
            if (_currentState == Player_State.Grounded || _currentState == Player_State.Airborne)
            {
                StartSlide();
                _isSliding = true;
            }
        }
        else
        {
            StopSlide();
            _isSliding = false;
        }
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        if (_isSliding)
        {
            StopSlide();
            _isSliding = false;
        }
    }

    void StartSlide()
    {
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_SlideStartMessage());

        _playerCapsuleCollider.height = _capsuleSlideHeight;
        _playerBoxCollider.size = _boxSlideSize;
        _playerBody.transform.localScale = _bodySlideSize;
        _groundCheck.transform.localPosition = _groundCheckSlidePosition;
        _mainCamera.transform.localPosition = _mainCameraSlidePosition;

        _pMaterial.staticFriction = _pMaterialSlideValues.x;
        _pMaterial.dynamicFriction = _pMaterialSlideValues.y;
        if (_rigidbody.linearVelocity != Vector3.zero)
        {
            _rigidbody.AddForce(_rigidbody.linearVelocity * 2f, ForceMode.Impulse);
        }
        else
        {
            _rigidbody.AddForce(transform.rotation * Vector3.forward * 50f, ForceMode.Impulse);
        }
        Debug.Log("player start slide behavior");
    }

    void StopSlide()
    {
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_SlideFinishMessage());

        _playerCapsuleCollider.height = _capsuleDefaultHeight;
        _playerBoxCollider.size = _boxDefaultSize;
        _playerBody.transform.localScale = _bodyDefaultSize;
        _groundCheck.transform.localPosition = _groundCheckDefaultPosition;
        _mainCamera.transform.localPosition = _mainCameraDefaultPosition;

        _pMaterial.staticFriction = _pMaterialNormalValues.x;
        _pMaterial.dynamicFriction = _pMaterialNormalValues.y;
        Debug.Log("player stop slide behavior");
    }
}

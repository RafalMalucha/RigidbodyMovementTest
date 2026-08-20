using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_SlideController : MonoBehaviour
{
    [Header("Player_SlideController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PhysicsMaterial _pMaterial;
    [SerializeField] private Vector2 _pMaterialNormalValues;
    [SerializeField] private Vector2 _pMaterialSlideValues;

    private bool _isSliding = false;
    private Player_State _currentState;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
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
        GameBootstrap.MessageBus.Publish(new Player_SlideStartMessage());
        transform.localScale = new Vector3(1f, 0.5f, 1f);
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
        GameBootstrap.MessageBus.Publish(new Player_SlideFinishMessage());
        transform.localScale = new Vector3(1f, 1f, 1f);
        _pMaterial.staticFriction = _pMaterialNormalValues.x;
        _pMaterial.dynamicFriction = _pMaterialNormalValues.y;
        Debug.Log("player stop slide behavior");
    }
}

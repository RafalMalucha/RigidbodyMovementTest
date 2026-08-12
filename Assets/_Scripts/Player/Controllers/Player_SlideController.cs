using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_SlideController : MonoBehaviour
{
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
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
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

    void StartSlide()
    {
        GameBootstrap.MessageBus.Publish(new Player_SlideStartMessage());
        transform.localScale = new Vector3(1f, 0.5f, 1f);
        _pMaterial.staticFriction = _pMaterialSlideValues.x;
        _pMaterial.dynamicFriction = _pMaterialSlideValues.y;
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

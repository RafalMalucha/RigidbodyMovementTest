using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        Jump();
    }

    void OnPlayerIsGroundedMessageReceived(Player_IsGroundedMessage message)
    {
        _isGrounded = message.IsGrounded;
    }

    private void Jump()
    {
        Debug.Log("player jump behavior");

        if(_isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}

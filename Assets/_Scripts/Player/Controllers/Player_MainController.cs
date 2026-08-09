using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MainController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _playerGravity;

    private bool _isGrounded;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.down * _playerGravity, ForceMode.Force);
    }

    void Update()
    {
        bool grounded = CheckIsGrounded();

        if (grounded != _isGrounded)
        {
            _isGrounded = grounded;
            GameBootstrap.MessageBus.Publish(new Player_IsGroundedMessage(grounded));
        }
    }

    private bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 1.5f, 0f), Vector3.down, 1.51f, LayerMask.GetMask("Level"));
    }
}

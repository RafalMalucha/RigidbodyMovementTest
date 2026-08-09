using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;

    private Vector2 _moveInput;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnPlayerMoveMessageReceived(Player_MoveMessage message)
    {
        _moveInput = message.MoveInput;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = _moveInput.y * transform.forward + _moveInput.x * transform.right;

        if (movement.magnitude > 1f)
            movement.Normalize();

        _rigidbody.AddForce(_moveSpeed * movement);
    }
}

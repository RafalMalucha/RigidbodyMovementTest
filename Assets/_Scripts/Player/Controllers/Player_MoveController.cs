using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_MoveMessage>(OnPlayerMoveMessageReceived);
    }

    void OnPlayerMoveMessageReceived(Player_MoveMessage message)
    {
        Look(message.MoveInput);
    }

    void Look(Vector2 moveInput)
    {
        Vector3 movement = moveInput.y * transform.forward + moveInput.x * transform.right;

        if (movement.magnitude > 1f)
            movement.Normalize();

        _rigidbody.AddForce(100f * movement);
    }
}

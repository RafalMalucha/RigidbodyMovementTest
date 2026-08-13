using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_WallrunController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _rWallrunCheck;
    [SerializeField] private BoxCollider _lWallrunCheck;

    private Player_State _currentState;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunEnterMessage>(OnPlayerWallrunEnterMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_WallrunEnterMessage>(OnPlayerWallrunEnterMessageReceived);
        GameBootstrap.MessageBus.Unsubscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        Debug.Log("wallrun controller jump");
    }

    void OnPlayerWallrunEnterMessageReceived(Player_WallrunEnterMessage message)
    {

    }

    void OnPlayerWallrunExitMessageReceived(Player_WallrunExitMessage message)
    {

    }
}

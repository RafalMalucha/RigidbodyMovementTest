using UnityEngine;

public class Player_JumpController : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("subscribing");
        GameBootstrap.MessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        Debug.Log("jump message received");
        Jump();
    }

    void Jump()
    {
        Debug.Log("player jump behavior");
    }
}

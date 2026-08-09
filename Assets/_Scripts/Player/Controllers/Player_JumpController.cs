using UnityEngine;

public class Player_JumpController : MonoBehaviour
{
    void Awake()
    {
        GameBootstrap.MessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        Jump();
    }

    void Jump()
    {
        Debug.Log("player jump behavior");
    }
}

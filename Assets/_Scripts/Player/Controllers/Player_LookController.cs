using UnityEngine;

public class Player_LookController : MonoBehaviour
{
    void Awake()
    {
        GameBootstrap.MessageBus.Subscribe<Player_LookMessage>(OnPlayerLookMessageReceived);
    }

    void OnPlayerLookMessageReceived(Player_LookMessage message)
    {
        Look(message.LookDelta);
        Debug.Log(message.LookDelta);
    }

    void Look(Vector2 lookDelta)
    {
        //Debug.Log(lookDelta);
    }
}

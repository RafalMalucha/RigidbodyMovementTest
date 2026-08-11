using UnityEngine;

public class Player_UseController : MonoBehaviour
{
    void OnEnable()
    {
        GameBootstrap.MessageBus.Subscribe<Player_UseMessage>(OnPlayerUseMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_UseMessage>(OnPlayerUseMessageReceived);
    }

    void OnPlayerUseMessageReceived(Player_UseMessage message)
    {
        Use();
    }

    void Use()
    {
        Debug.Log("player Use behavior");
    }
}

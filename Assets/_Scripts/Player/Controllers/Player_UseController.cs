using UnityEngine;

public class Player_UseController : MonoBehaviour
{
    void Awake()
    {
        GameBootstrap.MessageBus.Subscribe<Player_UseMessage>(OnPlayerUseMessageReceived);
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

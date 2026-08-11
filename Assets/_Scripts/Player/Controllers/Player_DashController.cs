using UnityEngine;

public class Player_DashController : MonoBehaviour
{
    void OnEnable()
    {
        GameBootstrap.MessageBus.Subscribe<Player_DashMessage>(OnPlayerDashMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_DashMessage>(OnPlayerDashMessageReceived);
    }

    void OnPlayerDashMessageReceived(Player_DashMessage message)
    {
        Dash();
    }

    void Dash()
    {
        Debug.Log("player dash behavior");
    }
}

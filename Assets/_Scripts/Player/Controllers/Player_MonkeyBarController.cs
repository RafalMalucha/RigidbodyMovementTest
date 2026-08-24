using UnityEngine;

public class Player_MonkeyBarController : MonoBehaviour
{
    [Header("Player_MonkeyBarController Setup")]
    [SerializeField] private bool _test;

    void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MonkeyBarEnterMessage>(OnPlayerMonkeyBarEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MonkeyBarExitMessage>(OnPlayerMonkeyBarExitMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MonkeyBarEnterMessage>(OnPlayerMonkeyBarEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MonkeyBarExitMessage>(OnPlayerMonkeyBarExitMessageReceived);
    }

    void OnPlayerMonkeyBarEnterMessageReceived(Player_MonkeyBarEnterMessage message)
    {

    }

    void OnPlayerMonkeyBarExitMessageReceived(Player_MonkeyBarExitMessage message)
    {

    }
}

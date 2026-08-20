using UnityEngine;
using TMPro;

public class StateDisplay : MonoBehaviour
{
    public TextMeshProUGUI PlayerStateText;
    private Player_State _playerState;

    private void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _playerState = message.Player_State;
    }

    void Update()
    {
        PlayerStateText.text = $"State: {_playerState}";
    }
}

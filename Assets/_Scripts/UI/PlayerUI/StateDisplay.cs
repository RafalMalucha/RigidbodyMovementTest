using UnityEngine;
using TMPro;

public class StateDisplay : MonoBehaviour
{
    public TextMeshProUGUI PlayerStateText;
    public TextMeshProUGUI PlayerStateModifierText;
    private Player_State _playerState;
    private Player_Modifier _playerModifier;

    private void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateModifierMessage>(OnPlayerStateModifierMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _playerState = message.Player_State;
    }

    void OnPlayerStateModifierMessageReceived(Player_StateModifierMessage message)
    {
        _playerModifier = message.Player_Modifier;
    }

    void Update()
    {
        PlayerStateText.text = $"State: {_playerState}";
        PlayerStateModifierText.text = $"Modifier: {_playerModifier}";
    }
}

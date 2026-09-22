using UnityEngine;
using TMPro;

public class PlayerHealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI PlayerHealthText;
    private int _currentPlayerHealth;

    public TextMeshProUGUI PlayerOverhealText;
    private int _currentPlayerOverheal;

    private void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_HealthCurrentHealthMessage>(OnPlayerCurrentHealthMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_HealthOverhealMessage>(OnPlayerOverhealMessageReceived);
    }

    void OnPlayerCurrentHealthMessageReceived(Player_HealthCurrentHealthMessage message)
    {
        _currentPlayerHealth = message.CurrentHealth;
    }

    void OnPlayerOverhealMessageReceived(Player_HealthOverhealMessage message)
    {
        _currentPlayerOverheal = message.CurrentOverheal;
    }

    void Update()
    {
        PlayerHealthText.text = $"{_currentPlayerHealth}";
        PlayerOverhealText.text = $"+ {_currentPlayerOverheal}";
    }
}

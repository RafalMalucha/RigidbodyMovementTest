using UnityEngine;
using TMPro;

public class PlayerHealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI PlayerHealthText;
    private int _currentPlayerHealth;

    private void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_HealthCurrentHealthMessage>(OnPlayerCurrentMessageReceived);
    }

    void OnPlayerCurrentMessageReceived(Player_HealthCurrentHealthMessage message)
    {
        Debug.LogWarning("health display message received");
        _currentPlayerHealth = message.CurrentHealth;
    }

    void Update()
    {
        PlayerHealthText.text = $"{_currentPlayerHealth}";
    }
}

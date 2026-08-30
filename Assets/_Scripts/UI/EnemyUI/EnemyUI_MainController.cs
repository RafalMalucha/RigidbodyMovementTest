using UnityEngine;
using TMPro;

public class EnemyUI_MainController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyHealthText;
    private int _currentEnemyHealth;

    void Update()
    {
        _enemyHealthText.text = $"{_currentEnemyHealth}";
    }

    public void SetNewEnemyHealth(int newEnemyHealth)
    {
        _currentEnemyHealth = newEnemyHealth;
    }
}

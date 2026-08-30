using UnityEngine;
using UnityEngine.Events;

public class Enemy_HealthController : MonoBehaviour
{
    [Header("Entity_HealthController Setup")]
    [SerializeField] private int _maxBaseHealth;
    [SerializeField] private UnityEvent<int> _updateHealthUI;

    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxBaseHealth;
        _updateHealthUI?.Invoke(_currentHealth);
    }

    private void RestoreHealth(int totalHealAmount)
    {
        _currentHealth += totalHealAmount;

        if (_currentHealth > _maxBaseHealth)
            _currentHealth = _maxBaseHealth;

        _updateHealthUI?.Invoke(_currentHealth);
    }

    private void ApplyDamage(int totalDamageAmount)
    {
        _currentHealth -= totalDamageAmount;

        if (_currentHealth <= 0)
            EntityDie();

        _updateHealthUI?.Invoke(_currentHealth);
    }

    private void EntityDie()
    {
        Destroy(transform.parent.gameObject);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}

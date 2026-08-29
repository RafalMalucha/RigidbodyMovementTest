using UnityEngine;

public class Entity_HealthController : MonoBehaviour
{
    [Header("Entity_HealthController Setup")]
    [SerializeField] private int _maxBaseHealth;

    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxBaseHealth;
    }

    private void RestoreHealth(int totalHealAmount)
    {
        _currentHealth += totalHealAmount;

        if (_currentHealth > _maxBaseHealth)
            _currentHealth = _maxBaseHealth;
    }

    private void ApplyDamage(int totalDamageAmount)
    {
        _currentHealth -= totalDamageAmount;

        if (_currentHealth <= 0)
            EntityDie();
    }

    private void EntityDie()
    {
        Destroy(transform.parent.gameObject);
    }

    private void FixedUpdate()
    {
        Debug.Log(_currentHealth);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}

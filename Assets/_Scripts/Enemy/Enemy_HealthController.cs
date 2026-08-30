using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy_HealthController : MonoBehaviour
{
    [Header("Entity_HealthController Setup")]
    [SerializeField] private int _maxBaseHealth;
    [SerializeField] private UnityEvent<int> _updateHealthUI;

    private Rigidbody _rigidbody;
    private int _currentHealth;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        Destroy(transform.gameObject);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void EnemyOnMeleeHit(int hitDamage, int hitForce, Vector3 hitPushDirection)
    {
        _rigidbody.AddForce(hitPushDirection * hitForce, ForceMode.Impulse);

        ApplyDamage(hitDamage);
    }
}

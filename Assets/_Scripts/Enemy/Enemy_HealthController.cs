using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy_HealthController : MonoBehaviour
{
    [Header("Entity_HealthController Setup")]
    [Space]
    [SerializeField] private int _maxBaseHealth;
    [SerializeField] private float _collisionDamageCooldown;

    [Header("Events Setup")]
    [Space]
    [SerializeField] private UnityEvent<int> _updateHealthUI;
    [SerializeField] private UnityEvent<int> _damageEffectUI;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private int _currentHealth;

    private float _lastFrameLinearVelocityMagnitude;
    private float _lastWallHitTime;

    private void OnEnable()
    {
        _lastWallHitTime = Time.time;
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponentInChildren<BoxCollider>();
        _currentHealth = _maxBaseHealth;
        _updateHealthUI?.Invoke(_currentHealth);
    }

    private void LateUpdate()
    {
        _lastFrameLinearVelocityMagnitude = _rigidbody.linearVelocity.magnitude;
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
        _damageEffectUI?.Invoke(totalDamageAmount);

        if (_currentHealth <= 0)
            EntityDie();

        _updateHealthUI?.Invoke(_currentHealth);
    }

    private void EntityDie()
    {
        Destroy(transform.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer != 6)
            return;

        if (_lastFrameLinearVelocityMagnitude < 10f)
            return;

        if (_lastWallHitTime > Time.time + _collisionDamageCooldown)
            return;

        _lastWallHitTime = Time.time;
        ApplyDamage((int)(_lastFrameLinearVelocityMagnitude / 5f));
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

    public void EnemyOnRaycastHit(int hitDamage, Vector3 hitDirection)
    {
        _rigidbody.AddForce(hitDirection * 50f, ForceMode.Impulse);

        ApplyDamage(hitDamage);
    }
}

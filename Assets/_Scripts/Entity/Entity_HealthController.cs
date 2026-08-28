using UnityEngine;

public class Entity_HealthController : MonoBehaviour
{
    [Header("Entity_HealthController Setup")]
    [SerializeField] private int _maxBaseHealth;
    [SerializeField] private int _maxOverheal;
    [SerializeField] private int _overhealDecayRate;

    private int _currentHealth;
    private int _currentOverheal;

    private void RestoreHealth(int totalHealAmount)
    {
        int baseHealthRestoreAmount = _maxBaseHealth - _currentHealth;
    }

    private void EntityDie()
    {
        Destroy(transform.parent.gameObject);
    }

    private void Overheal(int overhealAmount)
    {
        //_currentOverheal
    }

    private void FixedUpdate()
    {
        Debug.Log(_currentHealth);
    }
}

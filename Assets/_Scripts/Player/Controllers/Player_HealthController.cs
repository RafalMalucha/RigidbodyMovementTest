using UnityEngine;

public class Player_HealthController : MonoBehaviour
{
    [Header("Player_HealthController Setup")]
    [Space]
    [SerializeField] private int _maxPlayerHealth;
    [SerializeField] private int _maxPlayerOverheal;
    [SerializeField] private int _ticksPerOverhealReduction;

    private int _currentPlayerHealth;
    private int _currentPlayerOverheal;

    private int _overhealReductionTickCounter = 0;


    void OnEnable()
    {
        SetNewPlayerHealth(_maxPlayerHealth);
        SetNewPlayerOverheal(_maxPlayerOverheal);
    }

    void Update()
    {
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthCurrentHealthMessage(_currentPlayerHealth));
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthOverhealMessage(_currentPlayerOverheal));
    }

    void FixedUpdate()
    {
        _overhealReductionTickCounter++;

        if (_overhealReductionTickCounter >= _ticksPerOverhealReduction && _currentPlayerOverheal > 0)
        {
            _currentPlayerOverheal -= 1;
            _overhealReductionTickCounter = 0;
        }
    }

    private void SetNewPlayerHealth(int health)
    {
        _currentPlayerHealth = health;
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthCurrentHealthMessage(_currentPlayerHealth));
    }

    private void SetNewPlayerOverheal(int overheal)
    {
        _currentPlayerOverheal = overheal;
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthOverhealMessage(_currentPlayerOverheal));
    }

    private void AddHealth(int healAmount)
    {
        _currentPlayerHealth += healAmount;

        if (_currentPlayerHealth > _maxPlayerHealth)
            _currentPlayerHealth = _maxPlayerHealth;

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthCurrentHealthMessage(_currentPlayerHealth));
    }

    private void AddOverheal(int overhealAmount)
    {
        _currentPlayerOverheal += overhealAmount;

        if (_currentPlayerOverheal > _maxPlayerOverheal)
            _currentPlayerOverheal = _maxPlayerOverheal;

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthOverhealMessage(_currentPlayerOverheal));
    }

    private void RemoveHealth(int healthDamageAmount)
    {
        _currentPlayerHealth -= healthDamageAmount;

        if (_currentPlayerHealth <= 0)
        {
            Debug.LogError("player dead");
        }
    }

    private void RemoveOverheal(int overhealDamageAmount)
    {
        _currentPlayerOverheal -= overhealDamageAmount;

        if (_currentPlayerOverheal <= 0)
            SetNewPlayerOverheal(0);
    }

    public void ApplyHeal(int healAmount)
    {
        int missingHealth = _maxPlayerHealth - _currentPlayerHealth;

        if (healAmount <= missingHealth)
            AddHealth(healAmount);

        if (healAmount > missingHealth)
        {
            int remainingOverheal = healAmount - missingHealth;
            AddHealth(missingHealth);
            AddOverheal(remainingOverheal);
        }
    }

    public void ApplyDamage(int damageAmount)
    {
        int overhealDamage = Mathf.Min(damageAmount, _currentPlayerOverheal);

        RemoveOverheal(overhealDamage);

        int healthDamage = damageAmount - overhealDamage;

        if (healthDamage > 0)
        {
            RemoveHealth(healthDamage);
        }
    }
}

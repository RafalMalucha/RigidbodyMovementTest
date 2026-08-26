using UnityEngine;

public class Entity_HealthController : MonoBehaviour
{
    [SerializeField] private int _baseHealth;

    private void EntityDie()
    {
        Destroy(transform.parent.gameObject);
    }
}

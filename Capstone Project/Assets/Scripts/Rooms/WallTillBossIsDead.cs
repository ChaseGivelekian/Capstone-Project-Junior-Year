using UnityEngine;

public class WallTillBossIsDead : MonoBehaviour
{
    [SerializeField] private Health.Health enemy;
    [SerializeField] private Transform thisObject;
    private Health.Health _health;

    private void Awake()
    {
        _health = enemy.GetComponent<Health.Health>();
    }

    private void Update()
    {
        var enemyHealth = _health.currentHealth;
        if (enemyHealth <= 0)
        {
            thisObject.gameObject.SetActive(false);
        }
    }
}

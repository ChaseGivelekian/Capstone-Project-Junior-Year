using UnityEngine;

public class WallTillBossIsDead : MonoBehaviour
{
    [SerializeField] private Health.Health enemy;
    [SerializeField] private Transform thisObject;

    private void Update()
    {
        float enemyHealth = enemy.GetComponent<Health.Health>().currentHealth;
        if (enemyHealth <= 0)
        {
            thisObject.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Animator anim;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRange = .5f;
    [SerializeField] public int attackDamage = 1;
    [SerializeField] public float attackCooldown;
    public LayerMask enemyLayers;
    private float cooldownTimer = Mathf.Infinity;
    public float value;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer > attackCooldown)
        {
            MeleeAttack();
        }
        cooldownTimer += Time.deltaTime;
        // value = GetComponent<PlayerAttack>().manaAmount;
        Debug.Log(value);
    }
    private void MeleeAttack()
    {
        anim.SetTrigger("melee attack");
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);

            if (value < 100)
            {
                // value += 10;
                PlayerAttack playerAttack = GetComponent<PlayerAttack>();
                playerAttack.ManaLevel(10);
                // Debug.Log(value);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}



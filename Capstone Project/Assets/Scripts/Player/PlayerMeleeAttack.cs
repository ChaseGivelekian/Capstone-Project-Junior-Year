using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Animator anim;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRange = .5f;
    [SerializeField] public int attackDamage = 1;
    public LayerMask enemyLayers;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MeleeAttack();
        }
    }
    private void MeleeAttack()
    {
        anim.SetTrigger("melee attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}



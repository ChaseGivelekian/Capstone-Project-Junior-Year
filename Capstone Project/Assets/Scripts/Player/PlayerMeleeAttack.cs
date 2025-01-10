using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private static readonly int Property = Animator.StringToHash("melee attack");
    public Animator anim;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRange = .5f;
    [SerializeField] public int attackDamage = 1;
    [SerializeField] public float attackCooldown;
    [SerializeField] public float maxMana;
    public LayerMask enemyLayers;
    private float _cooldownTimer = Mathf.Infinity;
    private float _value;
    private PlayerAttack _playerAttack;

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _cooldownTimer > attackCooldown)
        {
            MeleeAttack();
        }
        _cooldownTimer += Time.deltaTime;
        _value = _playerAttack.manaAmount;
    }
    private void MeleeAttack()
    {
        anim.SetTrigger(Property);
        _cooldownTimer = 0;

        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Health.Health>().TakeDamage(attackDamage);

            if (!(_value < maxMana)) continue;
            var playerAttack = GetComponent<PlayerAttack>();
            playerAttack.ManaLevel(5, 0);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}



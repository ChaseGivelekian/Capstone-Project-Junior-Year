using Core;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("rangedAttack");

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float _cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip fireballSound;

    //References
    private Animator _anim;
    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        //Attack ony when player is in sight
        if (PlayerInSight())
        {
            if (_cooldownTimer >= attackCooldown)
            {
                _cooldownTimer = 0;
                _anim.SetTrigger(Attack);
            }
        }

        if (_enemyPatrol)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangedAttack()
    {
        SoundManager.Instance.PlaySound(fireballSound);
        _cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindFireball()
    {
        for (var i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private bool PlayerInSight()
    {
        var hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}

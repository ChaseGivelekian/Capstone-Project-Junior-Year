using Core;
using Player;
using UnityEngine;

namespace Enemy.Boss
{
    public class SamuraiBossAttacks : MonoBehaviour
    {
        [Header("Attack Parameters")] [SerializeField]
        private float attackCooldown;

        [SerializeField] private float range;
        [SerializeField] private int damage;

        [Header("Collider Parameters")] [SerializeField]
        private float colliderDistance;

        [SerializeField] private BoxCollider2D boxCollider;

        [Header("Player Layer")] [SerializeField]
        private LayerMask playerLayer;

        private float _cooldownTimer = Mathf.Infinity;

        [Header("Attack Sound")] [SerializeField]
        private AudioClip attackSound;

        //References
        private Animator _anim;
        private PlayerHealth _playerHealth;
        private EnemyPatrol _enemyPatrol;
        private bool _b;
        private bool _b1;

        private void Awake()
        {
            _b1 = PlayerInSight();
            _b = PlayerInSight();
            _anim = GetComponent<Animator>();
            _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;

            //Attack ony when player is in sight
            if (_b)
            {
                if (_cooldownTimer >= attackCooldown && _playerHealth.currentHealth > 0)
                {
                    _cooldownTimer = 0;
                    double randomNum = Random.Range(0, 10);
                    _anim.SetTrigger(randomNum >= 5 ? "meleeAttack1" : "meleeAttack2");

                    SoundManager.Instance.PlaySound(attackSound);
                }
            }

            if (_enemyPatrol)
            {
                _enemyPatrol.enabled = !_b1;
            }
        }

        private bool PlayerInSight()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                Vector2.left, 0, playerLayer);

            if (hit.collider != null)
            {
                _playerHealth = hit.transform.GetComponent<PlayerHealth>();
            }

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }

        private void DamagePlayer()
        {
            //If a player is still in range, damage them
            if (PlayerInSight())
            {
                _playerHealth.TakeDamage(damage);
            }
        }
    }
}
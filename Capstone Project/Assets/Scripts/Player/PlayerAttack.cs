using Core;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private static readonly int Property = Animator.StringToHash("ranged attack");
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject[] fireballs;
        [SerializeField] private AudioClip fireballSound;
        public float manaAmount;
        public float maxMana;

        private Animator _anim;
        private PlayerMovement _playerMovement;
        private float _cooldownTimer = Mathf.Infinity;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
            maxMana = GetComponent<PlayerMeleeAttack>().maxMana;
            manaAmount = maxMana;
        }

        private void Update()
        {
            ManaLevel(0, 0);
            if (Input.GetMouseButton(1) && _cooldownTimer > attackCooldown && _playerMovement.CanAttack() &&
                manaAmount >= 10)
            {
                Attack();
            }

            _cooldownTimer += Time.deltaTime;
        }

        public void ManaLevel(float value, float resetMana)
        {
            manaAmount += value;
            if (resetMana != 0)
            {
                manaAmount = resetMana;
            }

            if (manaAmount < 20)
            {
                manaAmount += Time.deltaTime;
            }
        }

        private void Attack()
        {
            SoundManager.Instance.PlaySound(fireballSound);
            _anim.SetTrigger(Property);
            _cooldownTimer = 0;
            manaAmount -= 10;

            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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
    }
}
using Core;
using UnityEngine;

namespace Traps
{
    public class ArrowTrap : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject[] arrows;
        private float _cooldownTimer;

        [Header("SFX")] [SerializeField] private AudioClip arrowSound;

        private void Attack()
        {
            _cooldownTimer = 0;

            SoundManager.Instance.PlaySound(arrowSound);
            arrows[FindArrow()].transform.position = firePoint.position;
            arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
        }

        private int FindArrow()
        {
            for (var i = 0; i < arrows.Length; i++)
            {
                if (!arrows[i].activeInHierarchy)
                {
                    return i;
                }
            }

            return 0;
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= attackCooldown)
            {
                Attack();
            }
        }
    }
}
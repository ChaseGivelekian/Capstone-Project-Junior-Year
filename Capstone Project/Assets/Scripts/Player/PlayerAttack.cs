using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] public float manaAmount;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        // PlayerMeleeAttack playerMeleeAttack = GetComponent<PlayerMeleeAttack>();
        // manaAmount = playerMeleeAttack.value;
        if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack() && manaAmount >= 10)
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;

        if (manaAmount < 20)
        {
            manaAmount += Time.deltaTime;
        }
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("ranged attack");
        cooldownTimer = 0;
        manaAmount -= 10;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}

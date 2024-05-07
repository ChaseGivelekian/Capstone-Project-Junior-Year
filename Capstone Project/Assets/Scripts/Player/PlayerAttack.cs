using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    public float manaAmount;
    private float maxMana;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        maxMana = GetComponent<PlayerMeleeAttack>().maxMana;
        manaAmount = maxMana;
    }
    private void Update()
    {
        ManaLevel(0);
        if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack() && playerMovement.isGrounded() && manaAmount >= 10)
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
    public void ManaLevel(float value)
    {
        manaAmount += value;
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

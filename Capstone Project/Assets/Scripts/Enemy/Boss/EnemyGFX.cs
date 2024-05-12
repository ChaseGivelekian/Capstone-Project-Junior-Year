using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Rigidbody2D rb;
    public float speed = 200f;
    public float jumpNodeHeightRequirement = .8f;
    public float jumpModifier = .3f;
    public AIPath aiPath;
    public float jumpCheckOffset;
    private bool isGrounded = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        enemy = enemy.GetComponent<Transform>();
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        Debug.Log(isGrounded);
        if (isGrounded)
        {
            if (enemy.position.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        if (aiPath.desiredVelocity.x >= .01f)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (aiPath.desiredVelocity.x <= -.01f)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }
}

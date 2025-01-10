using UnityEngine;
using Pathfinding;

public class EnemyGfx : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Rigidbody2D rb;
    public float speed = 200f;
    public float jumpNodeHeightRequirement = .8f;
    public float jumpModifier = .3f;
    public AIPath aiPath;
    public float jumpCheckOffset;
    private bool _isGrounded;
    private Transform _transform;
    private Collider2D _collider2D;


    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _transform = enemy.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        enemy = _transform;
        _isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, _collider2D.bounds.extents.y + jumpCheckOffset);
        if (_isGrounded)
        {
            if (enemy.position.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * (speed * jumpModifier));
            }
        }

        transform.localScale = aiPath.desiredVelocity.x switch
        {
            >= .01f => new Vector3(3, 3, 3),
            <= -.01f => new Vector3(-3, 3, 3),
            _ => transform.localScale
        };
    }
}

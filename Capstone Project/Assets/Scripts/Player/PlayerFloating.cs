using UnityEngine;

public class PlayerFloating : MonoBehaviour
{
    // public float speed = .01f; // Speed at which the object moves up
    private Animator anim;
    [SerializeField] public Transform target;
    private float speed = 1f;
    private Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        target.GetComponent<BoxCollider2D>().enabled = false;
        target.GetComponent<PlayerMovement>().enabled = false;
        target.GetComponent<Rigidbody2D>().gravityScale = 0;

        Vector2 targetPosition = new Vector2(target.position.x, 10);
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, speed);

        if (transform.position.y > 5.5)
        {
            transform.position = new Vector2(target.position.x, 10);
            Debug.Log("this works");
            target.GetComponent<PlayerFloating>().enabled = false;
        }
    }
    // IEnumerator Floating()
    // {

    // }
}

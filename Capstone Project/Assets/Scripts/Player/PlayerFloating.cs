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
    public void Update()
    {


        if (transform.position.y >= 5.3)
        {
            // transform.position = new Vector2(target.position.x, 10);
            enabled = false;
        }
        else
        {
            target.GetComponent<BoxCollider2D>().enabled = false;
            target.GetComponent<PlayerMovement>().enabled = false;
            target.GetComponent<Rigidbody2D>().gravityScale = 0;

            // Vector2 targetPosition = new Vector2(target.position.x, 10);
            // transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, speed);
            transform.Translate(Vector3.up * (Time.deltaTime * 7));
        }
        Debug.Log("I hope this stops");
    }
}

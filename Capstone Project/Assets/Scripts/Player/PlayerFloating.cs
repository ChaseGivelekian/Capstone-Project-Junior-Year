using UnityEngine;

public class PlayerFloating : MonoBehaviour
{
    [SerializeField] public Transform target;
    private Rigidbody2D body;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (transform.position.y >= 5.3)
        {
            enabled = false;
        }
        else
        {
            target.GetComponent<BoxCollider2D>().enabled = false;
            target.GetComponent<PlayerMovement>().enabled = false;
            target.GetComponent<Rigidbody2D>().gravityScale = 0;
            body.velocity = Vector2.zero;

            transform.Translate(Vector3.up * (Time.deltaTime * 7));
        }
    }
}

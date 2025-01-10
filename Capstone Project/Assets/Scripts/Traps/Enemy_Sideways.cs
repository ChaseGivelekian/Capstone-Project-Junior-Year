using Unity.VisualScripting;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool _movingLeft;
    private float _leftEdge;
    private float _rightEdge;

    private void Awake()
    {
        _leftEdge = transform.position.x - movementDistance;
        _rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                _movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                _movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}

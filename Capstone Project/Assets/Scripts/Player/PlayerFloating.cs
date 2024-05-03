using UnityEngine;

public class PlayerFloating : MonoBehaviour
{
    [SerializeField] public Transform target;
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

            transform.Translate(Vector3.up * (Time.deltaTime * 7));
        }
    }
}

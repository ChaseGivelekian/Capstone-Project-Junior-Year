using UnityEngine;

public class FlowerDeath : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetTrigger("flowerDeath");
        }
    }
}

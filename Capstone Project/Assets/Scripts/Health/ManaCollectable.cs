using UnityEngine;

public class ManaCollectable : MonoBehaviour
{
    [SerializeField] private float manaValue;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<PlayerMeleeAttack>().maxMana += manaValue;
            collision.GetComponent<PlayerAttack>().manaAmount = collision.GetComponent<PlayerMeleeAttack>().maxMana;
            gameObject.SetActive(false);
        }
    }
}

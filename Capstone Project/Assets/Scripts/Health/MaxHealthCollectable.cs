using Core;
using UnityEngine;

namespace Health
{
    public class MaxHealthCollectable : MonoBehaviour
    {
        [SerializeField] private float healthValue;
        [SerializeField] private AudioClip pickupSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            SoundManager.Instance.PlaySound(pickupSound);
            collision.GetComponent<PlayerHealth>().startingHealth += 1;
            collision.GetComponent<PlayerHealth>().currentHealth = collision.GetComponent<PlayerHealth>().startingHealth;
            gameObject.SetActive(false);
        }
    }
}

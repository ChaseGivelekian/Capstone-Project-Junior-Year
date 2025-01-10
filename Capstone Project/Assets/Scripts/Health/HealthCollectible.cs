using Core;
using UnityEngine;

namespace Health
{
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField] private float healthValue;
        [SerializeField] private AudioClip pickupSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            SoundManager.Instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}

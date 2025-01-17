using Core;
using Player;
using UnityEngine;

namespace Health
{
    public class ManaCollectable : MonoBehaviour
    {
        [SerializeField] private float manaValue;
        [SerializeField] private AudioClip pickupSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            SoundManager.Instance.PlaySound(pickupSound);
            collision.GetComponent<PlayerMeleeAttack>().maxMana += manaValue;
            collision.GetComponent<PlayerAttack>().manaAmount = collision.GetComponent<PlayerMeleeAttack>().maxMana;
            gameObject.SetActive(false);
        }
    }
}
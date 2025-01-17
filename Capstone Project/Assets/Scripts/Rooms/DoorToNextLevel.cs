using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rooms
{
    public class DoorToNextLevel : MonoBehaviour
    {
        [SerializeField] private PlayerHealth player;
        private float _persistantHealth;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _persistantHealth = player.GetComponent<PlayerHealth>().startingHealth;
            PlayerPrefs.SetFloat("persistantHealth", _persistantHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
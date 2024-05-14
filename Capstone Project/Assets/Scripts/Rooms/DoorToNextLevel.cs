using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{
    [SerializeField] private Health player;
    private float persistantHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            persistantHealth = player.GetComponent<Health>().startingHealth;
            PlayerPrefs.SetFloat("persistantHealth", persistantHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

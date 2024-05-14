using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToPreviousLevel : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    private float persistantHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            persistantHealth = player.GetComponent<PlayerHealth>().startingHealth;
            PlayerPrefs.SetFloat("persistantHealth", persistantHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}

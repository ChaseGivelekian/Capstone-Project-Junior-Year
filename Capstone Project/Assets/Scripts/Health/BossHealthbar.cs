using UnityEngine.UI;
using UnityEngine;

public class BossHealthbar : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform player;
    [SerializeField] private Health bossHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Update()
    {
        totalhealthBar.fillAmount = bossHealth.startingHealth / 20;
        currenthealthBar.fillAmount = bossHealth.currentHealth / 20;

        player = player.GetComponent<Transform>();
        door = door.GetComponent<Transform>();

        if (bossHealth.currentHealth <= 0 || player.position.x <= door.position.x)
        {
            totalhealthBar.enabled = false;
            currenthealthBar.enabled = false;
        }
        else
        {
            totalhealthBar.enabled = true;
            currenthealthBar.enabled = true;
        }
    }
}

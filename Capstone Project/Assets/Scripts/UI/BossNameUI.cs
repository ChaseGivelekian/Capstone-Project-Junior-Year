using UnityEngine;
using UnityEngine.UI;

public class BossNameUI : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform player;
    [SerializeField] private Health bossHealth;
    [SerializeField] private Text bossName;

    private void Update()
    {
        player = player.GetComponent<Transform>();
        door = door.GetComponent<Transform>();

        if (bossHealth.currentHealth <= 0 || player.position.x <= door.position.x)
        {
            bossName.enabled = false;
        }
        else
        {
            bossName.enabled = true;
        }
    }
}

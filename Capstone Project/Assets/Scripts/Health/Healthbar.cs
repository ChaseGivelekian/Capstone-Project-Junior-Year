using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Update()
    {
        totalhealthBar.fillAmount = playerHealth.startingHealth / 10;
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}

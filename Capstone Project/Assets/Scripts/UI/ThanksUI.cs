using UnityEngine;
using UnityEngine.UI;

public class ThanksUI : MonoBehaviour
{
    [SerializeField] private Health bossHealth;
    [SerializeField] private Text thanksUI;

    void Update()
    {
        if (bossHealth.currentHealth <= 0)
        {
            thanksUI.enabled = true;
        }
        else
        {
            thanksUI.enabled = false;
        }
    }
}

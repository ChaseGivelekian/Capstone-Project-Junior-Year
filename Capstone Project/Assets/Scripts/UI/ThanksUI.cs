using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ThanksUI : MonoBehaviour
    {
        [SerializeField] private Health.Health bossHealth;
        [SerializeField] private Text thanksUI;

        private void Update()
        {
            thanksUI.enabled = bossHealth.currentHealth <= 0;
        }
    }
}
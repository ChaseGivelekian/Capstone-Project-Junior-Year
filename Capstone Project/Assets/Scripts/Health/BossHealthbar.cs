using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class BossHealthbar : MonoBehaviour
    {
        [SerializeField] private Transform door;
        [SerializeField] private Transform player;
        [SerializeField] private Health bossHealth;
        [SerializeField] private Image totalhealthBar;
        [SerializeField] private Image currenthealthBar;
        [SerializeField] private float value;
        private Transform _transform;
        private Transform _transform1;

        private void Awake()
        {
            _transform1 = door.GetComponent<Transform>();
            _transform = player.GetComponent<Transform>();
        }

        private void Update()
        {
            totalhealthBar.fillAmount = bossHealth.startingHealth / value;
            currenthealthBar.fillAmount = bossHealth.currentHealth / value;

            player = _transform;
            door = _transform1;

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
}

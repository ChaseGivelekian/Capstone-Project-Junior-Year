using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BossNameUI : MonoBehaviour
    {
        [SerializeField] private Transform door;
        [SerializeField] private Transform player;
        [SerializeField] private Health.Health bossHealth;
        [SerializeField] private Text bossName;
        private Transform _transform;
        private Transform _transform1;

        private void Awake()
        {
            _transform1 = door.GetComponent<Transform>();
            _transform = player.GetComponent<Transform>();
        }

        private void Update()
        {
            player = _transform;
            door = _transform1;

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
}
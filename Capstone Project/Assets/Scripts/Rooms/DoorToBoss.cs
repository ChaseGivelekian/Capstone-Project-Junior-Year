using UnityEngine;

namespace Rooms
{
    public class DoorToBoss : MonoBehaviour
    {
        [SerializeField] private Transform door;
        [SerializeField] private Transform player;
        [SerializeField] public Camera defaultCam;
        [SerializeField] public Camera bossCam;
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
            if (player.position.x > door.position.x)
            {
                defaultCam.enabled = false;
                bossCam.enabled = true;
            }
            else
            {
                defaultCam.enabled = true;
                bossCam.enabled = false;
            }
        }
    }
}
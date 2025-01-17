using UnityEngine;

namespace Core
{
    public class CameraController : MonoBehaviour
    {
        //Room camera
        [SerializeField] private float speed;
        private float _currentPosX;
        private Vector3 _velocity = Vector3.zero;

        //Follow player
        /*[SerializeField] private Transform player;
        [SerializeField] private float aheadDistance;
        [SerializeField] private float cameraSpeed;
        private float lookAhead; */

        private void Update()
        {
            //Room camera
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(_currentPosX, transform.position.y, transform.position.z), ref _velocity, speed);

            //Follow player
            //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
            //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        }

        public void MoveToNewRoom(Transform newRoom)
        {
            _currentPosX = newRoom.position.x;
        }
    }
}
using Core;
using Rooms;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Appear = Animator.StringToHash("appear");
        [SerializeField] private AudioClip checkpointSound; //Sound that plays when getting a new checkpoint
        [SerializeField] public Transform target;
        [SerializeField] public Health.Health[] enemiesHealth;
        private Transform _currentCheckpoint; //Stores the last checkpoint here
        private PlayerHealth _playerHealth;
        private UIManager _uiManager;
        private Animator _anim;
        public CameraController cameraController;
        public DoorToBoss doorToBoss;
        private PlayerFloating _playerFloating;
        private BoxCollider2D _boxCollider2D;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = target.GetComponent<PlayerMovement>();
            _boxCollider2D = target.GetComponent<BoxCollider2D>();
            _playerFloating = target.GetComponent<PlayerFloating>();
            _playerHealth = GetComponent<PlayerHealth>();
            _uiManager = FindObjectOfType<UIManager>();
            target.GetComponent<PlayerFloating>().enabled = false;
            _anim = GetComponent<Animator>();
            if (Camera.main != null) cameraController = Camera.main.GetComponent<CameraController>();

            // doorToBoss = doorToBoss.GetComponent<DoorToBoss>().bossCam;
        }

        private void Update()
        {
            var triggerValue = _anim.GetBool(Die);
            if (triggerValue)
            {
                _playerFloating.enabled = true;
            }
            else
            {
                _boxCollider2D.enabled = true;
                _playerMovement.enabled = true;
            }

            if (transform.position.y >= 5.38)
            {
                CheckRespawn();
            }
        }

        public void CheckRespawn()
        {
            //Check if check point available
            if (!_currentCheckpoint)
            {
                //Show game over screen
                _uiManager.GameOver();

                return; //Don't execute the rest of this function
            }

            foreach (var enemy in enemiesHealth)
            {
                enemy.GetComponent<Health.Health>().currentHealth = enemy.GetComponent<Health.Health>().startingHealth;
            }

            transform.position = _currentCheckpoint.position; //Move player to checkpoint position
            _playerHealth.Respawn(); //Restore player health and reset animation

            //Move camera to checkpoint room (**for this to work the checkpoint objects have to be placed as a child of the room object)

            if (doorToBoss)
            {
                if (doorToBoss.GetComponent<DoorToBoss>().defaultCam.enabled != true) return;
                cameraController.MoveToNewRoom(_currentCheckpoint.parent);
            }
            else
            {
                cameraController.MoveToNewRoom(_currentCheckpoint.parent);
            }
        }

        //Activate checkpoints
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Checkpoint")) return;
            _currentCheckpoint = collision.transform; //Store the checkpoint that we activated as the current one
            SoundManager.Instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger(Appear); //Trigger checkpoint animation
        }
    }
}
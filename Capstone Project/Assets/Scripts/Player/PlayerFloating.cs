using UnityEngine;

namespace Player
{
    public class PlayerFloating : MonoBehaviour
    {
        [SerializeField] public Transform target;
        private Rigidbody2D _body;
        private BoxCollider2D _boxCollider2D;
        private PlayerMovement _playerMovement;
        private Rigidbody2D _rigidbody2D;

        public void Awake()
        {
            _rigidbody2D = target.GetComponent<Rigidbody2D>();
            _playerMovement = target.GetComponent<PlayerMovement>();
            _boxCollider2D = target.GetComponent<BoxCollider2D>();
            _body = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            if (transform.position.y >= 5.3)
            {
                enabled = false;
            }
            else
            {
                _boxCollider2D.enabled = false;
                _playerMovement.enabled = false;
                _rigidbody2D.gravityScale = 0;
                _body.velocity = Vector2.zero;

                transform.Translate(Vector3.up * (Time.deltaTime * 7));
            }
        }
    }
}
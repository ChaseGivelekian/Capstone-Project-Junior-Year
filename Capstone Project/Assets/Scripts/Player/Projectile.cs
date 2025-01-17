using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        private static readonly int Explode = Animator.StringToHash("explode");
        [SerializeField] private float speed;
        private float _direction;
        private bool _hit;
        private float _lifetime;

        private Animator _anim;
        private BoxCollider2D _boxCollider;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (_hit) return;

            var movementSpeed = speed * Time.deltaTime * _direction;
            transform.Translate(movementSpeed, 0, 0);

            _lifetime += Time.deltaTime;
            if (_lifetime > 5) gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _hit = true;
            _boxCollider.enabled = false;
            _anim.SetTrigger(Explode);

            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Health.Health>().TakeDamage(1);
            }
        }

        public void SetDirection(float direction)
        {
            _lifetime = 0;
            _direction = direction;
            gameObject.SetActive(true);
            _hit = false;
            _boxCollider.enabled = true;

            var localScaleX = transform.localScale.x;

            if (!Mathf.Approximately(Mathf.Sign(localScaleX), direction))
            {
                localScaleX = -localScaleX;
            }

            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
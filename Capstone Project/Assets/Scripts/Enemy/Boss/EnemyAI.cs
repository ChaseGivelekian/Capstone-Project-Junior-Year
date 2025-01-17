using Pathfinding;
using UnityEngine;

namespace Enemy.Boss
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Pathfinding")] public Transform target;
        public float activateDistance = 50f;
        public float pathUpdateSeconds = 0.5f;
        private Collider2D _coll;
        [Header("Physics")] public float speed = 200f;
        public float nextWaypointDistance = 3f;
        public float jumpNodeHeightRequirement = 0.8f;
        public float jumpModifier = 0.3f;
        public float jumpCooldown = 1f;
        [Header("Custom Behavior")] public bool followEnabled = true;
        public bool jumpEnabled = true;
        public bool directionLookEnabled = true;
        [SerializeField] private LayerMask jumpableGround;
        private Path _path;
        private int _currentWaypoint;

        private Seeker _seeker;
        private Rigidbody2D _rb;

        public void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
            InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateSeconds);
            GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (TargetInDistance() && followEnabled)
            {
                PathFollow();
            }
        }

        private void UpdatePath()
        {
            if (followEnabled && TargetInDistance() && _seeker.IsDone())
            {
                _seeker.StartPath(_rb.position, target.position, OnPathComplete);
            }
        }

        private void PathFollow()
        {
            if (_path == null)
            {
                return;
            }

            //Reached the end of the path
            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            //See if colliding with anything
            bool isGrounded = Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0, Vector2.down, 0.1f,
                jumpableGround);

            //Direction Calculation
            var direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
            _ = direction * (speed * Time.deltaTime);

            //Jump
            if (jumpEnabled && isGrounded && jumpCooldown <= 0)
            {
                if (direction.y > jumpNodeHeightRequirement)
                {
                    _rb.AddForce(Vector2.up * (speed * jumpModifier));
                    jumpCooldown = 3f;
                }
            }
            else
            {
                jumpCooldown -= Time.deltaTime;
            }

            //Movement
            _rb.AddForce(Vector2.right * direction, ForceMode2D.Impulse);

            if (_rb.velocity.x > speed)
            {
                _rb.velocity = new Vector2(speed, _rb.velocity.y);
            }
            else if (_rb.velocity.x < speed * -1)
            {
                _rb.velocity = new Vector2(speed * -1, _rb.velocity.y);
            }

            //NextWaypoint
            var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                _currentWaypoint++;
            }

            //Direction Graphics Handling
            if (!directionLookEnabled) return;
            transform.localScale = _rb.velocity.x switch
            {
                > 0.05f => new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z),
                < -0.05f => new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z),
                _ => transform.localScale
            };
        }

        private bool TargetInDistance()
        {
            return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;
            _path = p;

            _currentWaypoint = 0;
        }
    }
}
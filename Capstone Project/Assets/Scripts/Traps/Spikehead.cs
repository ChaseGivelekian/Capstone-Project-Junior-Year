using Core;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private readonly Vector3[] _directions = new Vector3[4];
    private Vector3 _destination;
    private float _checkTimer;
    private bool _attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //Move spikehead to destination only if attacking
        if (_attacking)
        {
            transform.Translate(_destination * (Time.deltaTime * speed));
        }
        else
        {
            _checkTimer += Time.deltaTime;
            if (_checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Check if spikehead sees player if all 4 directions
        foreach (var t in _directions)
        {
            Debug.DrawRay(transform.position, t, Color.red);
            var hit = Physics2D.Raycast(transform.position, t, range, playerLayer);

            if (!hit.collider || _attacking) continue;
            _attacking = true;
            _destination = t;
            _checkTimer = 0;
        }
    }
    private void CalculateDirections()
    {
        _directions[0] = transform.right * range; //Right direction
        _directions[1] = -transform.right * range; //Left direction
        _directions[2] = transform.up * range; //Up direction
        _directions[3] = -transform.up * range; //Down direction
    }
    private void Stop()
    {
        _destination = transform.position; //Set destination as current position so it doesn't move
        _attacking = false;
    }
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop(); //Stop spikehead once he hits something
    }
}

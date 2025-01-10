using UnityEngine;
using System.Collections;
using Core;

public class Firetrap : MonoBehaviour
{
    private static readonly int Activated = Animator.StringToHash("activated");
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator _anim;
    private SpriteRenderer _spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool _triggered; //when the trap gets triggered
    private bool _active; //when the trap is active and can hurt the player

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_playerHealth && _active)
            _playerHealth.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        _playerHealth = collision.GetComponent<PlayerHealth>();

        if (!_triggered)
            StartCoroutine(ActivateFiretrap());

        if (_active)
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _playerHealth = null;
    }
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        _triggered = true;
        _spriteRend.color = Color.red;

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.Instance.PlaySound(firetrapSound);
        _spriteRend.color = Color.white; //turn the sprite back to its initial color
        _active = true;
        _anim.SetBool(Activated, true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        _active = false;
        _triggered = false;
        _anim.SetBool(Activated, false);
    }
}
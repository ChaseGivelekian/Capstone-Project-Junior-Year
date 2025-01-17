using System.Collections;
using Core;
using UnityEngine;

namespace Health
{
    public class Health : MonoBehaviour
    {
        private static readonly int Hurt = Animator.StringToHash("hurt");
        private static readonly int Die = Animator.StringToHash("die");

        [Header("Health")] [SerializeField] public float startingHealth;
        public float currentHealth;
        private Animator _anim;
        private bool _dead;

        [Header("iFrames")] [SerializeField] private float iFramesDuration;
        [SerializeField] private int numberOfFlashes;
        private SpriteRenderer _spriteRend;

        [Header("Components")] [SerializeField]
        private Behaviour[] components;

        [Header("Death Sound")] [SerializeField]
        private AudioClip deathSound;

        [SerializeField] private AudioClip hurtSound;

        private void Awake()
        {
            currentHealth = startingHealth;
            _anim = GetComponent<Animator>();
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

            if (currentHealth > 0)
            {
                _anim.SetTrigger(Hurt);
                StartCoroutine(Invulnerability());
                // transform.Translate(Vector2.left * transform.localScale.x, transform.position)
                SoundManager.Instance.PlaySound(hurtSound);
            }
            else
            {
                if (_dead) return;
                //Deactivate all attached component classes
                foreach (var component in components)
                {
                    component.enabled = false;
                }

                // anim.SetBool("grounded", true);
                _anim.SetTrigger(Die);

                _dead = true;
                SoundManager.Instance.PlaySound(deathSound);
            }
        }

        public void AddHealth(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
        }

        public void Respawn()
        {
            AddHealth(startingHealth);
            _anim.ResetTrigger("die");
            _anim.Play("Idle");
            _dead = false;

            //Deactivate all attached component classes
            foreach (var component in components)
            {
                component.enabled = true;
            }
        }

        private IEnumerator Invulnerability()
        {
            Physics2D.IgnoreLayerCollision(10, 11, true);
            for (var i = 0; i < numberOfFlashes; i++)
            {
                _spriteRend.color = new Color(1, 0, 0, .5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                _spriteRend.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }

            Physics2D.IgnoreLayerCollision(10, 11, false);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Flower
{
    public class FlowerDeath : MonoBehaviour
    {
        private static readonly int Death = Animator.StringToHash("flowerDeath");
        [SerializeField] private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            StartCoroutine(FadeAlphaToZero(GetComponent<SpriteRenderer>(), 2f));
            anim.SetTrigger(Death);
        }

        private static IEnumerator FadeAlphaToZero(SpriteRenderer renderer, float duration)
        {
            var startColor = renderer.color;
            var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                renderer.color = Color.Lerp(startColor, endColor, time / duration);
                yield return null;
            }
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
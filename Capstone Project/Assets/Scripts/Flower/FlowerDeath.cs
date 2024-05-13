using System.Collections;
using UnityEngine;

public class FlowerDeath : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FadeAlphaToZero(GetComponent<SpriteRenderer>(), 2f));
            anim.SetTrigger("flowerDeath");
        }
    }
    IEnumerator FadeAlphaToZero(SpriteRenderer renderer, float duration)
    {
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
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

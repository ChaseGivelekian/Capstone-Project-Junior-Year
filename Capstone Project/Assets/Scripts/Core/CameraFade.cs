using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CameraFade : MonoBehaviour
    {
        [SerializeField] public Image fadeImage;
        [SerializeField] public float fadeDuration = 1f;
        private bool _isFading;
        private float _fadeTimer;

        private void Start()
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = Color.black;
            StartFadeIn();
        }

        private void Update()
        {
            if (!_isFading) return;
            _fadeTimer += Time.deltaTime;
            var alpha = Mathf.Clamp01(_fadeTimer / fadeDuration);

            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            if (!(_fadeTimer >= fadeDuration)) return;
            _isFading = false;
            fadeImage.gameObject.SetActive(false);
        }

        private void StartFadeIn()
        {
            fadeImage.gameObject.SetActive(true);
            _fadeTimer = 0f;
            _isFading = true;
        }

        public void StartFadeOut()
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
            _fadeTimer = 0f;
            _isFading = true;
        }
    }
}

// do an on scene load for the fading to black
using UnityEngine;

namespace Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        private AudioSource _soundSource;
        private AudioSource _musicSource;

        private void Awake()
        {
            _soundSource = GetComponent<AudioSource>();
            _musicSource = transform.GetChild(0).GetComponent<AudioSource>();

            //Keep this object even when we go to a new scene
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            //Destroy duplicate game objects
            else if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            //Assign initial volumes
            ChangeMusicVolume(0);
            ChangeSoundVolume(0);
        }

        public void PlaySound(AudioClip sound)
        {
            _soundSource.PlayOneShot(sound);
        }

        public void ChangeSoundVolume(float change)
        {
            ChangeSourceVolume(1, "soundVolume", change, _soundSource);
        }

        public void ChangeMusicVolume(float change)
        {
            ChangeSourceVolume(.3f, "musicVolume", change, _musicSource);
        }

        private static void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
        {
            //Get the initial value of volume and change it
            var currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
            currentVolume += change;

            //Check if we reached the maximum or minimum value
            if (currentVolume > 1.01)
            {
                currentVolume = 0;
            }
            else if (currentVolume < 0)
            {
                currentVolume = 1;
            }

            //Assign final value
            var finalVolume = currentVolume * baseVolume;
            source.volume = finalVolume;

            //Save final value to player prefs
            PlayerPrefs.SetFloat(volumeName, currentVolume);
        }
    }
}
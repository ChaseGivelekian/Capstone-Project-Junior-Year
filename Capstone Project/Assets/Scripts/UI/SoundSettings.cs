using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer masterMixer;

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    private void SetVolume(float value)
    {
        if (value < 1)
        {
            value = .001f;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }

    private void RefreshSlider(float value)
    {
        soundSlider.value = value;
    }
}

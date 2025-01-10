using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; //Sound: or Music:
    private Text _txt;

    private void Awake()
    {
        _txt = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        var volumeValue = Mathf.Floor(PlayerPrefs.GetFloat(volumeName) * 100);
        _txt.text = textIntro + volumeValue;
    }
}

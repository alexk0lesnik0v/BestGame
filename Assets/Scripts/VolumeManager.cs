using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public TextMeshPro volumeText;
    public AudioMixer audioMixer;
    public string volumeParameter = "MasterVolume";
    public float volumeStep = 0.1f;
    private float currentVolume = 1.0f;

    void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("LinearVolume", 1.0f);
        float decibelVolume = Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat(volumeParameter, decibelVolume);
        UpdateVolumeText();
    }

    public void DecreaseVolume()
    {
        currentVolume -= volumeStep;
        if (currentVolume < 0)
            currentVolume = 0;
        float decibelVolume = Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat(volumeParameter, decibelVolume);
        UpdateVolumeText();
    }

    public void IncreaseVolume()
    {
        currentVolume += volumeStep;
        if (currentVolume > 1)
            currentVolume = 1;
        float decibelVolume = Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat(volumeParameter, decibelVolume);
        UpdateVolumeText();
    }

    private void UpdateVolumeText()
    {
        int volumePercentage = Mathf.RoundToInt(currentVolume * 100);
        volumeText.text = $"Громкость: {volumePercentage}%";
        PlayerPrefs.SetFloat("LinearVolume", currentVolume);
    }
}

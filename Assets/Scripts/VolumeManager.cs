using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public TextMeshPro volumeText;
    public AudioMixer audioMixer;
    public string volumeParameter = "MasterVolume";
    public float volumeStep = 0.1f;
    private float m_currentVolume = 1.0f;

    void Start()
    {
        m_currentVolume = PlayerPrefs.GetFloat("LinearVolume", 1.0f);
        float decibelVolume = Mathf.Log10(m_currentVolume) * 20;
        audioMixer.SetFloat(volumeParameter, decibelVolume);
        UpdateVolumeText();
    }

    public void DecreaseVolume()
    {
        m_currentVolume -= volumeStep;
        if (m_currentVolume < 0)
            m_currentVolume = 0;
        float decibelVolume = Mathf.Log10(m_currentVolume) * 20;
        audioMixer.SetFloat(volumeParameter, decibelVolume);
        UpdateVolumeText();
    }

    public void IncreaseVolume()
    {
        m_currentVolume += volumeStep;
        if (m_currentVolume > 1)
            m_currentVolume = 1;
        float decibelVolume = Mathf.Log10(m_currentVolume) * 20;
        audioMixer.SetFloat(volumeParameter, decibelVolume);
        UpdateVolumeText();
    }

    private void UpdateVolumeText()
    {
        int volumePercentage = Mathf.RoundToInt(m_currentVolume * 100);
        volumeText.text = $"Громкость: {volumePercentage}%";
        PlayerPrefs.SetFloat("LinearVolume", m_currentVolume);
    }
}

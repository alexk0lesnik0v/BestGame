using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TextMeshPro resolutionText;
    public int currentResolutionIndex = 0;
    private Resolution[] m_resolutions;

    void Start()
    {
        m_resolutions = Screen.resolutions;
        UpdateResolutionText();
    }

    public void PreviousResolution()
    {
        currentResolutionIndex--;
        if (currentResolutionIndex < 0)
            currentResolutionIndex = m_resolutions.Length - 1;
        UpdateResolutionText();
    }

    public void NextResolution()
    {
        currentResolutionIndex++;
        if (currentResolutionIndex >= m_resolutions.Length)
            currentResolutionIndex = 0;
        UpdateResolutionText();
    }

    public void ApplyResolution()
    {
        Resolution resolution = m_resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void UpdateResolutionText()
    {
        Resolution resolution = m_resolutions[currentResolutionIndex];
        resolutionText.text = $"{resolution.width}x{resolution.height}";
    }
}

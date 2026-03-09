using UnityEngine;
using UnityEngine.Audio;

public class GlobalAudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string volumeParameter = "MasterVolume";

    void Awake()
    {
        if (FindObjectsOfType<GlobalAudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0f);
        audioMixer.SetFloat(volumeParameter, savedVolume);
    }
}

using UnityEngine;

public class WallTorch : MonoBehaviour
{
    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioClip m_clip;

    void Start()
    {
        m_source.clip = m_clip;
        m_source.Play();
    }
}

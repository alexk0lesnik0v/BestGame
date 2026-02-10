using UnityEngine;

public class WallTorch : MonoBehaviour
{
    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioClip m_clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_source.PlayOneShot(m_clip);
    }


}

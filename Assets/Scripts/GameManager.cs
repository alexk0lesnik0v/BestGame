using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioClip m_clip;
    void Start()
    {
        m_source.PlayOneShot(m_clip);
    }
}

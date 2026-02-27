using Players;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float m_spikeDamage;
    [SerializeField] private AudioClip m_audioClip;
    [SerializeField] private AudioSource m_source;
    
    private Animation m_animation;

    private void Start()
    {
        m_animation = GetComponent<Animation>();
        m_animation.Play("Spike");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            m_animation.Play("Spike");
            m_source.PlayOneShot(m_audioClip);
            player.TakeDamage(m_spikeDamage);
            Debug.Log($"Your current health: {player.health}");
        }
        
    }
}

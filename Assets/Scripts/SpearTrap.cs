using Players;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private Spear m_spear;
    [SerializeField] private float m_spearDamage;
    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioClip m_clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            m_spear.IsActive();
            m_source.PlayOneShot(m_clip);
            player.TakeDamage(m_spearDamage);
            Debug.Log($"Your current health: {player.health}");
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        m_spear.IsNoActive();
    }
}

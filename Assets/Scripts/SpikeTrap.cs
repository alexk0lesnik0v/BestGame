using Players;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float m_spikeDamage;
    
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
            
            player.TakeDamage(m_spikeDamage);
            Debug.Log($"������� ����, ������� �� {player.health}");
        }
    }
}

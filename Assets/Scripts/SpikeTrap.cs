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
        
        /* if (other.CompareTag("Player"))
        {
            m_animation.Play("Spike");
            Player playerHealth = other.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_spikeDamage);
                Debug.Log($"������� ����, ������� �� {playerHealth.Health}");
            }

        }*/
    }
}

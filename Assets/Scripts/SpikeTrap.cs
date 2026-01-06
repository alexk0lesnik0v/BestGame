using Players;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float m_spikeDamage;
    [SerializeField] private Animation m_animation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_animation.Play("Spike");
            Player playerHealth = other.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_spikeDamage);
                Debug.Log($"Получен урон, текущее хп {playerHealth.Health}");
            }

        }
    }
}

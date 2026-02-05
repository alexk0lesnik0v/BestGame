using Players;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private Spear m_spear;
    [SerializeField] private float m_spearDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            m_spear.IsActive();
            
            player.TakeDamage(m_spearDamage);
            Debug.Log($"������� ����, ������� �� {player.health}");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        m_spear.IsNoActive();
    }
}

using Players;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private Spear m_spear;
    [SerializeField] private float m_spearDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_spear.IsActive();
            Player playerHealth = other.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_spearDamage);
                Debug.Log($"Нанесен урон, текущее хп {playerHealth.Health}");
            }
     
        }
    }

}

using Players;
using UnityEngine;

namespace Enemies
{
    public class GostTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject m_gost;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_gost.SetActive(true);
                
                Destroy(this.gameObject, 1f);
            }
        }
    }
}
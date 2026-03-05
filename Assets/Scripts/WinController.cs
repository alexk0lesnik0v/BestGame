using Players;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinController :  MonoBehaviour
    {
        [SerializeField] private DoorInteraction m_doorInteraction;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_doorInteraction.OpenDoorUp();
            }
        }
    }
}
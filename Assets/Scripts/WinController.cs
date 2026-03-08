using Players;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinController :  MonoBehaviour
    {
        [SerializeField] private DoorInteraction m_doorInteraction;
        [SerializeField] private Timer m_timer;
        [SerializeField] private float m_time;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_doorInteraction.OpenDoorUp();
        
                m_timer.RestartTimer(m_time);
            }
        }
    }
}
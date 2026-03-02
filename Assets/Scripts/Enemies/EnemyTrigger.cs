using System;
using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyTrigger : MonoBehaviour
    {
        public event Action PlayerTriggered;
        
        private Player m_player;

        private void Start()
        {
            m_player  = FindAnyObjectByType<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                PlayerTriggered?.Invoke();
            }
        }
    }
}
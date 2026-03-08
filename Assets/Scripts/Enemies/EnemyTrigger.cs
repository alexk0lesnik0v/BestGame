using System;
using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyTrigger : MonoBehaviour
    {
        public event Action PlayerTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                PlayerTriggered?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}
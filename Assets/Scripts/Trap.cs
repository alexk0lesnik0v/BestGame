using Players;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damageAmount = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerHealth = other.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log($"Получен урон, текущее хп {playerHealth.Health}");
            }

        }
    }
}

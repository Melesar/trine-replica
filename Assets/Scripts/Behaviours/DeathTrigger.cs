using AI;
using UnityEngine;

namespace Behaviours
{
    public class DeathTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var health = other.GetComponent<HealthBehaviour>();
            health?.Die();
        }
    }
}
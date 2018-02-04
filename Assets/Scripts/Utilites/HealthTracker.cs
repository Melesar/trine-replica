using UnityEngine;
using Framework.Data;
using AI;

namespace Utilites
{
    public class HealthTracker : MonoBehaviour, IDamageReceiver, IDeathListener
    {
        public IntVariable characterHealth;

        void IDamageReceiver.OnDamageTaken(int damageTaken, int currentHealth)
        {
            characterHealth.Value = currentHealth;    
        }

        private void Start()
        {
            var healthBehaviour = GetComponent<HealthBehaviour>();
            if (healthBehaviour != null) {
                characterHealth.Value = healthBehaviour.healthStats.initialHealth;
            }
        }

        public void OnDeath()
        {
            characterHealth.Value = 0;
        }
    }
}
using UnityEngine;
using Stats;
using Framework.Events;
using Framework.References;
using UnityEngine.EventSystems;

namespace AI
{
    public class HealthBehaviour : MonoBehaviour
    {
        public HealthStats healthStats;
        public GameObjectEvent onDeath;

        public StringReference damageTakenParamName;
        public StringReference deathParamName;

        public int Health { get; private set; }

        public bool IsDead => Health == 0;

        private Animator animator;
        private EventSystem eventSystem;

        public void TakeDamage(int damage)
        {
            if (IsDead) {
                return;
            }
            
            Health = Mathf.Max(Health - damage, 0);
            SendDamageTaken(damage, Health);

            if (Health == 0) {
                Die();
                return;
            }

            animator.SetTrigger(damageTakenParamName);
        }

        public void Die()
        {
            enabled = false;
            animator.SetTrigger(deathParamName);
            SendDeath();
            onDeath?.Raise(gameObject);
        }

        private void SendDamageTaken(int damageTaken, int currentHealth)
        {
            ExecuteEvents.Execute<IDamageReceiver>(gameObject, new BaseEventData(eventSystem), 
                (listener, data) => listener.OnDamageTaken(damageTaken, currentHealth));
        }

        private void SendDeath()
        {
            ExecuteEvents.Execute<IDeathListener>(gameObject, new BaseEventData(eventSystem), 
                (listener, data) => listener.OnDeath());
        }

        private void Start()
        {
            Health = healthStats.initialHealth;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            eventSystem = EventSystem.current;
        }
    }
}
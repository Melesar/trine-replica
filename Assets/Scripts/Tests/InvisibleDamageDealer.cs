using UnityEngine;
using UnityEngine.EventSystems;
using AI;

namespace Tests
{
    [RequireComponent(typeof(HealthBehaviour))]
    public class InvisibleDamageDealer : MonoBehaviour, IPointerClickHandler
    {
        public int damageToDeal;

        private HealthBehaviour healthBehaviour;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            healthBehaviour.TakeDamage(damageToDeal);
        }

        private void Start()
        {

        }

        private void Awake()
        {
            healthBehaviour = GetComponent<HealthBehaviour>();
        }
    }
}
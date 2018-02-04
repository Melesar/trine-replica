using System;
using Framework.Data;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image healthBar;
        
        public HealthStats healthStats;
        public IntVariable healthAmount;

        private void UpdateHealthBar(int currentHealth)
        {
            if (healthBar == null) {
                return;
            }

            var maxHealth = healthStats.initialHealth;
            healthBar.fillAmount = (float) currentHealth / maxHealth;
        }

        private void Start()
        {
            UpdateHealthBar(healthAmount.Value);
        }

        private void Awake()
        {
            healthAmount.valueChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int oldHealth, int newHealth)
        {
            UpdateHealthBar(newHealth);
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDamageReceiver : IEventSystemHandler
{
    void OnDamageTaken (int damageTaken, int currentHealth);
}
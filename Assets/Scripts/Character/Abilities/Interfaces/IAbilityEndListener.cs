using UnityEngine.EventSystems;

namespace Character.Abilities
{
    public interface IAbilityEndListener : IEventSystemHandler
    {
        void OnAbilityEnd();
    }
}
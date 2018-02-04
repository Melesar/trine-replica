using UnityEngine.EventSystems;

namespace Character.Abilities
{
    public interface IAbilityStartListener : IEventSystemHandler
    {
        void OnAbilityStart();

    }
}
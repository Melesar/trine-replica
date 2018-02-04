using UnityEngine.EventSystems;

namespace AI
{
    public interface IPlayerMissedListener : IEventSystemHandler
    {
        void OnPlayerMissed();
    }
}
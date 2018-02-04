using UnityEngine;
using UnityEngine.EventSystems;

namespace AI
{
    public interface IPlayerDetectedListener : IEventSystemHandler
    {
        void OnPlayerDetected (GameObject player);
    }
}
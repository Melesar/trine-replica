using UnityEngine.EventSystems;

namespace Interfaces
{
    public interface ITelekinesisReleaseListener : IEventSystemHandler
    {
        void OnTelekinesisRelease();
    }
}
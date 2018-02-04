using UnityEngine.EventSystems;

namespace Interfaces
{
    public interface ITelekinesisCaptureListener : IEventSystemHandler
    {
        void OnTelekinesisCapture();
    }
}
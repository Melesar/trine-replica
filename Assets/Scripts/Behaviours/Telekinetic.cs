using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Behaviours
{
    public class Telekinetic : MonoBehaviour
    {
        private Rigidbody2D rb;

        private float originalGravityScale;
        private BaseEventData eventData;
        
        public void Capture()
        {
            originalGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;

            ExecuteEvents.Execute<ITelekinesisCaptureListener>(gameObject, eventData,
                (handler, data) => handler.OnTelekinesisCapture());
        }

        public void Release()
        {
            rb.gravityScale = originalGravityScale;
            
            ExecuteEvents.Execute<ITelekinesisReleaseListener>(gameObject, eventData,
                (handler, data) => handler.OnTelekinesisRelease());
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            eventData = new BaseEventData(EventSystem.current);
        }
    }
}
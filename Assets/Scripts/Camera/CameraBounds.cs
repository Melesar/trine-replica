using UnityEngine;
using Variables;

namespace CameraBehaviours
{
    public class CameraBounds : MonoBehaviour
    {
        public Bounds cameraBounds;
        public BoundsVariable boundsVariable;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
        }

        private void Awake()
        {
            boundsVariable.Value = cameraBounds;
        }
    }
}
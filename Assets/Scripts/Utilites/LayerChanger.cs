using UnityEngine;

namespace Utilites
{
    public class LayerChanger : MonoBehaviour, IDeathListener
    {
        public LayerMask layerToSet;

        /// <summary>
        /// Changes player's layer upon death so that it
        /// is no longer being tracked by enemiess 
        /// </summary>
        void IDeathListener.OnDeath()
        {
            gameObject.layer = (int) Mathf.Log(layerToSet.value, 2);
        }
    }
}
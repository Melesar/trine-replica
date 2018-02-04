using System.Collections;
using UnityEngine;

namespace Systems
{
    public abstract class SystemBase : ScriptableObject
    {
        public virtual Coroutine Initialize(MonoBehaviour coroutineHost)
        {
            return coroutineHost.StartCoroutine(InitializationCoroutine());
        }

        protected abstract IEnumerator InitializationCoroutine();
    }
}
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Systems
{
    public abstract class SystemBase : ScriptableObject
    {
        private MonoBehaviour coroutineHost;

        private int coroutinesRunning;

        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            if (coroutineHost == null) {
                CreateCoroutineHost();
            }

            coroutinesRunning++;
            return coroutineHost.StartCoroutine(coroutine);
        }
        
        protected virtual void FinishCoroutine()
        {
            coroutinesRunning--;
        }
        
        protected virtual void OnEnable()
        {
            Debug.Log($"Enabling {name}. Instance id: {GetInstanceID()}");
        }

        protected virtual void OnDisable()
        {
            DisposeCoroutineHost();
            Debug.Log($"Disabling {name}. Instance id: {GetInstanceID()}");
        }

        private void CreateCoroutineHost()
        {
            var holder = new GameObject("Coroutine runner") {
                hideFlags = HideFlags.HideAndDontSave
            };

            coroutineHost = holder.AddComponent<CoroutineRunner>();
        }

        private void DisposeCoroutineHost()
        {
            if (coroutinesRunning == 0 && coroutineHost != null) {
                Destroy(coroutineHost.gameObject);
            }
        }
    }
    
    public class CoroutineRunner : MonoBehaviour {}
}
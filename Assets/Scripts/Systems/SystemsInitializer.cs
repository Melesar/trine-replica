using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class SystemsInitializer : MonoBehaviour
    {
        public List<SystemBase> systems;

        private IEnumerator Start()
        {
            foreach (var system in systems) {
                yield return system.Initialize(this);
            }
        }
    }
}
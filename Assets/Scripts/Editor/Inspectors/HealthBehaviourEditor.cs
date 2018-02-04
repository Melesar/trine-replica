using AI;
using UnityEditor;

namespace Editor.Inspectors
{
    [CustomEditor(typeof(HealthBehaviour))]
    public class HealthBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var health = (HealthBehaviour) target;
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Health: ");
            EditorGUILayout.LabelField($"{health.Health}/{health.healthStats.initialHealth}");
            EditorGUILayout.EndHorizontal();
        }
    }
}
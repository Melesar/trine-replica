using UnityEditor;
using UnityEngine;

namespace Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(CharacterTypeKeybinding))]
    public class CharacterTypeKeybindingDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width / 2f, position.height),
             property.FindPropertyRelative("key"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height),
             property.FindPropertyRelative("characterType"), GUIContent.none);
        }
    }
}
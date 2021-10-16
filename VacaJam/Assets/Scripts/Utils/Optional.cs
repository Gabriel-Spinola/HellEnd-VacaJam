using System;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private T _value;

        public bool Enabled => _enabled;
        public T Value => _value;

        public Optional(T initialValue)
        {
            _enabled = true;
            _value = initialValue;
        }
    }
    
    public struct OptionalNonSerializeble<T>
    {
        private bool _enabled;
        private T _value;

        public bool Enabled => _enabled;
        public T Value => _value;

        public OptionalNonSerializeble(T initialValue)
        {
            _enabled = true;
            _value = initialValue;
        }
    }

    namespace Editor
    {
        [CustomPropertyDrawer(typeof(Optional<>))]
        public class OptionalPropertyDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                var valueProperty = property.FindPropertyRelative("_value");

                return EditorGUI.GetPropertyHeight(valueProperty);
            }

            public override void OnGUI(
                Rect position,
                SerializedProperty property,
                GUIContent label
            )
            {
                var valueProperty = property.FindPropertyRelative("_value");
                var enabledProperty = property.FindPropertyRelative("_enabled");

                EditorGUI.BeginProperty(position, label, property);

                position.width -= 24;

                EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
                EditorGUI.PropertyField(position, valueProperty, label, true);
                EditorGUI.EndDisabledGroup();

                int indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                position.x += position.width + 24;
                position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
                position.x -= position.width;

                EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);

                EditorGUI.indentLevel = indent;

                EditorGUI.EndProperty();
            }
        }
    }
}
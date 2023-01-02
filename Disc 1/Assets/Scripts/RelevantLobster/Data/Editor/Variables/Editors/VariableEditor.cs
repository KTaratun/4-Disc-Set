using UnityEngine;
using System.Reflection;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// Base <see cref="Editor"/> for a <see cref="Variable{T}"/>.
    /// </summary>
    /// <typeparam name="TV">The type of <see cref="Variable{T}"/> this <see cref="Editor"/> is for.</typeparam>
    /// <typeparam name="T">The type of data the <see cref="Variable{T}"/> contains.</typeparam>
    public class VariableEditor<TV, T> : UnityEditor.Editor where TV :Variable<T>
    {
        private const int _STANDARD_BUTTON_WIDTH = 24;
        private const string _scriptPropertyName = "m_Script";

        private static readonly GUIContent s_btnContent = new GUIContent("✓", "Apply Value");

        private static GUIContent s_propertiesBtnContent;
        private static MethodInfo s_openPropertyEditorMethodInfo;

        /// <summary>
        /// Flag indicating if the <see cref="Variable{T}"/>'s value has changed in the editor.
        /// </summary>
        protected bool HasValueChanged { get; set; } = false;

        /// <summary>
        /// The value previously assigned to the <see cref="Variable{T}"/> in the editor.
        /// </summary>
        protected T PreviousAppliedValue { get; set; }

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            if (!(serializedObject.targetObject is TV variable))
            {
                base.OnInspectorGUI();
                return;
            }

            serializedObject.Update();

            SerializedProperty iterator = serializedObject.GetIterator();
            bool isPlayMode = EditorApplication.isPlaying;

            // This is atypical usage of a for loop. enterChildren is only on the first property, which is odd. We don't
            // have an explanition, as this source came from third party "Unity Atoms" package and it works. See
            // https://github.com/unity-atoms/unity-atoms
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                switch (iterator.propertyPath)
                {
                    // Initial value is always visible but is read only in play mode
                    case BaseVariable<T>.InitialValueFieldName:
                        DrawProperty(iterator, isPlayMode);
                        break;
                    case Variable<T>.PreviousValuePropertyName:
                        if (isPlayMode) { DrawProperty(iterator, true); }
                        break;
                    case Variable<T>.RuntimeValuePropertyName:
                        if (isPlayMode) { DrawRuntimeValue(iterator, variable); }
                        break;
                    case _scriptPropertyName:
                        bool prevGuiEnabled = GUI.enabled;
                        GUI.enabled = false;
                        EditorGUILayout.PropertyField(iterator);
                        GUI.enabled = prevGuiEnabled;
                        break;
                    default:
                        EditorGUILayout.PropertyField(iterator);
                        break;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Set the value of the <see cref="Variable{T}"/> to apply it when it has changed.
        /// </summary>
        /// <param name="variable">The <see cref="Variable{T}"/> to have its value set.</param>
        /// <param name="newValue">The new value for the <see cref="Variable{T}"/>.</param>
        protected virtual void SetValue(TV variable, T newValue)
        {
            // Need to set the variable to its previously applied value first in order to emulate the same
            // behavior as appears at runtime, with the Previous Value field actually showing correctly, but
            // NOT send notification because this value was already previously applied.
        }

        private void OnEnable()
        {
            s_propertiesBtnContent ??= new GUIContent(EditorGUIUtility.FindTexture("SettingsIcon"), "Properties");
        }

        private void DrawPropertiesButton(SerializedProperty property)
        {
            // Properties button drawn if there is an object reference value
            if (property.propertyType != SerializedPropertyType.ObjectReference || null == property.objectReferenceValue)
            {
                return;
            }

            if (GUILayout.Button(s_propertiesBtnContent, GUILayout.Width(_STANDARD_BUTTON_WIDTH)))
            {
                EditorUtility.OpenPropertyEditor(property.objectReferenceValue);
            }
        }

        private void DrawProperty(SerializedProperty property, bool isReadOnly = false)
        {
            EditorGUILayout.BeginHorizontal();

            bool prevGuiEnabled = GUI.enabled;
            GUI.enabled = !isReadOnly;
            EditorGUILayout.PropertyField(property);
            GUI.enabled = prevGuiEnabled;

            DrawPropertiesButton(property);

            EditorGUILayout.EndHorizontal();
        }

        private void DrawRuntimeValue(SerializedProperty property, TV variable)
        {
            EditorGUILayout.BeginHorizontal();

            // Field
            Color origColor = GUI.color;
            GUI.color = HasValueChanged ? Color.yellow : origColor;

            T value = variable.Value;
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(property);

            if (false == HasValueChanged && EditorGUI.EndChangeCheck())
            {
                HasValueChanged = true;
                PreviousAppliedValue = value;
            }

            GUI.color = origColor;

            DrawPropertiesButton(property);

            // Apply button
            if (GUILayout.Button(s_btnContent, GUILayout.Width(_STANDARD_BUTTON_WIDTH)))
            {
                T newValue = variable.Value;
                SetValue(variable, newValue);
                serializedObject.Update();
                HasValueChanged = false;
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
using JetBrains.Annotations;
using RelevantLobster.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RelevantLobster.Data.Editor
{
    /// <summary>
    /// Base <see cref="ScriptableObjectPropertyDrawer{T}"/> for adding an additional option button to the right of
    /// the regular property when the object reference is assigned.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="ScriptableObject"/> this <see cref="OptionButtonScriptableObjectPropertyDrawer{T}"/> is for.
    /// </typeparam>
    public abstract class OptionButtonScriptableObjectPropertyDrawer<T> 
        : ScriptableObjectPropertyDrawer<T> where T : ScriptableObject
    {
        protected abstract GUIContent ButtonContent { get; }

        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, GUIContent label)
        {
            if (null == property.objectReferenceValue)
            {
                base.OnGUI(position, property, label);

                return;
            }

            // Otherwise, the post button should be drawn to the right of the field
            Rect btnRect = RectUtility.SplitRectH(position, out Rect basePosition,
                position.width - Standard_Button_Width, Standard_Gutter_Width);
            
            btnRect.height = EditorGUIUtility.singleLineHeight;

            // Base property
            base.OnGUI(basePosition, property, label);

            // Post button
            if (GUI.Button(btnRect, ButtonContent))
            {
                if (property.objectReferenceValue is T objReference) { OnButtonPressed(property, objReference); }
            }
        }

        protected abstract void OnButtonPressed(SerializedProperty property, T objectReference);
    }
}
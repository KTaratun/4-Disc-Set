using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RelevantLobster.Data.Editor.Signals
{
    using Data.Signals;
    using System;

    [CustomEditor(typeof(Signal), editorForChildClasses: true)]
    public class SignalEditor : UnityEditor.Editor
    {
        private static readonly Color PostButtonColor = new Color(0.16f, 0.69f, 1f);

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Signal signal = (target as Signal);

            if (signal == null)
                return;

            Color originalColor = GUI.backgroundColor;
            GUI.backgroundColor = PostButtonColor;
           
            bool bGUIEnabled = GUI.enabled;
            GUI.enabled = true;

            if (GUILayout.Button("Post"))
            {
                signal.Post();
            }

            GUI.enabled = bGUIEnabled;
            GUI.backgroundColor = originalColor;

            EditorGUILayout.Space();
            IList<Action> actionList = signal.GetCopyOfListeners();

            EditorGUILayout.LabelField($"{actionList.Count} Registered Actions (Invocation Order):");
            foreach (var action in actionList)
            {
                string strListenerInfo = Helpers.GetClassMethodName(action);
                EditorGUILayout.LabelField("  ° " + strListenerInfo);
            }
        }
    }
}
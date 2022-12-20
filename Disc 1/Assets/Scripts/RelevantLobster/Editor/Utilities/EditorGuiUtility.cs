using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RelevantLobster.Editor.Utilities
{
    public static class EditorGuiUtility
    {
        public static UnityEngine.Object ObjectFieldWithTootip(Rect position, string tooltip, UnityEngine.Object obj, Type objType, bool allowSceneObjects)
        {
            EditorGUI.LabelField(position, new GUIContent("", tooltip));

            return EditorGUI.ObjectField(position, obj, objType, allowSceneObjects);
        }
    }
}
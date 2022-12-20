using RelevantLobster.Data.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RelevantLobster.Data.Editor.Signals
{
    public abstract class SignalPropertyDrawer<T> : OptionButtonScriptableObjectPropertyDrawer<T> where T : Signal
    {
        private static readonly GUIContent _S_btnContent = new GUIContent("➤", "Post Signal");

        protected override GUIContent ButtonContent => _S_btnContent;

        protected override void OnButtonPressed(SerializedProperty property, T objectReference)
        {
            objectReference.Post();
        }
    }

    [CustomPropertyDrawer(typeof(Signal))]
    public class SignalPropertyDrawer : SignalPropertyDrawer<Signal> { }
}
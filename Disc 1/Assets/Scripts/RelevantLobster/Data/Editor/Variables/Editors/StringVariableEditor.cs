using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="StringVariable"/>.
    /// </summary>
    [CustomEditor(typeof(StringVariable), true)]
    public class StringVariableEditor : VariableEditor<StringVariable, string> { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="BoolVariable"/>.
    /// </summary>
    [CustomEditor(typeof(BoolVariable), true)]
    public class BoolVariableEditor : VariableEditor<BoolVariable, bool> { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="Vector2Variable"/>.
    /// </summary>
    [CustomEditor(typeof(Vector2Variable), true)]
    public class Vector2VariableEditor : VariableEditor<Vector2Variable, Vector2> { }
}

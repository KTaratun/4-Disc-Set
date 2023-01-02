using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="Vector3Variable"/>.
    /// </summary>
    [CustomEditor(typeof(Vector3Variable), true)]
    public class Vector3VariableEditor : VariableEditor<Vector3Variable, Vector3> { }
}

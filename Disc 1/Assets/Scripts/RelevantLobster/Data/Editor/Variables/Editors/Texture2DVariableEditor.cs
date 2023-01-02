using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="Texture2DVariable"/>.
    /// </summary>
    [CustomEditor(typeof(Texture2DVariable), true)]
    public class Texture2DVariableEditor : VariableEditor<Texture2DVariable, Texture2D> { }
}
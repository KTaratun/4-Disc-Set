using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="SpriteVariable"/>.
    /// </summary>
    [CustomEditor(typeof(SpriteVariable), true)]
    public class SpriteVariableEditor : VariableEditor<SpriteVariable, Sprite> { }
}
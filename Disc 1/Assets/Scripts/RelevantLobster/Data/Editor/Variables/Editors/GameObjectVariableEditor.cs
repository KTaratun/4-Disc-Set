using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="GameObjectVariable"/>.
    /// </summary>
    [CustomEditor(typeof(GameObjectVariable), true)]
    public class GameObjectVariableEditor : VariableEditor<GameObjectVariable, GameObject> { }
}
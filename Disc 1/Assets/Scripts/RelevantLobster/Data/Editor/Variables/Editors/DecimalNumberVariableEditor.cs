using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RelevantLobster.Data.Editor.Variables.Editors
{
    using Data.Variables;
    using Types.Numbers;

    /// <summary>
    /// A <see cref="VariableEditor{TV, T}"/> for the type <see cref="DecimalNumberVariable"/>.
    /// </summary>
    [CustomEditor(typeof(DecimalNumberVariable), true)]
    public class DecimalNumberVariableEditor : VariableEditor<DecimalNumberVariable, DecimalNumber> { }
}
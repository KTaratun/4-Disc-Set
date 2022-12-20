using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data
{
    /// <summary>
    /// Base class for a <see cref="ScriptableObject"/> which can have a literal value associated with it.
    /// </summary>
    /// <typeparam name="T">The type of literal value that can be associated with this <see cref="ScriptableObject"/></typeparam>
    public abstract class SubLiteralScriptableObject<T> : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// Flag indicating if the function is being overridden by a literal value for debugging purposes.
        /// </summary>
        internal bool SubLiteralValue { get; set; }

        /// <summary>
        /// The literal value the function should be overridden with.
        /// </summary>
        internal T LiteralValue { get; set; }
#endif
    }
}
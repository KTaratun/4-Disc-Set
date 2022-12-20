using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Types.Numbers
{
    /// <summary>
    /// Base class for a special number type which wraps a C# number type.
    /// </summary>
    /// <typeparam name="T">The type of C# number that is wrapped.</typeparam>
    public class BaseNumber<T> where T : struct, IComparable<T>
    {
        // This serialized field should not have a tooltip, as it is not the one that is ever meant to display; rather,
        // the tooltip defined by any serialized field OF THIS TYPE should be the one shown in the UI.
        [SerializeField] private T m_value = default;

#if UNITY_EDITOR
        internal const string ValueFieldName = nameof(m_value);
#endif

        /// <summary>
        /// The current value of this <see cref="BaseNumber{T}"/> as its inherent type <typeparamref name="T"/>.
        /// </summary>
        public T Value
        {
            get => m_value;
            set => m_value = value;
        }

        /// <summary>
        /// Constructs a new <see cref="BaseNumber{T}"/>.
        /// </summary>
        protected BaseNumber() { }

        protected BaseNumber(T value) {Value = value; }
    }
}

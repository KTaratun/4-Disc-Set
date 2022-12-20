using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// Common interface for all variables.
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// The <see cref="IVariable"/>'s current runtime value as an <see cref="object"/>.
        /// </summary>
        /// <remarks>
        /// <para>Be aware of and avoid object boxing, which can cause multiple GC allocations and affect performance.</para>
        /// <para>For more information, see https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing</para>
        /// </remarks>
        public object BaseValue { get; }

        /// <summary>
        /// Reset the <see cref="IVariable"/>'s value.
        /// </summary>
        /// name="notify">
        /// Flag indicating if any registered observers of the Variable should be notified of the reset value change
        /// </param>
        public void ResetValue(bool notify = false);

        /// <summary>
        /// Common interface for all generic types of Variables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IVariable<out T> : IVariable
        {
            /// <summary>
            /// The <see cref="IVariable{T}"/>'s initial runtime value.
            /// </summary>
            public T InitialValue { get; }

            /// <summary>
            /// The <see cref="IVariable{T}"/>'s current runtime value.
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// The <see cref="IVariable{T}"/>'s previous runtime value.
            /// </summary>
            public T PreviousValue { get; }
        }
    }
}
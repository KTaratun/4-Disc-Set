using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Utilities
{
    public static class ObjectUtility
    {
        private static readonly Type s_stringType = typeof(string);

        /// <summary>
        /// Determine if two <see cref="System.Object"/>s are both <see langword="null"/>.
        /// </summary>
        /// <param name="left">The first <see cref="System.Object"/> to compare to <see langword="null"/></param>
        /// <param name="right">The second <see cref="System.Object"/> to compare to <see langword="null"/></param>
        /// <returns>True if both <paramref name="left"/> and <paramref name="right"/> are <see langword="null"/>; false otherwise</returns>
        public static bool AreBothNull(object left, object right) => left is null && right is null;

        /// <summary>
        /// Determine if two <see cref="System.Object"/>s are both non-<see langword="null"/>.
        /// </summary>
        /// <param name="left">The first <see cref="System.Object"/> to compare to <see langword="null"/></param>
        /// <param name="right">The second <see cref="System.Object"/> to compare to <see langword="null"/></param>
        /// <returns>True if both <paramref name="left"/> and <paramref name="right"/> are not <see langword="null"/>; false otherwise</returns>
        public static bool AreBothNotNull(object left, object right) => left is null == false && right is null == false;

        /// <summary>
        /// Determine if an object of type <typeparamref name="T"/> is <i>treated<i> as a value type, even though it may
        /// <i>actually</i> be a reference type.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There are various types, both in C# and Unity, that are treated as value types in the way that they behave,
        /// even though they are actually reference types.
        /// </para>
        /// <para>
        /// <see cref="string"/> in C# is a reference type, but it is treated like a value type because, as soon as it
        /// is created, it is made immutable, so there is (almost) always a new reference created for every string
        /// operation. Internally, and in some newer versions of C#, one <i>can</i> manipulate the string without
        /// creating a new reference.
        /// </para>
        /// <para>
        /// Classes deriving from <see cref="UnityEngine.Object"/> are treated like value types because of the way the
        /// Unity engine defines and (de)serializes them.
        /// </para>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsTreatedAsValueType<T>()
        {
            Type objectType = typeof(T);

            return objectType.IsValueType || objectType == s_stringType || objectType.IsSubclassOf(typeof(UnityEngine.Object));
        }
    }
}
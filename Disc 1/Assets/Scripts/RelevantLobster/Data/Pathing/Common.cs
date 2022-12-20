using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Pathing
{
    public static class Common
    {
        /// <summary>
        /// The name of the <see cref="Data"/> package.
        /// </summary>
        public const string PackageName = nameof(Data);

        public const string FrameworkName = nameof(RelevantLobster);

        /// <summary>
        /// Common strings for use with <see cref="Signal"/>s.
        /// </summary>
        public static class Signals
        {
            /// <summary>
            /// The name of this part of the <see cref="RelevantLobster"/> package.
            /// </summary>
            public const string SubPackageName = nameof(Data.Signals);
        }

        /// <summary>
        /// Common strings for use with <see cref="Variables{T}"/>s.
        /// </summary>
        public static class Variables
        {
            /// <summary>
            /// The name of this part of the <see cref="RelevantLobster"/> package.
            /// </summary>
            public const string SubPackageName = nameof(Data.Variables);
        }
    }
}
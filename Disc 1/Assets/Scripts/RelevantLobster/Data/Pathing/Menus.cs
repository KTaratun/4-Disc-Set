using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Pathing
{
    public static class Menus
    {
        /// <summary>
        /// Common separator used for constructing a Unity Editor menu path string.
        /// </summary>
        public const string Separator = "/";

        /// <summary>
        /// The root menu path for <see cref="RelevantLobster"/> in general.
        /// </summary>
        public const string Root = Common.FrameworkName + Separator;

        /// <summary>
        /// The root menu path for this package.
        /// </summary>
        public const string PackageRoot = Common.PackageName + Separator;

        /// <summary>
        /// The root menu path for use with the <see cref="UnityEditor.MenuItem"/> attribute.
        /// </summary>
        public const string MenuItem = Root + PackageRoot;

        public static class CreateAssetMenu
        {
            public const string Signals = MenuItem + Common.Signals.SubPackageName + Separator;

            public const string Variables = MenuItem + Common.Variables.SubPackageName + Separator;
        }
    }
}
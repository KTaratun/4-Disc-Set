using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Signals
{
    public static class Helpers
    {
        /// <summary>
        /// Helper that extracts class and method name.
        /// </summary>
        /// <param name="delegateToUse">The <see cref="Delegate"/> to extract information from.</param>
        /// <returns>String of the full method name: "namespace.class.method"</returns>
        public static string GetClassMethodName(Delegate delegateToUse)
        {
            string methodName = delegateToUse.Method.Name; // Includes Namespace

            Type methodType = delegateToUse.Method.ReflectedType;
            string className = (methodType != null) ? methodType.FullName : "Unknown";
            
            return $"{className}.{methodName}";
        }
    }
}
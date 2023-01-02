using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Functions
{
    /// <summary>
    /// Base class for all functions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A Function provides injectable logic as a source for consumers. Functions always return a value. If you need
    /// injectable functionality that does not return a value, use a derivation of <see cref="ActionBase"/>.
    /// </para>
    /// <para>
    /// Functions can be implemented using one of two methods.
    /// </para>
    /// <list type="number">
    /// <listheader>
    /// <description><b>Implementing and Using Functions</b></description>
    /// </listheader>
    /// <item>
    /// <term><b>Derivation</b></term>
    /// <description>
    /// <para>
    /// A concrete class deriving from one of <see cref="FunctionBase{TR}"/>'s intermediate, non-generic classes (e.g.
    /// <see cref="BoolFunction"/>, <see cref="LongFunctionLong"/> is created and the Invoke method is overridden with the
    /// implementation of the custom logic desired.
    /// </para>
    /// <para>
    /// In most cases of this type, the Assign method will never be used.
    /// </para>
    /// </description>
    /// </item>
    /// <item>
    /// <term><b>Assignation</b></term>
    /// <description>
    /// <para>
    /// This abstract class is used as a field or property reference and the Assign method is used at
    /// runtime to dynamically assign the appropriate logic.
    /// </para>
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <typeparam name="TR">The type of data this <see cref="FunctionBase{TR}"/> returns.</typeparam>
    public class FunctionBase<TR> : SubLiteralScriptableObject<TR> { }
}
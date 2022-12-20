using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    using Pathing;
    using Types.Numbers;

    /// <summary>
    /// A <see cref="Variable{T}"/> of type <see cref="DecimalNumber"/>.
    /// </summary>
    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Variables + nameof(DecimalNumber))]
    public class DecimalNumberVariable : Variable<DecimalNumber> { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Utilities
{
    /// <summary>
    /// Utility methods relating to the <see cref="Rect"/> class.
    /// </summary>
    public static class RectUtility
    {
        /// <summary>
        /// Splits the given <see cref="Rect"/> horizontally into two <i>new<i> <see cref="Rect"/>s.
        /// </summary>
        /// <param name="orig">The <see cref="Rect"/> to be split. This <see cref="Rect"/> is not modified.</param>
        /// <param name="top">The new <see cref="Rect"/> representing the left portion of the split.</param>
        /// <param name="topHeight">The width desired for the left <see cref="Rect"/>.</param>
        /// <param name="gutter">The amount of the space to place between the top two new <see cref="Rect"/>s.</param>
        /// <returns>The new <see cref="Rect"/> representing the right portion of the split.</returns>
        public static Rect SplitRectH(Rect orig, out Rect left, float leftWidth, float gutter = 0f)
        {
            // Left
            left = SnipRectH(orig, leftWidth);

            // Right
            if (leftWidth == 0) { return new Rect(orig); }

            return leftWidth > 0
                ? new Rect(orig.x + leftWidth + gutter, orig.y, orig.width - leftWidth - gutter, orig.height)
                : new Rect(orig.x, orig.y, orig.width + leftWidth + gutter, orig.height);
        }

        /// <summary>
        /// Splits the given <see cref="Rect"/> vertically into two <i>new<i> <see cref="Rect"/>s.
        /// </summary>
        /// <param name="orig">The <see cref="Rect"/> to be split. This <see cref="Rect"/> is not modified.</param>
        /// <param name="top">The new <see cref="Rect"/> representing the top portion of the split.</param>
        /// <param name="topHeight">The height desired for the top <see cref="Rect"/>.</param>
        /// <param name="gutter">The amount of the space to place between the top two new <see cref="Rect"/>s.</param>
        /// <returns>The new <see cref="Rect"/> representing the bottom portion of the split.</returns>
        public static Rect SplitRectV(Rect orig, out Rect top, float topHeight, float gutter = 0f)
        {
            // Top
            top = SnipRectV(orig, topHeight);

            // Bottom
            if (topHeight == 0) { return new Rect(orig); }

            return topHeight > 0
                ? new Rect(orig.x, orig.y + topHeight + gutter, orig.width, orig.height - topHeight - gutter)
                : new Rect(orig.x, orig.y, orig.width, orig.height + topHeight + gutter);
        }

        /// <summary>
        /// Snips a <see cref="Rect"/> horizontally.
        /// </summary>
        /// <remarks>
        /// If the given <paramref name="width"/> is negative, the resulting <see cref="Rect"/> will have the given
        /// width and be centered vertically on the right side of the <paramref name="orig"/> <see cref="Rect"/>.
        /// </remarks>
        /// <param name="orig">The original <see cref="Rect"/> to be snipped.</param>
        /// <param name="width">The desired width for the snipped <see cref="Rect"/>.</param>
        /// <returns></returns>
        public static Rect SnipRectH(Rect orig, float width)
        {
            if (width == 0) { return orig; }

            return width > 0
                ? new Rect(orig.x, orig.y, width, orig.height)
                : new Rect(orig.x + orig.width + width, orig.y, -width, orig.height);
        }

        /// <summary>
        /// Snips a <see cref="Rect"/> vertically.
        /// </summary>
        /// <remarks>
        /// If the given <paramref name="height"/> is negative, the resulting <see cref="Rect"/> will have the given
        /// height and be centered vertically on the bottom side of the <paramref name="orig"/> <see cref="Rect"/>.
        /// </remarks>
        /// <param name="orig"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Rect SnipRectV(Rect orig, float height)
        {
            if (height == 0) { return orig; }

            return height > 0 
                ? new Rect(orig.x, orig.y, orig.width, height)
                : new Rect(orig.x, orig.y + orig.height + height, orig.width, -height);
        }
    }
}

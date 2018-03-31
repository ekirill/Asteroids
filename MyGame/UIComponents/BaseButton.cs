using System;
using System.Drawing;

namespace MyGame.UIComponents
{
    /// <summary>
    /// Represents UI button
    /// </summary>
    class BaseButton: BaseLabel
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="rect">Position of button</param>
        /// <param name="bgColor">Background color</param>
        /// <param name="textColor">Text color</param>
        /// <param name="text">Text to display</param>
        public BaseButton(Rectangle rect, Color bgColor, Color textColor, String text) : base(rect, bgColor, textColor, text) { }
    }
}

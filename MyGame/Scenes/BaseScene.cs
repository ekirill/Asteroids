using System;
using System.Drawing;

namespace MyGame.Scenes
{
    /// <summary>
    /// Base class for all scenes of the game. Scene should instantinate layers and delegate
    /// logic to them.
    /// </summary>
    abstract class BaseScene: Core.BaseInterfaceElement
    {
        protected Core.DebugLog debug;

        public BaseScene(Core.DebugLog debug)
        {
            this.debug = debug;
        }
    }
}

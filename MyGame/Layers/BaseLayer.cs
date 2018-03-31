using System;
using System.Drawing;

namespace MyGame.Layers
{
    /// <summary>
    /// Base class for layers. Layers instantinate game objects, that interact with each other.
    /// </summary>
    abstract class BaseLayer: Core.BaseInterfaceElement
    {
        protected Core.DebugLog debug;

        public BaseLayer(Core.DebugLog debug)
        {
            this.debug = debug;
        }
    }
}


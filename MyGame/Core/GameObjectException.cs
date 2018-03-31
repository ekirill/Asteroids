using System;

namespace MyGame.Core
{
    /// <summary>
    /// Exception for throwing errors at object init phase
    /// </summary>
    class GameObjectException: Exception
    {

        public GameObjectException(string message)
        : base(message)
        {
        }

    }
}

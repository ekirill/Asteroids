using System;

namespace MyGame.Core
{
    public enum MoveDirection
    {
        Up,
        Down
    }

    public delegate void MoveKeyHandler(MoveDirection direction);
    public delegate void ShootKeyHandler();
}

using System;
using System.Drawing;


namespace MyGame.Core
{
    /// <summary>
    /// Class for representing movable objects of the gameplay
    /// </summary>
    public abstract class BaseGameObject: BaseInterfaceElement
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="pos">Initial position on the form</param>
        /// <param name="dir">The directional vector. Each update call updates position using this vector.</param>
        /// <param name="size">Size of an element.</param>
        public BaseGameObject(Point pos, Point dir, Size size)
        {
            if (size.Width < 1 || size.Height < 1) {
                throw new GameObjectException("Object must have size > 0");
            }
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public override void Draw() { }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }
    }
}

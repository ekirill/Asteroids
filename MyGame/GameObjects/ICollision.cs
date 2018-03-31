using System;
using System.Drawing;

namespace MyGame.GameObjects
{
    /// <summary>
    /// Interface for objects, that can collide with each other
    /// </summary>
    public interface ICollision
    {
        /// <summary>
        /// Should return true, if object collides with obj
        /// </summary>
        /// <param name="obj">obj implementin ICollision interface</param>
        /// <returns>bool</returns>
        bool Collision(ICollision obj);

        /// <summary>
        /// Rectangle that represents object borders.
        /// </summary>
        Rectangle Rect { get; }

        /// <summary>
        /// Handler method for collisions with other objects
        /// </summary>
        void CollisionOccured(ICollision obj);
    }
}

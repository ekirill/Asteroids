using System;


namespace MyGame.Core
{
    /// <summary>
    /// Base class for all Drawable and Updatable classes
    /// </summary>
    abstract public class BaseInterfaceElement
    {
        public BaseInterfaceElement() { }

        /// <summary>
        /// Called every Game tick at Draw phase and is responsible for representing itself on the form
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Called every Game tick at Update phase and is responsible for modifing the state of self
        /// </summary>
        public abstract void Update();
    }
}

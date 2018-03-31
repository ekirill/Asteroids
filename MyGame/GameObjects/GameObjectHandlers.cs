using System;
using System.Drawing;

namespace MyGame.GameObjects
{
    public delegate void ShipShootingHandler(Point shootingPos);
    public delegate void ShipEnergyChangeHandler();
    public delegate void AsteroidShootHandler(Asteroid asteroid);
}

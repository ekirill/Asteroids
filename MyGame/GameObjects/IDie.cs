using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameObjects
{
    public delegate void DieHandler(IDie obj);

    public interface IDie
    {
        void Die();
        event DieHandler Died;
    }
}

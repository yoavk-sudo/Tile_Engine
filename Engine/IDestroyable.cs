using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    internal interface IDestroyable
    {
        void Destroy();
        void DestroyChildren();
    }
}

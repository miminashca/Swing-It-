using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class Breakables : AnimationSprite
    {
        public Breakables(string filename, int c, int r, TiledObject data) : base (filename, c, r)
        {

        }

        void OnCollision(GameObject other)
        {
            if (other is Anchor)
            {
                LateDestroy();
            }
        }
    }
}

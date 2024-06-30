using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class ColBlock : AnimationSprite
    {
        public bool isWall;
        public bool killZone;

        public ColBlock(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            collider.isTrigger = true;
            visible = false;
            isWall = data.GetBoolProperty("isWall");
            killZone = data.GetBoolProperty("killZone");
        }
    }
}

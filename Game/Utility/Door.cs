using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class Door:AnimationSprite
    {
        public int doorKey;
        public bool isOpen = false;
        public Door(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            doorKey = data.GetIntProperty("Key");
        }

        public void OpenDoor()
        {
            collider.isTrigger = true;
            SetCycle(1);
        }

        public void CloseDoor()
        {
            collider.isTrigger = false;
            SetCycle(0);
        }
    }
}

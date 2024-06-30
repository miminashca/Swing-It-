using GXPEngine;
using System;
using System.Linq;
using TiledMapParser;

namespace GXPEngine
{
    public class CheckPoint:AnimationSprite
    {
        Player _player;
        public bool soundPlayed = false;

        public TiledObject properties;

        Vec2 position = new Vec2();

        public CheckPoint(string filename, int c, int r, TiledObject obj) : base(filename, c, r)
        {
            x = obj.X;
            y = obj.Y;

            position.x = x;
            position.y = y;

            properties = obj;
            collider.isTrigger = true;
        }

        void Update()
        {
            if (_player == null) _player = MyGame.main.FindObjectOfType<Player>();

            if (_player.health == 0)
            {
                SetCycle(0);
            }
        }
    }
}

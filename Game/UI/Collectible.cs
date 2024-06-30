using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class Collectible : AnimationSprite
    {
        Player _player;


        Vec2 _pos = new Vec2();
        bool UI;
        int NumCollect;
        public Collectible(string filename, int c, int r, TiledObject data) : base (filename, c, r)
        {
            _pos.x = data.X;
            _pos.y = data.Y;

            collider.isTrigger = true;
            UI = data.GetBoolProperty("UI");
            NumCollect = data.GetIntProperty("NumCollect");

            _player = MyGame.main.FindObjectOfType<Player>();
        }

        void Update()
        {
            _player = MyGame.main.FindObjectOfType<Player>();

            if (UI)
            {
                x = Level.main.sky.x + _pos.x;
                y = Level.main.sky.y + _pos.y;

                if (_player.collection >= NumCollect)
                {
                    //Console.WriteLine("");
                    SetCycle(0);
                }
                else SetCycle(1);
            }
        }
    }
}

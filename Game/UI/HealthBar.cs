using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class HealthBar : AnimationSprite
    {
        Player _player;

        Vec2 _pos = new Vec2();

        int HealthNum;
        public HealthBar(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            _pos.x = data.X;
            _pos.y = data.Y;

            HealthNum = data.GetIntProperty("NumHeart");
            _player = MyGame.main.FindObjectOfType<Player>();
        }

        void Update()
        {
            if (_player == null) _player = MyGame.main.FindObjectOfType<Player>();

            x = Level.main.sky.x + _pos.x;
            y = Level.main.sky.y + _pos.y;

            if (HealthNum <= _player.health)
            {
                SetCycle(0);
            } else SetCycle(1);
        } 
    }
}

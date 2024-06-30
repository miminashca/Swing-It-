using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class Enemy : AnimationSprite
    {
        Player _player = MyGame.main.FindObjectOfType<Player>();

        public Vec2 velocity = new Vec2(0, 0);
        public Vec2 position;

        int speedx;
        int speedy;

        float speed;

        public Enemy(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            position.x = data.X;
            position.y = data.Y;

            speedx = data.GetIntProperty("Speedx");
            speedy = data.GetIntProperty("Speedy");
            speed = data.GetFloatProperty("Speed")/10;

            collider.isTrigger = true;
        }

        void Update()
        {
            if (_player == null)
            {
                _player = MyGame.main.FindObjectOfType<Player>();
            }

            UpdateScreenPosition();
            Control();

            Animation();
        }

        void UpdateScreenPosition()
        {
            position.x = x;
            position.y = y;


            /*MoveUntilCollision(0, velocity.y);*/
            Collision collision = MoveUntilCollision(velocity.x, 0);
            if ( collision != null/* || MoveUntilCollision(0, velocity.y) != null*/)
            {
                speed *= -1;
            }

            LocatePlayer();
        }

        void Control()
        {
            velocity *= 0.9f;

            velocity += new Vec2(speedx, speedy) * speed;

           
        }

        void LocatePlayer()
        {
            if (_player.x + _player.width/2+0.1f > x - width/2 && _player.x - _player.width/2-0.1f < x + width/2)
            {
                if (_player.y + _player.height/2 > y && _player.y <= y)
                {
                    _player.DamagePlayer();
                } else if (_player.y + height/2 > y - height/2 && _player.y + height/2 < y)
                {
                    Destroy();
                }
            }
        }

        void Animation()
        {
            SetCycle(0, 2);
            Animate(0.03f);

            if (velocity.x > 0)
            {
                Mirror(true, false);
            } else if (velocity.x < 0)
            {
                Mirror(false, false);
            }
        }
    }
}

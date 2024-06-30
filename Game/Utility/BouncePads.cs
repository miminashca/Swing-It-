using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class BouncePads:AnimationSprite
    {
        SoundHandler _soundHandler;

        public Vec2 velocity = new Vec2(0, 0);
        public bool animation = false;
        
        public BouncePads(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            velocity.y = data.GetFloatProperty("bounciness");
            collider.isTrigger = true;

            _soundHandler = MyGame.main.FindObjectOfType<SoundHandler>();
        }

        void Update()
        {
            if (_soundHandler == null) _soundHandler = MyGame.main.FindObjectOfType<SoundHandler>();

            if (animation)
            {
                Animation();
            }
        }

        void Animation()
        {
            SetCycle(0, 2);
            Animate(1f);

            _soundHandler.JumpPad();

            if (currentFrame == 0)
            {
                animation = false;
            }
        }
    }
}

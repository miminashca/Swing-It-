using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TiledMapParser;

namespace GXPEngine
{
    internal class DisPlatform : AnimationSprite
    {
        int WalkTimer = 0;
        int WaitTimer = -5000;
        bool setTimerWalk = false;
        bool setTimerWait = false;

        float timerWalk;
        float timerWait;

        bool Start = true;

        public DisPlatform(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            collider.isTrigger = false;
            timerWalk = data.GetFloatProperty("WalkTimer") * 1000;
            timerWait = data.GetFloatProperty("WaitTimer") * 1000;
        }

        void Update()
        {
            if (Start)
            {
                collider.isTrigger = true;
            }

            int start = Time.time;
            GameObject[] collisions = GetCollisions();
            //Console.WriteLine("DisPlatofmr.Update() got collisions in {0} ms", Time.time - start);


            var touch = collisions.FirstOrDefault(x => x is AnimationSprite) as AnimationSprite;
            if (touch != null && touch.y < y)
            {
                if (!setTimerWalk && !setTimerWait)
                {
                    WalkTimer = Time.time;
                    setTimerWalk = true;
                    touch.y -= 1;
                }
            }

            if (WaitTimer + timerWait < Time.time && setTimerWait)
            {
                collider.isTrigger = false;
                setTimerWait = false;
            }

            if (WalkTimer + timerWalk < Time.time && setTimerWalk)
            {
                WaitTimer = Time.time;
                setTimerWait = true;
                collider.isTrigger = true;
                setTimerWalk = false;
            }

            if (setTimerWalk)
            {
                collider.isTrigger = false;
            }
            if (setTimerWait)
            {
                visible = false;
            } else visible = true;
            {
                
            }
        }
    }
}

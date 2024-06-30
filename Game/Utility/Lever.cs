using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class Lever:AnimationSprite
    {

        public int leverKey;
        public bool pulled = false;
        public Lever(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            leverKey = data.GetIntProperty("Key");
            collider.isTrigger = true;
        }

        public void openDoor()
        {
            Door[] doors = Level.main.FindObjectsOfType<Door>();
            foreach(Door door in doors)
            {
                if (door.doorKey == leverKey)
                {
                    door.OpenDoor();
                }
            }
        }

        public void closeDoor()
        {
            Door[] doors = Level.main.FindObjectsOfType<Door>();
            foreach (Door door in doors)
            {
                if (door.doorKey == leverKey)
                {
                    door.CloseDoor();
                    pulled = false;
                }
            }
        }
        
        void Update()
        {
            if (!pulled)
            {
                SetCycle(0);
            } else if (pulled)
            {
                SetCycle(1);
            }
        }
    }
}
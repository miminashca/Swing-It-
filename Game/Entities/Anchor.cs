using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

    public class Anchor : Sprite
    {
        public bool _isOnWall
        {
            get
            {
                return isOnWalL;
            }
        }
        public Vec2 position;
        public bool isOnWalL = false;
        public Anchor() : base("Images/mini_mushroom.png", false, true)
        {
            SetOrigin(width/2, height/2);
            // width = 30;
            // height = 30;
            position = new Vec2(0, 0); 
        }

        public void Update()
        {
            x = position.x;
            y = position.y; 
            CheckCollisions();
        }

        void CheckCollisions()
        {
            var wall = GetCollisions().FirstOrDefault(x => x is ColBlock) as ColBlock;
            if (wall != null && wall.isWall)
            {
                isOnWalL = true;
            }
        }
    }
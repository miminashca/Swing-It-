using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TiledMapParser;

namespace GXPEngine
{
    internal class Level : GameObject
    {
        public Pivot sky;
        public Player _player;
        public static Level main;
        SceneHandler _sceneHandler;

        public List<Door> Doors = new List<Door>();

        string level;
        
        public Level(int pLevelNumb)
        {
            levelSelect(pLevelNumb);

            sky = new Pivot();
            AddChild(sky);

            int p1 = GetChildCount();
            //Console.WriteLine("\n\nLevel(): now has {0} tiles: sky pivot", p1);
            TiledLoader loader = new TiledLoader(level, sky, false);
            
            loader.addColliders = false;
            loader.LoadTileLayers(0);
            int p2 = GetChildCount();
            //Console.WriteLine("Level(): now added {0} tiles: Background trees", p2 - p1);

            loader.rootObject = this;
            for (int i = 1; i < loader.NumTileLayers - 1; i++)
            {
                loader.LoadTileLayers(i);
            }
            int p3 = GetChildCount();
            //Console.WriteLine("Level(): now added {0} tiles: Background terrain", p3 - p2);
            
            loader.addColliders = true;
            loader.LoadTileLayers(loader.NumTileLayers - 1);
            int p4 = GetChildCount();
            //Console.WriteLine("Level(): now added {0} collider tiles: Terrain", p4 - p3);

            loader.addColliders = false;
            loader.LoadObjectGroups(0);

            loader.addColliders = true;
            loader.autoInstance = true;
            for (int i = 1; i <= loader.NumObjectGroups - 1; i++)
            {
                loader.LoadObjectGroups(i);
            }
            int p5 = GetChildCount();
            //Console.WriteLine("Level(): now added {0} Objects with collision", p5 - p4);

            _player = MyGame.main.FindObjectOfType<Player>();
            _sceneHandler = MyGame.main.FindObjectOfType<SceneHandler>();
            main = this;
        }

        void levelSelect(int pLevelNumb)
        {
            switch (pLevelNumb)
            {
                case 1:
                    level = "Level/startScreen.tmx";
                    break;
                case 2:
                    level = "Level/Level1.tmx";
                    break;
                case 3:
                    level = "Level/Level2.tmx";
                    break;
                case 4:
                    level = "Level/endScreen.tmx";
                    break;
            }
        }

        public void Update()
        {
            _player = MyGame.main.FindObjectOfType<Player>();
            if (_sceneHandler == null) _sceneHandler = MyGame.main.FindObjectOfType<SceneHandler>();

            if (_player != null)
            {
                Scroll();
            } else if (Input.AnyKeyDown() && Input.GetKeyDown(Key.SPACE) == false)
            {
                switch(_sceneHandler.levelNumb)
                {
                    case 1: 
                        _sceneHandler.levelNumb++;
                        _sceneHandler.DestroyAll();
                        break;
                    case 4:
                        _sceneHandler.levelNumb = 1;
                        _sceneHandler.DestroyAll();
                        break;
                }
            }
        }

        void Scroll()
        {
            float scrollvelocity = .05f;
            float scrollspeedground = 1f;
            float scrollspeedsky = 1f;

            float middle = game.width / 2;
            float center = game.height / 2;

            x -= middle;
            x = x * (1f - scrollvelocity) * scrollspeedground - _player.x * scrollvelocity * scrollspeedground;
            x += middle;


            sky.x = -sky.x;
            sky.x = sky.x * (1f - scrollvelocity) * scrollspeedsky - _player.x * scrollvelocity * scrollspeedsky;
            sky.x = -sky.x - 48;

            y -= center;
            y = y * (1f - scrollvelocity) * scrollspeedground - _player.y * scrollvelocity * scrollspeedground;
            y += center;

            sky.y = -sky.y;
            sky.y = sky.y * (1f - scrollvelocity) * scrollspeedsky - _player.y * scrollvelocity * scrollspeedsky;
            sky.y = -sky.y - 28;
        }
    }
}

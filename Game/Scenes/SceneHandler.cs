using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class SceneHandler:GameObject
    {
        Player player;
        
        Level _level;

        public int levelNumb = 1;
        public SceneHandler() 
        {          
            player = MyGame.main.FindObjectOfType<Player>();

            NewLevel();
        }

        public void NewLevel()
        {
            _level = new Level(levelNumb);
            LateAddChild(_level);
        }

        public void DestroyAll()
        {
            List<GameObject> children = GetChildren();
            foreach (GameObject child in children)
            {
                child.LateDestroy();
            }
            NewLevel();
        }

        void Update()
        {

            if (player == null)
            {
                player = MyGame.main.FindObjectOfType<Player>();
            }
            
        }
    }
}
using System;
using GXPEngine;
using TiledMapParser;

namespace GXPEngine
{
    internal class EndPoint:AnimationSprite
    {
        SceneHandler _sceneHandler;
        SoundHandler _soundHandler;

        public bool nextLevel = false;

        public EndPoint(string filename, int c, int r, TiledObject obj) : base(filename, c, r)
        {
            collider.isTrigger = true;
            _sceneHandler = MyGame.main.FindObjectOfType<SceneHandler>();
            _soundHandler = MyGame.main.FindObjectOfType<SoundHandler>();
        }

        void Update()
        {
            if (_sceneHandler == null) _sceneHandler = MyGame.main.FindObjectOfType<SceneHandler>();
            if (_soundHandler == null) _soundHandler = MyGame.main.FindObjectOfType<SoundHandler>();

            if (nextLevel)
            {
                _sceneHandler.levelNumb += 1;
                _sceneHandler.DestroyAll();
                nextLevel = false;
            }
        }

        bool soundPlaying = false;
        void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                if (!soundPlaying)
                {
                    soundPlaying = true;
                    _soundHandler.EndSound();
                }
            }
        }

    }
}

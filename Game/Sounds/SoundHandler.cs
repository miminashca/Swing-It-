using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class SoundHandler : GameObject
    {
        SoundChannel background = new Sound("Music/Background_Music.wav", true, false).Play();
        
        SoundChannel NextLevel;
        SoundChannel CheckpointSound;

        SoundChannel jumpPad;
        
        SoundChannel Damaged;
        SoundChannel jumping;

        EndPoint _endpoint;

        public SoundHandler()
        {
            
        }

        void Update()
        {
            _endpoint = MyGame.main.FindObjectOfType<EndPoint>();

            if (soundPlaying)
            {
                if (!NextLevel.IsPlaying && _endpoint != null)
                {
                    _endpoint.nextLevel = true;
                    background.IsPaused = false;
                    soundPlaying = false;
                }
            }
        }
        
        public void DamageSound()
        {
            Damaged = new Sound("Sounds/Enemy_damage.wav", false, false).Play();
        }

        public void Jump()
        {
            jumping = new Sound("Sounds/Jump.wav", false, false).Play();
        }

        public void JumpPad()
        {
            jumpPad = new Sound("Sounds/Jump_pad.wav", false, false).Play();
        }

        public void Checkpoint()
        {
            CheckpointSound = new Sound("Sounds/Checkpoint.wav", false, false).Play();
        }

        bool soundPlaying = false;
        public void EndSound()
        {
            background.IsPaused = true;

            if (background.IsPaused && !soundPlaying)
            {
                soundPlaying = true;
                NextLevel = new Sound("Music/Goal.wav", false, false).Play();
            }
        }
    }
}

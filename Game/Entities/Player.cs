using GXPEngine;
using GXPEngine.Core;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Threading;
using TiledMapParser;

namespace GXPEngine
{
    internal class Player : AnimationSprite
    {
        SoundHandler _soundHandler;

        //rope code
        // public Vec2 _playerPos
        // {
        //     get
        //     {
        //         return playerPos;
        //     }
        // }

        public bool _anchorActive
        {
            get
            {
                return anchorActive;
            }
        }
    
        // private Vec2 playerPos;
        public bool gravity=true;
	
        public VerletBody body;
        private Vec2 mousePos;
        private float elasticity = 10f;
    
        private Arrow arrow;
        Vec2 zero;
        private bool anchorActive = false;
        public VerletPoint anchorPoint;
        private VerletPoint playerPoint;
        private Anchor anchor;
        private float ropeLength;
        private List<PointSprite> pointSprites;

        private Level level;
        private Vec2 levelPos;
        // 
        
        public float _mass
        {
            get
            {
                return mass;
            }
        }

        bool onGround = true;
        private bool onWall = false;

        private float mass;

        public Vec2 position;
        public Vec2 oldPosition;

        public Vec2 velocity;
        
        int jumpForce;
        float speed;
        
        public int health;
        int startHealth;

        public int checkpoint = 0;
        public Vec2 spawn;

        public TiledObject properties;

        Vec2 startPos;

        bool idle = true;
        bool throwing = false;

        float gravityPlayer;

        public int collection = 0;

        private bool playerIsColliding = false;

        bool jumping = false;

        public Player(string filename, int c, int r, TiledObject data) : base(filename, c, r)
        {
            mass = 3f;

            position.x = data.X;
            position.y = data.Y;

            startPos = position;
            spawn = startPos;

            gravityPlayer = data.GetFloatProperty("gravity");
            health = data.GetIntProperty("health");
            speed = data.GetFloatProperty("Speed");
            jumpForce = data.GetIntProperty("jumpForce");
            properties = data;

            startHealth = health;
            
            //rope
            Vec2 start = new Vec2 (width/2, height/2);
            zero = new Vec2 (0, 0);
            arrow = new Arrow(start, zero, 1);
            AddChild(arrow);
            // playerPos = zero;
            anchorPoint = new VerletPoint(zero);
            ropeLength = 10 * 13;
            pointSprites = new List<PointSprite>();
            //

            _soundHandler = MyGame.main.FindObjectOfType<SoundHandler>();
        }
        
        protected override Collider createCollider()
        {
            EasyDraw baseShape = new EasyDraw(70,110);
            baseShape.SetXY(-40,-50);
            //baseShape.Clear(ColorTranslator.FromHtml("#55FF0000"));
            AddChild(baseShape);
            return new BoxCollider(baseShape);
        }

        public void Update()
        {
            // GameObject[] overlaps = GetOverlaps(velocity.x, velocity.y);
            if (_soundHandler == null) _soundHandler = MyGame.main.FindObjectOfType<SoundHandler>();

            level = MyGame.main.FindObjectOfType<Level>();
            if (level != null)
            {
                levelPos = new Vec2(level.x, level.y);
            }

            mousePos = new Vec2(Input.mouseX, Input.mouseY);
            if (!anchorActive)
            {
                Controls();
                UpdateScreenPosition();
            }
            onWall = false;

            if (!anchorActive)
            {
                Controls();
                UpdateScreenPosition();

                if (!jumping && onGround)
                {
                    Aim();
                }
            }
            if (anchorActive)
            {
                if (gravity) {
                    body.AddAcceleration(new Vec2(0, 0.9f));
                }
                body.UpdateVerlet ();
                body.UpdateConstraints ();
            
                Swing();
                MoveAnchor();
                ShrinkRope();
                if (anchor != null)
                {
                    anchor.position = anchorPoint.position-position;
                }
            }
            
            Activation();
            Animation();
        }

        void UpdateScreenPosition()
        {
            position.x = x;
            position.y = y;
            MoveUntilCollision(0, velocity.y);
            MoveUntilCollision(velocity.x, 0);
        }

        void Controls()
        {
            velocity *= 0.9f;
            Collision grounded = MoveUntilCollision(0, velocity.y);
            bool testGround = false;

            if (!anchorActive)
            {
                if (Input.GetKeyDown(Key.Q))
                {
                    health--;
                }

                if (grounded != null && !testGround)
                {
                    velocity.y = 0;
                    onGround = true;
                    testGround = true;
                } else
                {
                    velocity.y += gravityPlayer;
                }

                if (Input.GetKey(Key.R))
                {
                    x = spawn.x;
                    y = spawn.y;
                }

                if (Input.GetKey(Key.W) && grounded != null && !throwing && !jumping)
                {
                    jumping = true;
                    onGround = false;
                    testGround = false;
                    velocity += new Vec2(0, -jumpForce);
                    _soundHandler.Jump();
                }

                if (velocity.y > 1)
                {
                    jumping = false;
                }

                if (Input.GetKey(Key.A))
                {
                    idle = false;
                    velocity += new Vec2(-1, 0) * speed;
                }
                else if (Input.GetKey(Key.D))
                {
                    idle = false;
                    velocity += new Vec2(1, 0) * speed;
                }

                if (Input.AnyKey() == false)
                {
                    idle = true;
                }
            
                if (Input.GetKey(Key.SPACE))
                {
                    throwing = true;
                } else
                {
                    throwing = false;
                }

                if (velocity.x < 0.001f && velocity.x > -0.001f)
                {
                    velocity.x = 0;
                }
                if (velocity.y < 0.001f && velocity.y > -0.001f)
                {
                    velocity.y = 0;
                }
            }
        }

        void Activation()
        {
            int start = Time.time;
            GameObject[] collisions = GetCollisions();
            //Console.WriteLine("Player.Activation() got collisions in {0} ms", Time.time - start);

            var point = collisions.FirstOrDefault(x => x is CheckPoint) as CheckPoint;
            if (point != null)
            {
                if (checkpoint < point.properties.GetIntProperty("checkpoint"))
                {
                    checkpoint = point.properties.GetIntProperty("checkpoint");
                    spawn.x = (int)point.x;
                    spawn.y = (int)point.y-height/4;
                    point.soundPlayed = false;
                    point.SetCycle(1);

                    if (!point.soundPlayed)
                    {
                        _soundHandler.Checkpoint();
                        point.soundPlayed = true;
                    }
                }
            }


            var bounce = collisions.FirstOrDefault(x => x is BouncePads) as BouncePads;
            if (bounce != null)
            {
                if (y + height / 2 < bounce.y && velocity.y > 0)
                {
                    velocity += bounce.velocity;
                    bounce.animation = true;
                }
            }
            
            var lever = collisions.FirstOrDefault(x => x is Lever) as Lever;
            if (lever != null)
            {
                if (!lever.pulled)
                {
                    lever.pulled = true;
                    lever.openDoor();
                }
            }

            var collect = collisions.FirstOrDefault(x => x is Collectible) as Collectible;
            if (collect != null)
            {
                collection++;
                collect.Destroy();
            }

            var kill = collisions.FirstOrDefault(x => x is ColBlock) as ColBlock;
            if (kill != null && kill.killZone)
            {
                Respawn();
            }

            var wall = collisions.FirstOrDefault(x => x is ColBlock) as ColBlock;
            if (wall != null && wall.isWall)
            {
                playerIsColliding = true;
            }
        }

        void Animation()
        {
            if (idle)
            {
                SetCycle(0, 2);
            }
            if (!idle && !throwing)
            {
                SetCycle(2, 4);
            }
            if (throwing)
            {
                SetCycle(6, 1);
            }

            if (anchorActive)
            {
                SetCycle(7);
            }

            Animate(0.05f);

            if (velocity.x < 0)
            {
                Mirror(false, false);
            } else if (velocity.x > 0)
            {
                Mirror(true, false);
            }
        }

        void Respawn()
        {
            health = startHealth;
            x = spawn.x;
            y = spawn.y;

            Lever[] levers = Level.main.FindObjectsOfType<Lever>();
            foreach (Lever lever in levers)
            {
                lever.pulled = false;
                lever.closeDoor();
            }
        }

        int timer = 0;
        bool setTimer = false;
        public void DamagePlayer()
        {
            if (timer + 1000 < Time.time)
            {
                health--;
                setTimer = false;
                _soundHandler.DamageSound();
            }

            if (!setTimer)
            {
                timer = Time.time;
                setTimer = true;
            }

            if (health == 0)
            {
                Respawn();
            }
        }
        
        
        //rope
        public void Aim()
        {
            if (Input.GetKey(Key.SPACE))
            {
                arrow.startPoint = this.position + levelPos;
                if ((mousePos - (this.position + levelPos)).Length() > ropeLength)
                {
                    arrow.vector = (mousePos - (this.position + levelPos)).Normalized() * ropeLength;
                }
                else
                {
                    arrow.vector = (mousePos - (this.position + levelPos));
                }
            }

            if (Input.GetKeyUp(Key.SPACE))
            {
                arrow.vector = zero;
                ReleaseRope();
            }
            
        }

        void ReleaseRope() {
            if (arrow.vector.Length() != 0)
            {
                arrow.vector = zero;
            }
            anchorActive = true;
            
            body = new VerletBody(this);
            // Rope with one fixed point
            for (int i = 0; i < 28; i++)
            {
                body.AddPoint(new VerletPoint(position.x, position.y - i * 10, i == 0));
                for (int j = 0; j < i; j++)
                {
                    //body.AddConstraint(i - 1, i, 1.1f, false);
                    body.AddConstraint(j, i);
                }
            }

            foreach (VerletPoint p in body.point)
            {
                pointSprites.Add(new PointSprite(p, this));
            }

            foreach (PointSprite sprite in pointSprites)
            {
                AddChild(sprite);
            }

            AddChild(body);
            body.point[0]._playerPoint = true;
            body.point[body.point.Count - 1]._anchorPoint = true;

            anchor = new Anchor();
            AddChild(anchor);
                
            anchorPoint = new VerletPoint(zero);
            playerPoint = new VerletPoint(zero);
            
            foreach (VerletPoint p in body.point)
            {
                if (p._anchorPoint)
                {
                    anchorPoint = p;
                }

                if (p._playerPoint)
                {
                    playerPoint = p;
                }
            }
            
            Vec2 velocityDir = (mousePos-levelPos - anchorPoint.position);
            anchorPoint.acceleration += velocityDir;
        }
        
        void Swing() 
        {
            if (body != null)
            {
                VerletPoint swingPoint = null;
                if (!anchorPoint._fixed)
                {
                    swingPoint = anchorPoint;
                }
                else if (!playerPoint._fixed)
                {
                    swingPoint = playerPoint;
                }
            
                if (Input.GetKey(Key.A))
                {
                    swingPoint.acceleration.x-= 5;
                    swingPoint.acceleration.y-= 3;
                }
                else if (Input.GetKey(Key.D))
                {
                    // swingSpeed++;
                    swingPoint.acceleration.x+= 5;
                    swingPoint.acceleration.y-= 3;
                }
            }
        }
        void MoveAnchor()
        {
            if (playerPoint._fixed)
            {
                if (anchor._isOnWall)
                {
                    Console.WriteLine("fixed");
                    playerPoint._fixed ^= true;
                    anchorPoint._fixed ^= true;
                }
            }

            Vec2 oldVelosity = zero;
            
            if (playerIsColliding)
            {
                oldVelosity = playerPoint.velocity;
                Console.WriteLine("coll");
                // playerPoint.acceleration = oldVelosity * -1;
                // playerPoint.position += playerPoint.acceleration*5;
                playerPoint.position -= oldVelosity*5;
                playerIsColliding = false;
            }
            
            position = playerPoint.position;
            x = position.x;
            y = position.y;
        }
        private void ShrinkRope()
        {
            if (anchorActive)
            {
                if (Input.GetKeyUp(Key.W))
                {
                    if (body != null)
                    {
                        body.Destroy();
                    }
                    if (anchor != null)
                    {
                        anchor.Destroy();
                    }
                    foreach (PointSprite sprite in pointSprites)
                    {
                        sprite.Destroy();
                    }
                    pointSprites = new List<PointSprite>();
                
                    _soundHandler.Jump();
                    // if (MoveUntilCollision(0, velocity.y) != null)
                    // {
                    //     velocity += new Vec2(0, -jumpForce);
                    // }
                    // else
                    // {
                        velocity.x += playerPoint.acceleration.x*2;
                        //velocity.y += playerPoint.acceleration.y*3;
                   // }
                
                    anchorActive = false;
                }
            }
        }
    }
}

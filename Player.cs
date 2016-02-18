using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MummyDispair
{
    class Player : TypeComponent, ILoadable, IUpdateable, ICollisionStay, ICollisionEnter
    {
        private AnimationStrategy strategy;
        private Direction direction;
        private float speed;
        private bool walking;
        private Animator animator;
        private Collider collider;

        private bool grounded;
        private Vector2 translation;
        private float force;

        SoundEffect mummyJump;
        SoundEffectInstance mummyJumpInstance;
        SoundEffect mummyDamage;
        SoundEffectInstance mummyDamageInstance;

        HealthBar healthBar;

        public Player(GameObject gameObject, int speed) : base(gameObject)
        {
            this.speed = speed;
            direction = (Direction.Right);
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");
            this.collider = (Collider)gameObject.GetComponent("Collider");
            this.healthBar = (HealthBar)gameObject.GetComponent("HealthBar");

            CreateAnimations();
            strategy = new Idle(animator);
            strategy.Update(direction, Vector2.Zero);

            mummyJump = content.Load<SoundEffect>("Sound/JumpSound");
            mummyJumpInstance = mummyJump.CreateInstance();
            mummyJumpInstance.Volume = 0.05f;

            mummyDamage = content.Load<SoundEffect>("Sound/deathSound");
            mummyDamageInstance = mummyDamage.CreateInstance();
            mummyDamageInstance.Volume = 0.3f;
        }

        /// <summary>
        /// https://www.youtube.com/watch?v=IysShLIaosk
        /// The wall collision is inspired partly by the link above:
        /// </summary>
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            translation = Vector2.Zero;

            if (MeetingWall(0, 1))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            //Check for movement.
            if (keyState.IsKeyDown(Keys.D) && !keyState.IsKeyDown(Keys.A))
            {
                walking = true;
                direction = (Direction.Right);
                translation.X = 1;
            }
            else if (keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
            {
                walking = true;
                direction = (Direction.Left);
                translation.X = -1;
            }
            else
            {
                walking = false;
                translation.X = 0;
            }

            //Checks for platform fallthrough
            if (keyState.IsKeyDown(Keys.S) && grounded)
            {
                if (PlatformBelow())
                {
                    gameObject.Transformer.Translate(new Vector2(0, 2));
                }
            }

            //Check for jump.
            if (!grounded && translation.Y < 4)
            {
                force += 0.13f;
            }

            if (grounded)
            {
                if (keyState.IsKeyDown(Keys.Space))
                {
                    grounded = false;
                    force = -4.3f;
                    mummyJumpInstance.Play();
                }
            }

            //Chosing the correct strategy.
            if (!grounded)
            {
                if (!(strategy is Jump))
                {
                    strategy = new Jump(animator, gameObject.Transformer);
                }
            }
            else if (walking)
            {
                if (!(strategy is Walk))
                {
                    strategy = new Walk(animator, gameObject.Transformer);
                }
            }
            else
            {
                if (!(strategy is Idle))
                {
                    strategy = new Idle(animator);
                }
            }

            translation.Y = force;
            translation = translation * GameWorld.Instance.DeltaTime * speed;

            if (MeetingWall((int)translation.X, 0))
            {
                while (!MeetingWall(Math.Sign(translation.X), 0))
                {
                    gameObject.Transformer.Translate(new Vector2(Math.Sign(translation.X), 0));
                }
                translation.X = 0;
            }

            if (MeetingWall(0, (int)translation.Y))
            {
                while (!MeetingWall(0, Math.Sign(translation.Y)))
                {
                    gameObject.Transformer.Translate(new Vector2(0, Math.Sign(translation.Y)));
                }
                translation.Y = 0;
                force = 0;
            }

            strategy.Update(direction, translation);
        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("WalkRight", new Animation(7, 660, 0, 166, 165, 8.5f, Vector2.Zero, true));
            animator.CreateAnimation("WalkLeft", new Animation(7, 825, 0, 166, 165, 8.5f, Vector2.Zero, true));

            animator.CreateAnimation("JumpRight", new Animation(4, 330, 0, 166, 165, 11f, Vector2.Zero, false));
            animator.CreateAnimation("JumpLeft", new Animation(4, 495, 0, 166, 165, 11f, Vector2.Zero, false));

            animator.CreateAnimation("IdleRight", new Animation(8, 0, 0, 166, 165, 4f, Vector2.Zero, true));
            animator.CreateAnimation("IdleLeft", new Animation(8, 165, 0, 166, 165, 4f, Vector2.Zero, true));
        }

        //Checks if there is a platform below the player, without a wall blocking.
        private bool PlatformBelow()
        {
            Rectangle rect = new Rectangle(collider.CollisionBox.X, collider.CollisionBox.Y + 1,
                collider.CollisionBox.Width, collider.CollisionBox.Height);

            bool platformBelow = false;

            //Makes sure there is no wall in the way.
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other.GetGameObject.TypeComponent is Wall || other.GetGameObject.TypeComponent is DartShooter)
                {
                    if (rect.Intersects(other.CollisionBox))
                    {
                        return false;
                    }
                }
                if (other.GetGameObject.TypeComponent is Platform)
                {
                    if (rect.Intersects(other.CollisionBox))
                    {
                        platformBelow = true;
                    }
                }
            }

            return platformBelow;
        }
        
        //Checks for solid ground below the player.
        private bool MeetingWall(int x, int y)
        {
            Rectangle rect = new Rectangle(collider.CollisionBox.X + x, collider.CollisionBox.Y + y,
                collider.CollisionBox.Width, collider.CollisionBox.Height);

            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other.GetGameObject.TypeComponent is Wall || other.GetGameObject.TypeComponent is DartShooter)
                {
                    if (rect.Intersects(other.CollisionBox))
                    {
                        return true;
                    }
                }
                if (other.GetGameObject.TypeComponent is Platform && force >= 0 &&
                    collider.CollisionBox.Y + collider.CollisionBox.Height < other.CollisionBox.Y + 1)
                {
                    if (rect.Intersects(other.CollisionBox))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void OnCollisionStay(Collider other)
        {
            //Unstuck function.
            if (other.GetGameObject.TypeComponent is Wall || other.GetGameObject.TypeComponent is DartShooter)
            {
                Vector2 newPos = FindGoodPosition(0, 0);
                gameObject.Transformer.Translate(newPos);
            }

            else if (other.GetGameObject.TypeComponent is Scorpion)
            {
                TakeDamage(1);
            }
        }

        //If the player is stuck, we find the nearest free position.
        private Vector2 FindGoodPosition(int x, int y)
        {
            for (int i = -y; i <= y; i++)
            {
                for (int j = -x; j <= x; j++)
                {
                    if (!MeetingWall(i, j))
                    {
                        //Sometimes lag (i assume) causes the player to get stuck.
                        System.Diagnostics.Debug.WriteLine("Unstuck with new translation: " + i + ", " + j);
                        return new Vector2(i, j);
                    }
                }
            }
            return FindGoodPosition(x + 1, y + 1);
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GetGameObject.TypeComponent is ToiletPaper)
            {
                other.GetGameObject.IsAlive = false;
                healthBar.Life += 1;
            }

            else if (other.GetGameObject.TypeComponent is Dart)
            {
                 other.GetGameObject.IsAlive = false;
                 TakeDamage(1);
            }
        }

        private void TakeDamage(int damage)
        {
            if (mummyDamageInstance.State.ToString() == "Stopped")
            {
                mummyDamageInstance.Play();
                healthBar.Life -= damage;
            }
        }
    }
}

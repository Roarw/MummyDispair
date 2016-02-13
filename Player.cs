using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MummyDispair
{
    class Player : Component, ILoadable, IUpdateable, IAnimateable, ICollisionStay, ICollisionEnter, ICollisionExit
    {
        private AnimationStrategy strategy;
        private Direction direction;
        private float speed;
        private bool walking;
        private Animator animator;
        private float force;
        private bool jumpReady;

        public Animator Animator
        {
            get
            {
                return animator;
            }
        }

        public Player(GameObject gameObject, int speed) : base(gameObject)
        {
            this.speed = speed;
            direction = (Direction.Right);
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            strategy = new Idle(animator);
            strategy.Update(direction, Vector2.Zero);
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            //Check for movement.
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.A))
            {
                walking = true;
                
                if (keyState.IsKeyDown(Keys.D))
                {
                    direction = (Direction.Right);
                    translation += new Vector2(1, 0);
                }
                else if (keyState.IsKeyDown(Keys.A))
                {
                    direction = (Direction.Left);
                    translation += new Vector2(-1, 0);
                }
            }
            else
            {
                walking = false;
            }

            //Check for jump.
            if (gameObject.Transformer.Position.Y > 300 && !jumpReady) 
            {
                force = 0;
                jumpReady = true;
            }
            else if (!jumpReady)
            {
                force += 0.13f;
            }

            if (keyState.IsKeyDown(Keys.Space) && jumpReady)
            {
                jumpReady = false;
                force -= 4.3f;
            }

            translation += new Vector2(0, force);

            //Chosing the correct strategy.
            if (!jumpReady)
            {
                if (!(strategy is Jump))
                {
                    strategy = new Jump(animator, gameObject.Transformer, speed);
                }
            }
            else if (walking)
            {
                if (!(strategy is Walk))
                {
                    strategy = new Walk(animator, gameObject.Transformer, speed);
                }
            }
            else
            {
                if (!(strategy is Idle))
                {
                    strategy = new Idle(animator);
                }
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

        public void OnAnimationDone(string animationName)
        {
        }

        public void OnCollisionStay(Collider other)
        {
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GetGameObject.GetComponent("Wall") != null)
            {
                force = 0;
                jumpReady = true;
            }
        }

        public void OnCollisionExit(Collider other)
        {
        }
    }
}

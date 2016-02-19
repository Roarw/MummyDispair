using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MummyDispair
{
    class Scorpion : TypeComponent, ILoadable, IUpdateable
    {
        private Animator animator;
        private Vector2 startPosition;
        private int patrolX;
        private Direction direction;
        private AnimationStrategy strategy;

        private int speed;
        private bool patrolPointRight;
        private Vector2 otherPosition;
        private Vector2 firstPosition;
        private bool changeDirection;

        public Scorpion(GameObject gameObject, Vector2 startPosition, int patrolX) : base(gameObject)
        {
            this.startPosition = startPosition;
            this.patrolX = patrolX;

            speed = 120;
            otherPosition = new Vector2(gameObject.Transformer.Position.X + patrolX, gameObject.Transformer.Position.Y);
            firstPosition = startPosition;
            changeDirection = false;


            if (gameObject.Transformer.Position.X < otherPosition.X)
            {
                patrolPointRight = true;
                direction = (Direction.Right);
            }
            else
            {
                patrolPointRight = false;
                direction = (Direction.Left);
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();

            strategy = new Walk(animator, gameObject.Transformer);
            strategy.Update(direction, Vector2.Zero);
        }

        public void Update()
        {
            if (patrolPointRight)
            {
                if (gameObject.Transformer.Position.X < firstPosition.X ||
                    gameObject.Transformer.Position.X > otherPosition.X)
                {
                    changeDirection = true;
                }
            }
            else 
            {
                if (gameObject.Transformer.Position.X > firstPosition.X ||
                    gameObject.Transformer.Position.X < otherPosition.X)
                {
                    changeDirection = true;
                }
            }

            if (changeDirection)
            {
                if (direction == Direction.Right)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Right;
                }
                changeDirection = false;
            }

            Vector2 translation = Vector2.Zero;

            if (direction == Direction.Right)
            {
                translation += new Vector2(1, 0);
            }
            else
            {
                translation += new Vector2(-1, 0);
            }

            translation = translation * GameWorld.Instance.DeltaTime * speed;
            
            if (!(strategy is Walk))
            {
                strategy = new Walk(animator, gameObject.Transformer);
            }
            strategy.Update(direction, translation);
        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("AttackRight", new Animation(6, 0, 0, 89, 75, 11f, Vector2.Zero, true));
            animator.CreateAnimation("AttackLeft", new Animation(6, 75, 0, 89, 75, 11f, Vector2.Zero, true));

            animator.CreateAnimation("WalkRight", new Animation(8, 150, 0, 89, 75, 8.5f, Vector2.Zero, true));
            animator.CreateAnimation("WalkLeft", new Animation(8, 225, 0, 89, 75, 8.5f, Vector2.Zero, true));
        }


    }
}

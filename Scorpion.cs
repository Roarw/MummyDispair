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

        public Scorpion(GameObject gameObject, Vector2 startPosition, int patrolX) : base(gameObject)
        {
            direction = (Direction.Right);
            this.startPosition = startPosition;
            this.patrolX = patrolX;
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            animator.PlayAnimation("WalkRight");

            strategy = new Walk(animator, gameObject.Transformer);
            strategy.Update(direction, Vector2.Zero);
        }

        public void Update()
        {
            
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

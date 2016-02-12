using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Jump : AnimationStrategy
    {
        private Transform transform;
        private float speed;
        private Animator animator;

        public Jump(Animator animator, Transform transform, float speed)
        {
            this.animator = animator;
            this.transform = transform;
            this.speed = speed;
        }

        public void Update(Direction direction, Vector2 translation)
        {
            switch (direction)
            {
                case Direction.Right:
                    animator.PlayAnimation("JumpRight");
                    break;
                case Direction.Left:
                    animator.PlayAnimation("JumpLeft");
                    break;
            }

            transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
        }
    }
}

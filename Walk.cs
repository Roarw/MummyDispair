using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Walk : IStrategy
    {
        private Transform transform;
        private float speed;
        private Animator animator;


        public Walk(Animator animator, Transform transform, float speed)
        {
            this.animator = animator;
            this.transform = transform;
            this.speed = speed;
        }

        public void Update(Direction direction, Vector2 translation)
        {
            /* switch (direction)
             {
                 case Direction.Front:
                     animator.PlayAnimation(AnimationName.WalkFront);
                     break;
                 case Direction.Back:
                     animator.PlayAnimation(AnimationName.WalkBack);
                     break;
                 case Direction.Right:
                     animator.PlayAnimation(AnimationName.WalkRight);
                     break;
                 case Direction.Left:
                     animator.PlayAnimation(AnimationName.WalkLeft);
                     break;
             }*/

            transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
        }
    }
}


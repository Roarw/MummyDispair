using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Idle : IStrategy
    {
        private Animator animator;

        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        public void Update(Direction direction, Vector2 translation)
        {
            switch (direction)
            {
                case Direction.Right:
                    animator.PlayAnimation("IdleRight");
                    break;
                case Direction.Left:
                    animator.PlayAnimation("IdleLeft");
                    break;
            }
        }
    }
}

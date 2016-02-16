using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Female : TypeComponent, ILoadable
    {
        private Animator animator;

        public Female(GameObject gameObject) : base(gameObject)
        {
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            animator.PlayAnimation("Idle");
        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("Idle", new Animation(6, 0, 0, 68, 148, 7.5f, Vector2.Zero, true));
        }
    }
}

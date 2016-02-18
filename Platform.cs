using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class Platform : TypeComponent, ILoadable
    {
        private Animator animator;

        public Platform(GameObject gameObject) : base(gameObject)
        {
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            animator.PlayAnimation("Standard");
        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("Standard", new Animation(1, 0, 0, 100, 30, 0, Vector2.Zero, true));
        }
    }
}

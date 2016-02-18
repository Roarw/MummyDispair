using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class DartShooter : TypeComponent, ILoadable, IUpdateable
    {
        private Animator animator;
        private bool shootRight;

        public DartShooter(GameObject gameObject, bool shootRight) : base(gameObject)
        {
            this.shootRight = shootRight;
        }

        public void LoadContent(ContentManager content)
        {
        this.animator = (Animator)gameObject.GetComponent("Animator");

        CreateAnimations();
        animator.PlayAnimation("Standard");
        }

        public void Update()
        {
            
        }

        private void CreateAnimations()
        {
        animator.CreateAnimation("Standard", new Animation(1, 0, 0, 105, 100, 0, Vector2.Zero, true));
        }
    }
}

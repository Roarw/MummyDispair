using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MummyDispair
{
    class Scorpion : TypeComponent, ILoadable
    {
        private Animator animator;
        private Vector2 coord1;
        private Vector2 coord2;

        public Scorpion(GameObject gameObject, Vector2 coord1, Vector2 coord2) : base(gameObject)
        {
            this.coord1 = coord1;
            this.coord2 = coord2;

        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            animator.PlayAnimation("Standard");
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

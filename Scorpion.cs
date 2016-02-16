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
            animator.CreateAnimation("WalkRight", new Animation(7, 660, 0, 166, 165, 8.5f, Vector2.Zero, true));
            animator.CreateAnimation("WalkLeft", new Animation(7, 825, 0, 166, 165, 8.5f, Vector2.Zero, true));

            animator.CreateAnimation("AttackRight", new Animation(4, 330, 0, 166, 165, 11f, Vector2.Zero, false));
            animator.CreateAnimation("AttackLeft", new Animation(4, 495, 0, 166, 165, 11f, Vector2.Zero, false));
        }
    }
}

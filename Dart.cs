using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Dart : TypeComponent, ILoadable, IUpdateable, ICollisionStay, ICollisionEnter, ICollisionExit
    {

        private Animator animator;
        private int speed;

        public Dart(GameObject gameObject, int speed) : base(gameObject)
        {
            this.speed = speed;
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            animator.PlayAnimation("Standard");
        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("Standard", new Animation(1, 0, 0, 76, 20, 0, Vector2.Zero, true));
        }

        public void Update()
        {
            Vector2 translation = Vector2.Zero;

            translation += new Vector2(speed, 0);
            translation = translation * GameWorld.Instance.DeltaTime;

            gameObject.Transformer.Translate(translation);
        }

        public void OnCollisionEnter(Collider other)
        {
        }

        public void OnCollisionExit(Collider other)
        {
        }

        public void OnCollisionStay(Collider other)
        {
            if (other.GetGameObject.TypeComponent is Wall)
            {
                gameObject.IsAlive = false;
            }
        }

        
    }
}

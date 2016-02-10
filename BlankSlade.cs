using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MummyDispair
{
    class BlankSlade : Component
    {
        private IStrategy strategy;
        private Direction direction;
        private float speed;
        private bool walking;
        private Animator animator;

        public Animator Animator
        {
            get
            {
                return animator;
            }
        }

        public BlankSlade(GameObject gameObject, int speed) : base(gameObject)
        {
            this.speed = speed;
            direction = (Direction.Right);
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            // CreateAnimations();
            strategy = new Idle(animator);
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            //Check for movement.
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.A) ||
                keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.W))
            {
                walking = true;
                
                if (keyState.IsKeyDown(Keys.D))
                {
                    direction = (Direction.Right);
                    translation += new Vector2(1, 0);
                }
                else if (keyState.IsKeyDown(Keys.A))
                {
                    direction = (Direction.Left);
                    translation += new Vector2(-1, 0);
                }
            }
            else
            {
                walking = false;
            }

            /*if (keyState.IsKeyDown(Keys.Space))
            {
                attacking = true;
            }*/

            //Strategies
            /* if (dying)
             {
                 if (!(strategy is Die))
                 {
                     strategy = new Die(animator);
                 }
             }*/
            if (walking)
            {
                if (!(strategy is Walk))
                {
                    strategy = new Walk(animator, (Transform)gameObject.GetComponent("Transform"), speed);
                }
            }
            else
            {
                if (!(strategy is Idle))
                {
                    strategy = new Idle(animator);
                }
            }

            strategy.Update(direction, translation);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MummyDispair
{
    class DartShooter : TypeComponent, ILoadable, IUpdateable
    {
        private Animator animator;
        private bool shootRight;
        private float interval;
        private Director dir;
        private ContentManager content;

        SoundEffect shootDart;
        SoundEffectInstance shootDartInstance;

        GameObject playerObject;

        public DartShooter(GameObject gameObject, bool shootRight, float interval, GameObject playerObject) : base(gameObject)
        {
            this.shootRight = shootRight;
            this.interval = interval;

            if (shootRight)
            {
                dir = new Director(new DartBuilder(160));
            }
            else
            {
                dir = new Director(new DartBuilder(-160));
            }

            this.playerObject = playerObject;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            this.animator = (Animator)gameObject.GetComponent("Animator");
        
            CreateAnimations();
            animator.PlayAnimation("Standard");

            shootDart = content.Load<SoundEffect>("Sound/blowpipeDarts");
            shootDartInstance = shootDart.CreateInstance();
        }

        public void Update()
        {
            interval += GameWorld.Instance.DeltaTime * 10;

            if (interval > 60)
            {
                interval = 0;

                if (!shootRight)
                {
                    RunBuilder(new Vector2(gameObject.Transformer.Position.X, gameObject.Transformer.Position.Y + 40));
                }
                else
                {
                    RunBuilder(new Vector2(gameObject.Transformer.Position.X + 24, gameObject.Transformer.Position.Y + 40));
                }
            }

        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("Standard", new Animation(1, 0, 0, 105, 100, 0, Vector2.Zero, true));
        }

        private GameObject RunBuilder(Vector2 position)
        {
            if (playerObject != null)
            {
                //Reducing sound manually (because could not be assed to use an emitter).
                if (Distance(gameObject.Transformer.Position.X, playerObject.Transformer.Position.X,
                    gameObject.Transformer.Position.Y, playerObject.Transformer.Position.Y) < 300)
                {
                    shootDartInstance.Volume = 0.3f;
                }
                else if (Distance(gameObject.Transformer.Position.X, playerObject.Transformer.Position.X,
                    gameObject.Transformer.Position.Y, playerObject.Transformer.Position.Y) < 700)
                {
                    shootDartInstance.Volume = 0.2f;
                }
                else if (Distance(gameObject.Transformer.Position.X, playerObject.Transformer.Position.X,
                    gameObject.Transformer.Position.Y, playerObject.Transformer.Position.Y) < 1100)
                {
                    shootDartInstance.Volume = 0.1f;
                }
                else
                {
                    shootDartInstance.Volume = 0.0f;
                }
            }

            shootDartInstance.Play();

            GameObject dart = dir.Construct(position);
            dart.LoadContent(content);
            GameWorld.Instance.Objects.Add(dart);

            return dart;
        }

        private float Distance(float x1, float x2, float y1, float y2)
        {
            float x = Math.Abs(x1 - x2);
            float y = Math.Abs(y1 - y2);

            return x + y;
        }
    }
}

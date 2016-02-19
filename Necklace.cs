using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class Necklace : TypeComponent, ILoadable, ICollisionEnter
    {
        private Animator animator;

        SoundEffect doorSound;
        SoundEffectInstance doorSoundInstance;

        public Necklace(GameObject gameObject) : base(gameObject)
        {
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)gameObject.GetComponent("Animator");

            CreateAnimations();
            animator.PlayAnimation("Standard");

            doorSound = content.Load<SoundEffect>("Sound/doors");
            doorSoundInstance = doorSound.CreateInstance();
            doorSoundInstance.Volume = 0.55f;
        }

        public void OnCollisionEnter(Collider other)
        {
            doorSoundInstance.Play();
            gameObject.IsAlive = false;
            GameWorld.Instance.NextLevel(gameObject);
        }

        private void CreateAnimations()
        {
            animator.CreateAnimation("Standard", new Animation(1, 0, 0, 67, 76, 0, Vector2.Zero, true));
        }
    }
}

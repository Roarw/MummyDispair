using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Animator: Component, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex;
        private float timeElapsed;
        private float fps;
        private Rectangle[] rectangles;
        private string nameOfAnimation;
        private Dictionary<string, Animation> animations;

        public string NameOfAnimation
        {
            get
            {
                return nameOfAnimation;
            }
        }

        public Dictionary<string, Animation> Animations
        {
            get
            {
                return animations;
            }
        }

        public Animator(GameObject gameObject): base(gameObject)
        {
            this.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            this.animations = new Dictionary<string, Animation>();
        }

        public void Update()
        {
            timeElapsed += GameWorld.Instance.DeltaTime;

            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                if (animations[nameOfAnimation].Repeat)
                {
                    timeElapsed = 0;
                    currentIndex = 0;
                    gameObject.OnAnimationDone(nameOfAnimation);
                }
                else
                {
                    currentIndex = rectangles.Length - 1;
                }
            }

            spriteRenderer.Rectangle = rectangles[currentIndex];

        }

        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }

        public void PlayAnimation(string nameOfAnimation)
        {
            //Checks if it’s a new animation
            if (this.nameOfAnimation != nameOfAnimation)
            {
                //Sets the rectangles
                this.rectangles = animations[nameOfAnimation].Rectangles;
                this.spriteRenderer.Offset = animations[nameOfAnimation].Offset;
                string oldNameOfAnimation = this.nameOfAnimation;
                this.nameOfAnimation = nameOfAnimation;
                this.fps = animations[nameOfAnimation].Fps;
                
                if (oldNameOfAnimation != null && 
                    oldNameOfAnimation.Substring(0, 4) != oldNameOfAnimation.Substring(0, 4))
                {
                    timeElapsed = 0;
                    currentIndex = 0;
                    this.spriteRenderer.Rectangle = rectangles[currentIndex];
                }
            }
        }
    }
}

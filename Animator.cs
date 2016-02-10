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
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public Animator(GameObject gameObject): base(gameObject)
        {
            fps = 6;
             
           
        }
        public void Update()
        {
            timeElapsed += GameWorld.Instance.DeltaTime;
            currentIndex = (int)(timeElapsed * fps);
            if (currentIndex > rectangles.Length - 1)
            {
                gameObject.OnAnimationDone(nameOfAnimation);
                timeElapsed = 0;
                currentIndex = 0;
            }

            spriteRenderer.Rectangle = rectangles[currentIndex];

        }
        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }
        public void PlayAnimation(string animationName)
        {
            if(nameOfAnimation != animationName)
            {
                rectangles = animations[animationName].GetRectangles();
                spriteRenderer.Rectangle = rectangles[0];
                spriteRenderer.Offset = animations[animationName].GetOffset();
                nameOfAnimation = animationName;
                fps = animations[animationName].getFps();
                timeElapsed = 0;
                currentIndex = 0;
            }
        }
    }
}

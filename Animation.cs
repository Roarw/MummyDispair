using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Animation
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangles;

        public float getFps()
        {
            return fps;
        }
        public Vector2 GetOffset()
        {
            return offset;
        }
        public Rectangle[] GetRectangles()
        {
            return rectangles;
        }
        public Animation(int frames, int yPos, int StartFrame, int width, int height, float fps, Vector2 offset)
        {
            rectangles = new Rectangle[frames];

            this.offset = offset;

            this.fps = fps;

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + StartFrame) * width, yPos, width, height);
            }
        }
    }
}

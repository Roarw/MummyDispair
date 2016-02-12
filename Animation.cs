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
        private int frames;
        private bool repeat;

        public float Fps
        {
            get
            {
                return fps;
            }
        }
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }
        public Rectangle[] Rectangles
        {
            get
            {
                return rectangles;
            }
        }
        public int Frames
        {
            get
            {
                return frames;
            }
        }
        public bool Repeat
        {
            get
            {
                return repeat;
            }
        }

        public Animation(int frames, int yPos, int StartFrame, int width, int height, float fps, Vector2 offset, bool repeat)
        {
            this.rectangles = new Rectangle[frames];
            this.offset = offset;
            this.fps = fps;
            this.frames = frames;
            this.repeat = repeat;

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + StartFrame) * width, yPos, width, height);
            }
        }
    }
}

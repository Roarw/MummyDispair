using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class Collider : Component, ILoadable, IUpdateable, IDrawable
    {
        private SpriteRenderer spriteRenderer;
        private Texture2D texture;
        private bool doCollisionChecks;
        private List<Collider> otherColliders;
        private Animator animator;
        private Lazy<Dictionary<string, Color[][]>> pixels;

        public Color[] CurrentPixels
        {
            get
            {
                int frames = animator.Animations[animator.NameOfAnimation].Frames - 1;
                return pixels.Value[animator.NameOfAnimation][frames];
            }
        }

        public List<Collider> OtherColliders
        {
            get
            {
                return otherColliders;
            }
        }

        public bool DoCollisionChecks
        {
            set
            {
                doCollisionChecks = value;
            }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(gameObject.Transformer.Position.X + spriteRenderer.Offset.X),
                    (int)(gameObject.Transformer.Position.Y + spriteRenderer.Offset.Y),
                    spriteRenderer.Rectangle.Width, spriteRenderer.Rectangle.Height);

            }
        }

        public Collider(GameObject gameObject) : base(gameObject)
        {
            GameWorld.Instance.Colliders.Add(this);
            this.otherColliders = new List<Collider>();
        }

        public void LoadContent(ContentManager content)
        {
            spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            animator = (Animator)gameObject.GetComponent("Animator");
            texture = content.Load<Texture2D>("Textures/CollisionTexture");

            pixels = new Lazy<Dictionary<string, Color[][]>>(() => CachePixels());
        }

        public void Update()
        {
            if (doCollisionChecks)
            {
                CheckCollision();
            }
        }

        //public Rectangle GetSideRectangle(string side)
        //{
        //    if (side == "Top")
        //    {
        //        return new Rectangle(CollisionBox.Left, 
        //            CollisionBox.Top, CollisionBox.Right, 1);
        //    }
        //    else if (side == "Bottom")
        //    {
        //        return new Rectangle(CollisionBox.Left,
        //            1, CollisionBox.Right, CollisionBox.Bottom);
        //    }
        //    else if (side == "Left")
        //    {
        //        return new Rectangle(CollisionBox.Left,
        //            CollisionBox.Top, 1, CollisionBox.Bottom);
        //    }
        //    else if (side == "Right")
        //    {
        //        return new Rectangle(1, CollisionBox.Top, 
        //            CollisionBox.Right, CollisionBox.Bottom);
        //    }
        //    else
        //    {
        //        return CollisionBox;
        //    }
        //}

        private void CheckCollision()
        {
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other != this)
                {
                    if (CollisionBox.Intersects(other.CollisionBox) && CheckPixelCollision(other))
                    {
                        gameObject.OnCollisionStay(other);

                        if (!otherColliders.Contains(other))
                        {
                            otherColliders.Add(other);
                            gameObject.OnCollisionEnter(other);
                        }
                    }
                    else
                    {
                        if (otherColliders.Contains(other))
                        {
                            gameObject.OnCollisionExit(other);
                            otherColliders.Remove(other);
                        }
                    }
                }
            }
        }

        private bool CheckPixelCollision(Collider other)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(CollisionBox.Top, other.CollisionBox.Top);
            int bottom = Math.Min(CollisionBox.Bottom, other.CollisionBox.Bottom);
            int left = Math.Max(CollisionBox.Left, other.CollisionBox.Left);
            int right = Math.Min(CollisionBox.Right, other.CollisionBox.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int firstIndex = (x - CollisionBox.Left) + (y - CollisionBox.Top) * CollisionBox.Width;
                    int secondIndex = (x - other.CollisionBox.Left) + (y - other.CollisionBox.Top) * other.CollisionBox.Width;
                    //Get the color of both pixels at this point
                    Color colorA = CurrentPixels[firstIndex];
                    if (secondIndex < other.CurrentPixels.Count())
                    {
                        Color colorB = other.CurrentPixels[secondIndex];
                        // If both pixels are not completely transparent
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            //Then an intersection has been found
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private Dictionary<string, Color[][]> CachePixels()
        {
            Dictionary<string, Color[][]> tmpPixels = new Dictionary<string, Color[][]>();

            foreach (KeyValuePair<string, Animation> pair in animator.Animations)
            {
                Animation animation = pair.Value;
                Color[][] colors = new Color[animation.Frames][];

                for (int i = 0; i < animation.Frames; i++)
                {
                    colors[i] = new Color[animation.Rectangles[i].Width * animation.Rectangles[i].Height];

                    spriteRenderer.Sprite.GetData(0, animation.Rectangles[i], colors[i], 0,
                        animation.Rectangles[i].Width * animation.Rectangles[i].Height);
                }

                tmpPixels.Add(pair.Key, colors);
            }

            return tmpPixels;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class SpriteRenderer: Component, IDrawable, ILoadable
    {
        public Rectangle Rectangle { get; set; }
        public Texture2D Sprite { get; set; }
        public Vector2 Offset { get; set; }
        private string nameOfSprite;
        private float layerDepth;

        public SpriteRenderer(GameObject gameObject, string nameOfSprite, float layerDepth, Vector2 offset):  base (gameObject)
        {
            this.Offset = offset;
            this.nameOfSprite = nameOfSprite;
            this.layerDepth = layerDepth;
            this.Rectangle = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, gameObject.Transformer.Position + Offset, Rectangle, Color.White, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, layerDepth);
        }

        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(nameOfSprite);
        }
          
    }
    
}

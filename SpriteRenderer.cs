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

        public SpriteRenderer(GameObject gameObject, string nameOfSprite, float layerDepth):  base (gameObject)
        {
            this.nameOfSprite = nameOfSprite;
            this.layerDepth = layerDepth;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
           spriteBatch.Draw(Sprite, gameObject.transformer.Position + Offset, Rectangle, Color.White);
        }
        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(nameOfSprite);
        }
          
    }
    
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace MummyDispair
{
    class HealthBar : Component, ILoadable, IDrawable, IUpdateable
    {
        private GameObject player;
        private Vector2 coor;
        private Texture2D rect;
        private Color[] data;
        private int life;

        public int Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
            }
        }
        
        public HealthBar(GameObject player, int life)
        {

            this.Life = life;
            this.player = player;

            Texture2D rect = new Texture2D(GameWorld.Instance.GraphicsDevice, life*50, 20);
            Vector2 coor = new Vector2(player.Transformer.Position.X - 300, player.Transformer.Position.Y - 160);

            Color[] data = new Color[life * 50 * 20];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.DarkGreen;
            }
            rect.SetData(data);

            

            this.coor = coor;
            this.rect = rect;
            this.data = data;
            
            
        }
        
        public void Update()
        {
            coor = new Vector2(player.Transformer.Position.X - 300, player.Transformer.Position.Y - 160);
            Texture2D rect = new Texture2D(GameWorld.Instance.GraphicsDevice, Life * 50, 20);
            Color[] data = new Color[Life * 50 * 20];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Chocolate;
            }
            rect.SetData(data);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rect, coor, Color.White);
        }

        public void LoadContent(ContentManager content)
        {
            
        }
        
    }
}

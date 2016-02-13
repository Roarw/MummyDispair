using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MummyDispair
{
    class LevelCreator
    {

        ContentManager content;

        Director dir = new Director(new WallBuilder());

        public List<GameObject> CreatorObjects { get; set; } = new List<GameObject>();

        public LevelCreator(ContentManager content)
        {
            this.content = content;
        }

        public void AddToList()
        {
            GameObject wall = dir.Construct(Vector2.Zero);
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
        }

    }
}

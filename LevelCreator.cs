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
            GameObject wall = dir.Construct(new Vector2(0, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(100, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(200, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(300, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(400, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(500, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(600, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
            wall = dir.Construct(new Vector2(700, 400));
            wall.LoadContent(content);
            CreatorObjects.Add(wall);
        }

    }
}

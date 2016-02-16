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

        Director dir;

        public List<GameObject> CreatorObjects { get; set; } = new List<GameObject>();

        public LevelCreator(ContentManager content)
        {
            this.content = content;
        }

        public GameObject AddToList()
        {
            GameObject player = RunBuilder(new PlayerBuilder(), Vector2.Zero);

            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(0, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(100, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(200, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(300, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(400, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(500, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(600, 400));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(700, 400));

            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(500, 300));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(600, 300));
            RunBuilder(new WallBuilder("WallBasic.png"), new Vector2(700, 300));

            RunBuilder(new FemaleBuilder(), new Vector2(0, 257));

            RunBuilder(new ScorpionBuilder(), new Vector2(0, 20));


            return player;
        }

        private GameObject RunBuilder(IBuilder build, Vector2 position)
        {
            dir = new Director(build);
            GameObject gameObject = dir.Construct(position);
            gameObject.LoadContent(content);
            CreatorObjects.Add(gameObject);

            return gameObject;
        }

    }
}

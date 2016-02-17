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

        private int previous;
        private int next;
        private int yCoord = -1;

        public LevelCreator(ContentManager content)
        {
            this.content = content;
        }

        public GameObject AddToList()
        {
            WallAt(0, 0);
            WallRange(-1, 1, 1);
            WallRange(-2, 2, 2);
            WallRange(-3, -2, 3); WallRange(2, 3, 3);
            WallRange(-4, -3, 4); WallRange(3, 4, 4);
            WallRange(-5, -4, 5); WallRange(4, 5, 5);
            WallRange(-6, -5, 6); WallRange(5, 6, 6);
            WallRange(-7, -5, 7); WallRange(5, 7, 7);
            WallRange(-8, -5, 8); /*hole*/ WallRange(-3, -1, 8); /*relic hole*/ WallRange(1, 8, 8);
            WallRange(-9, -5, 9); WallRange(8, 9, 9);
            WallRange(-10, -5, 10); WallRange(8, 10, 10);
            WallRange(-11, -5, 11); WallRange(8, 11, 11);
            WallRange(-12, -1, 12); /*relic hole*/ WallRange(1, 6, 12); /*hole*/ WallRange(8, 12, 12);
            WallRange(-13, -12, 13); WallRange(8, 13, 13);
            WallRange(-14, -12, 14); WallRange(8, 14, 14);
            WallRange(-15, -12, 15); WallRange(8, 15, 15);
            WallRange(-16, -12, 16); /*hole*/ WallRange(-10, -5, 16); /*almost relic hole*/ WallRange(-3, 16, 16);
            WallRange(-17, -12, 17); WallRange(14, 17, 17);
            WallRange(-18, -12, 18); WallRange(15, 18, 18);
            WallRange(-19, -12, 19); WallRange(16, 19, 19);
            WallRange(-20, 8, 20); /**/ WallRange(17, 20, 20);
            WallRange(-21, -18, 21); WallAt(8, 21); /*spike*/ WallAt(10, 21); WallRange(17, 20, 21);
            WallRange(-22, -19, 22); WallRange(8, 10, 22); /*spike*/ WallAt(12, 22); WallRange(17, 20, 22);
            WallRange(-27, -20, 23); /*dart->*/WallAt(-10, 23); WallRange(9, 12, 23); /*spike*/ WallAt(14, 23); WallRange(17, 20, 23);
            WallRange(-27, -26, 24); WallAt(-20, 24); /*dart->*/WallAt(-10, 24); WallRange(-9, 5, 24); WallRange(10, 14, 24); /*hole*/ WallRange(16, 20, 24);
            WallRange(-27, -26, 25); WallRange(-16, -12, 25); WallRange(16, 20, 25);
            WallRange(-27, -26, 26); WallRange(-17, -12, 26); WallRange(16, 20, 26);
            WallRange(-27, -12, 27); WallRange(16, 20, 27);
            WallRange(-27, 20, 28);
            WallRange(-13, 17, 29);


            GameObject player = RunBuilder(new PlayerBuilder(), new Vector2(-2400, 2500));

            RunBuilder(new FemaleBuilder(), new Vector2(-2480, 2555));

            RunBuilder(new ScorpionBuilder(new Vector2 (-1500, 2650)), new Vector2(-2300, 2400));

            RunBuilder(new ToiletPaperBuilder(), new Vector2(-2400, 2400));

            return player;
        }

        private void WallRange(int x1, int x2, int y)
        {
            next = x1;
            if (yCoord == y)
            {
                DankWallRange(previous + 1, next - 1, y);
            }
            else
            {
                yCoord = y;
            }
            previous = x2;


            for (int x = x1; x <= x2; x++)
            {
                WallAt(x, y);
            }
        }

        private void DankWallRange(int x1, int x2, int y)
        {
            for (int x = x1; x <= x2; x++)
            {
                DankWallAt(x, y);
            }
        }

        private void WallAt(int x, int y)
        {
            RunBuilder(new WallBuilder("static/WallBasic.png"), new Vector2(x * 100, y * 100));
        }

        private void DankWallAt(int x, int y)
        {
            RunBuilder(new StaticObjectBuilder("static/WallDank.png", 1f), new Vector2(x * 100, y * 100));
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

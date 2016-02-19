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

        private ContentManager content;

        private Director dir;

        private List<GameObject> creatorObjects;
        private List<GameObject> removeLater;

        private int previous;
        private int next;
        private int yCoord = -1;

        public LevelCreator(ContentManager content)
        {
            this.content = content;
            creatorObjects = new List<GameObject>();
            removeLater = new List<GameObject>();
        }

        public List<GameObject> AddToList()
        {
            removeLater.Add(WallAt(0, 8));
            removeLater.Add(WallAt(0, 12));

            WallAt(0, 0);
            WallRange(-1, 1, 1);
            WallRange(-2, 2, 2);
            WallRange(-3, -2, 3); WallRange(2, 3, 3);
            WallRange(-4, -3, 4); WallRange(3, 4, 4);
            WallRange(-5, -4, 5); WallRange(4, 5, 5);
            WallRange(-6, -5, 6); WallRange(5, 6, 6);
            WallRange(-7, -5, 7); WallRange(5, 7, 7);
            WallRange(-8, -5, 8); /*hole*/ WallRange(-3, -1, 8); /*relic hole*/ WallRange(1, 8, 8);
            WallRange(-9, -6, 9); /*dart shooter*/ WallRange(8, 9, 9);
            WallRange(-10, -5, 10); WallRange(8, 10, 10);
            WallRange(-11, -6, 11); /*dart shooter*/ WallRange(8, 11, 11);
            WallRange(-12, -1, 12); /*relic hole*/ WallRange(1, 6, 12); /*hole*/ WallRange(8, 12, 12);
            WallRange(-13, -12, 13); WallRange(8, 13, 13);
            WallRange(-14, -12, 14); WallRange(8, 14, 14);
            WallRange(-15, -13, 15); /*dart shooter*/ WallRange(8, 15, 15);
            WallRange(-16, -12, 16); /*hole*/ WallRange(-10, -5, 16); /*almost relic hole*/ WallRange(-3, 16, 16);
            WallRange(-17, -12, 17); WallRange(14, 17, 17);
            WallRange(-18, -12, 18); WallRange(15, 18, 18);
            WallRange(-19, -12, 19); WallRange(16, 19, 19);
            WallRange(-20, 8, 20); /**/ WallRange(17, 20, 20);
            WallRange(-21, -18, 21); WallAt(8, 21); /*spike*/ WallAt(10, 21); WallRange(17, 20, 21);
            WallRange(-22, -19, 22); WallRange(8, 10, 22); /*spike*/ WallAt(12, 22); WallRange(17, 20, 22);
            WallRange(-27, -20, 23); /*dart shooter*/ WallRange(9, 12, 23); /*spike*/ WallAt(14, 23); WallRange(17, 20, 23);
            WallRange(-27, -26, 24); WallAt(-20, 24); /*dart shooter*/ WallRange(-9, 5, 24); WallRange(10, 14, 24); /*hole*/ WallRange(16, 20, 24);
            WallRange(-27, -26, 25); WallRange(-16, -12, 25); WallRange(16, 20, 25);
            WallRange(-27, -26, 26); WallRange(-17, -12, 26); WallRange(16, 20, 26);
            WallRange(-27, -12, 27); WallRange(16, 20, 27);
            WallRange(-27, 20, 28);
            WallRange(-13, 17, 29);

            PlatformAt(-4, 8); PlatformAt(-4, 9); PlatformAt(-4, 10); PlatformAt(-4, 11);
            PlatformAt(7, 12); PlatformAt(7, 13); PlatformAt(7, 14); PlatformAt(7, 15);
            PlatformAt(-11, 16); PlatformAt(-11, 17); PlatformAt(-11, 18); PlatformAt(-11, 19);
            PlatformAt(15, 24); PlatformAt(15, 25); PlatformAt(15, 26); PlatformAt(15, 27);

            RunBuilder(new ScorpionBuilder(1300 + 11), new Vector2(-900, 2300 + 50)); RunBuilder(new ScorpionBuilder(-1300 + 11), new Vector2(400, 2300 + 50));
            RunBuilder(new ScorpionBuilder(-900 + 11), new Vector2(1500, 2700 + 50));
            RunBuilder(new ScorpionBuilder(-700 + 11), new Vector2(400, 1900 + 50)); RunBuilder(new ScorpionBuilder(-700 + 11), new Vector2(700, 1900 + 50));


            GameObject player = RunBuilder(new PlayerBuilder(), new Vector2(-2400, 2500));

            DartShooterAt(-10, 24, false, 0, player); DartShooterAt(-10, 23, true, 30, player);
            DartShooterAt(5, 25, true, 0, player);
            DartShooterAt(-5, 9, true, 30, player); DartShooterAt(-5, 11, true, 0, player);
            DartShooterAt(-12, 15, true, 15, player);

            PoisonAt(-2, 16); PoisonAt(2, 16);
            PoisonAt(-6, 20); PoisonAt(-8, 20);
            PoisonAt(9, 22); PoisonAt(11, 23); PoisonAt(13, 24);
            PoisonAt(-9, 28); PoisonAt(-6, 28); PoisonAt(-2, 28); PoisonAt(1, 28);

            ToiletPaperAt(-2, 7);
            ToiletPaperAt(5, 23);
            ToiletPaperAt(8, 19);

            RunBuilder(new NecklaceBuilder(), new Vector2(16, 724));

            RunBuilder(new FemaleBuilder(), new Vector2(-2480, 2555));

            return creatorObjects;
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

        private GameObject WallAt(int x, int y)
        {
            return RunBuilder(new WallBuilder("static/BasicWall.png"), new Vector2(x * 100, y * 100));
        }

        private void DankWallAt(int x, int y)
        {
            if (y%3 != 0)
            {
                RunBuilder(new StaticObjectBuilder("static/MarkedWall2.png", 1f), new Vector2(x * 100, y * 100));
            }
            else
            {
                RunBuilder(new StaticObjectBuilder("static/MarkedWall1.png", 1f), new Vector2(x * 100, y * 100));
            }
        }

        private void PlatformAt(int x, int y)
        {
            RunBuilder(new PlatformBuilder("static/Platform.png"), new Vector2(x * 100, y * 100));
        }

        private void DartShooterAt(int x, int y, bool shootRight, float interval, GameObject playerObject)
        {
            if (!shootRight)
            {
                RunBuilder(new DartShooterBuilder(shootRight, interval, playerObject), new Vector2(x * 100 - 5, y * 100));
            }
            else
            {
                RunBuilder(new DartShooterBuilder(shootRight, interval, playerObject), new Vector2(x * 100, y * 100));
            }
        }

        private void PoisonAt(int x, int y)
        {
            RunBuilder(new PoisonBuilder(), new Vector2(x * 100, y * 100 - 3));
        }

        private void ToiletPaperAt(int x, int y)
        {
            RunBuilder(new ToiletPaperBuilder(), new Vector2(x * 100 + 32, y * 100 + 60));
        }
        
        private GameObject RunBuilder(IBuilder build, Vector2 position)
        {
            dir = new Director(build);
            GameObject gameObject = dir.Construct(position);
            gameObject.LoadContent(content);
            creatorObjects.Add(gameObject);

            return gameObject;
        }

        //Should only be run by GameWorld.
        public List<GameObject> NextLevel()
        {
            creatorObjects = new List<GameObject>();

            PlatformAt(6, 24); PlatformAt(6, 25); PlatformAt(6, 26); PlatformAt(6, 27);

            WallAt(5, 26); WallAt(5, 27);
            WallAt(-11, 25); WallAt(-10, 25);

            PoisonAt(-14, 25); PoisonAt(5, 20);

            foreach (GameObject go in removeLater)
            {
                go.IsAlive = false;
            }

            return creatorObjects;
        }
    }
}

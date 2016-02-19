using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class DartShooterBuilder : IBuilder
    {
        private GameObject gameObject;
        private bool shootRight;
        private float interval;
        private GameObject playerObject;

        public DartShooterBuilder(bool shootRight, float interval, GameObject playerObject)
        {
            this.shootRight = shootRight;
            this.interval = interval;
            this.playerObject = playerObject;
        }

        public void BuildGameObject(Vector2 position)
        {
            string picture;
            int xOffset;

            if (!shootRight)
            {
                xOffset = -6;
                picture = "static/DartWall.png";
            }
            else
            {
                xOffset = 0;
                picture = "static/DartWallReverse.png";
            }

            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, picture, 0.3f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new DartShooter(gameObject, shootRight, interval, playerObject));
            Collider collider = new Collider(gameObject, false, xOffset, -6);
            collider.DoCollisionChecks = false;
            gameObject.AddComponent(collider);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

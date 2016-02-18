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

        public DartShooterBuilder()
        {           
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "static/DartWall.png", 0.99f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new DartShooter(gameObject, false));
            Collider collider = new Collider(gameObject, false, -6, -6);
            collider.DoCollisionChecks = false;
            gameObject.AddComponent(collider);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

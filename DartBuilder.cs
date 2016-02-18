using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class DartBuilder : IBuilder
    {
        private GameObject gameObject;
        private int speed;

        public DartBuilder(int speed)
        {
            this.speed = speed;
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "static/Dart.png", 0.5f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Dart(gameObject, speed));
            Collider collider = new Collider(gameObject, true);
            collider.DoCollisionChecks = true;
            gameObject.AddComponent(collider);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

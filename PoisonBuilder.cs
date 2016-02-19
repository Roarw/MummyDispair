using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class PoisonBuilder : IBuilder
    {
        private GameObject gameObject;

        public PoisonBuilder()
        {

        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "static/Poison.png", 0.02f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Poison(gameObject));
            Collider collider = new Collider(gameObject, false);
            collider.DoCollisionChecks = false;
            gameObject.AddComponent(collider);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

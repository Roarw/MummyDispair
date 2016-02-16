using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class FemaleBuilder : IBuilder
    {
        private GameObject gameObject;

        public FemaleBuilder()
        {
        }

        public void BuildGameObject(Vector2 position)
        {
            this.gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "FemaleSpritesheet", 0.1f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Female(gameObject));
            Collider collider = new Collider(gameObject);
            collider.DoCollisionChecks = false;
            gameObject.AddComponent(collider);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class Builder : IBuilder
    {
        private GameObject gameObject;

        public Builder()
        {
        }

        public void BuildGameObject(Vector2 position)
        {
            this.gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "HeroSheet", 0, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Player(gameObject, 100));
            Collider collider = new Collider(gameObject);
            collider.DoCollisionChecks = true;
            gameObject.AddComponent(collider);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

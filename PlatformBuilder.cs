using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class PlatformBuilder : IBuilder
    {
        private GameObject gameObject;
        private string nameOfSprite;

        public PlatformBuilder(string nameOfSprite)
        {
            this.nameOfSprite = nameOfSprite;
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, nameOfSprite, 0.99f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Platform(gameObject));
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

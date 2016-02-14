using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class WallBuilder : IBuilder
    {
        private GameObject gameObject;
        private string nameOfSprite;

        public WallBuilder(string nameOfSprite)
        {
            this.nameOfSprite = nameOfSprite;
        }

        public void BuildGameObject(Vector2 position)
        {
            this.gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, nameOfSprite, 1f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Wall(gameObject));
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

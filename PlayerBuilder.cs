using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;

        public PlayerBuilder()
        {
        }

        public void BuildGameObject(Vector2 position)
        {
            this.gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "MummySpritesheet.png", 0, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new BlankSlade(gameObject, 145));
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

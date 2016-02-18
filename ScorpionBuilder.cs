using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class ScorpionBuilder : IBuilder
    {
        private int patrolX;
        private GameObject gameObject;

        public ScorpionBuilder(int patrolX)
        {
            this.patrolX = patrolX;
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "sheets/ScorpionSpritesheet.png", 0f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Scorpion(gameObject, position, patrolX));
            Collider collider = new Collider(gameObject, true);
            collider.DoCollisionChecks = false;
            gameObject.AddComponent(collider);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

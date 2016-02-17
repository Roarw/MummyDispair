using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class ScorpionBuilder : IBuilder
    {
        
        private Vector2 coord2;
        private GameObject gameObject;
        private Vector2 vector2;

        public ScorpionBuilder(Vector2 coord2)
        {
           
            this.coord2 = coord2;
        }

        public ScorpionBuilder(Vector2 coord2, Vector2 vector2) : this(coord2)
        {
            this.vector2 = vector2;
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "sheets/ScorpionSpritesheet.png", 0f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Scorpion(gameObject, position, coord2));
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

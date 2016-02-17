using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class ToiletPaperBuilder : IBuilder
    {
        private GameObject gameObject;

        public ToiletPaperBuilder()
        {
        }


        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "static/ToiletRoll.png", 0, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new ToiletPaper(gameObject));
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

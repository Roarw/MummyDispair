using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class StaticObjectBuilder : IBuilder
    {
        private GameObject gameObject;
        private string nameOfSprite;
        private float layerDepth;

        public StaticObjectBuilder(string nameOfSprite, float layerDepth)
        {
            this.nameOfSprite = nameOfSprite;
            this.layerDepth = layerDepth;
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, nameOfSprite, layerDepth, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new StaticObject(gameObject));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

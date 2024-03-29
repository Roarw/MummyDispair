﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class DartBuilder : IBuilder
    {
        private GameObject gameObject;
        private int speed;

        public DartBuilder(int speed)
        {
            this.speed = speed;
        }

        public void BuildGameObject(Vector2 position)
        {
            string picture;

            if (speed < 0)
            {
                picture = "static/Dart.png";
            }
            else
            {
                picture = "static/DartReverse.png";
            }

            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, picture, 0.4f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Dart(gameObject, speed));
            Collider collider = new Collider(gameObject, true);
            collider.DoCollisionChecks = true;
            gameObject.AddComponent(collider);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

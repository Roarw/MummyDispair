﻿using Microsoft.Xna.Framework;
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
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "sheets/MummySpritesheet.png", 0.05f, Vector2.Zero));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddTypeComponent(new Player(gameObject, 170));
            gameObject.AddComponent(new HealthBar(gameObject, 5));
            Collider collider = new Collider(gameObject, true, -55, -20, -25);
            collider.DoCollisionChecks = true;
            gameObject.AddComponent(collider);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

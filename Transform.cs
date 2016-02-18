﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    class Transform : Component
    {
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
        }

        public Transform(GameObject gameObject, Vector2 position) 
            : base(gameObject)
        {
            this.position = position;
        }

        public void Translate(Vector2 translation)
        {
            position += translation;
        }
    }
}

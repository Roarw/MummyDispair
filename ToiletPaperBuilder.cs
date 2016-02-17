using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class ToiletPaperBuilder 
    {
        private GameObject gameObject;

        public ToiletPaperBuilder()
        {

        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);

        }
    }
}

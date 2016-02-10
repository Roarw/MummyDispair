using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    abstract class Component
    {
        protected GameObject gameObject;

        public GameObject GetGameObject
        {
            get
            {
                return gameObject;
            }
        }

        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Component()
        {

        }
    }
}

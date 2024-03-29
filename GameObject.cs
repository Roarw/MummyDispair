﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    enum Direction
    {
        Left, Right
    }


    class GameObject : Component, IAnimateable, ICollisionEnter, ICollisionExit, ICollisionStay, IDrawable, IUpdateable, ILoadable
    {
        private List<Component> components;
        public Transform transformer;

        private bool isAlive;
        private TypeComponent typeComp;

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Transform Transformer
        {
            get
            {
                return transformer;
            }
        }

        public TypeComponent TypeComponent
        {
            get
            {
                return typeComp;
            }
        }

        public GameObject(Vector2 position)
        {
            this.components = new List<Component>();

            this.transformer = new Transform(this, position);
            components.Add(transformer);
            
            isAlive = true;
        }

        public void AddTypeComponent(TypeComponent typeComp)
        {
            this.typeComp = typeComp;
            AddComponent(typeComp);
        }

        public void AddComponent(Component comp)
        {
            components.Add(comp);
        }

        public Component GetComponent(string compName)
        {
            Component comp = components.Find(x => x.GetType().Name == compName);

            if (comp == null)
            {
                System.Diagnostics.Debug.WriteLine(compName + " returned null.");
            }

            return comp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component comp in components)
            {
                if (comp is IDrawable)
                {
                    (comp as IDrawable).Draw(spriteBatch);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Component comp in components)
            {
                if (comp is ILoadable)
                {
                    (comp as ILoadable).LoadContent(content);
                }
            }
        }

        public void OnAnimationDone(string animationName)
        {
            foreach (Component comp in components)
            {
                if (comp is IAnimateable)
                {
                    (comp as IAnimateable).OnAnimationDone(animationName);
                }
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            foreach (Component comp in components)
            {
                if (comp is ICollisionEnter)
                {
                    (comp as ICollisionEnter).OnCollisionEnter(other);
                }
            }
        }

        public void OnCollisionExit(Collider other)
        {
            foreach (Component comp in components)
            {
                if (comp is ICollisionExit)
                {
                    (comp as ICollisionExit).OnCollisionExit(other);
                }
            }
        }

        public void OnCollisionStay(Collider other)
        {
            foreach (Component comp in components)
            {
                if (comp is ICollisionStay)
                {
                    (comp as ICollisionStay).OnCollisionStay(other);
                }
            }
        }

        public void Update()
        {
            foreach (Component comp in components)
            {
                if (comp is IUpdateable)
                {
                    (comp as IUpdateable).Update();
                }
            }
        }
    }
}

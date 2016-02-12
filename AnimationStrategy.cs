using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    interface AnimationStrategy
    {
        void Update(Direction direction, Vector2 translation);
    }
}

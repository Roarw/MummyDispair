﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    interface IBuilder
    {
        GameObject GetResult();

        void BuildGameObject(Vector2 position);
    }
}

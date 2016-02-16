using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    interface Camera
    {
        Matrix CameraMatrix { get; }
        void UpdateCameraMatrix();
    }
}

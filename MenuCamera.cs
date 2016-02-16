using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class MenuCamera : Camera
    {
        private Matrix cameraMatrix;

        public Matrix CameraMatrix
        {
            get
            {
                return cameraMatrix;
            }
        }

        public MenuCamera()
        {
            UpdateCameraMatrix();
        }

        public void UpdateCameraMatrix()
        {
            cameraMatrix = Matrix.CreateTranslation(0, 0, 0.0f);
        }
    }
}

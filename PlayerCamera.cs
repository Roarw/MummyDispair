using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class PlayerCamera : Camera
    {
        private Matrix cameraMatrix;
        private GameObject player;
        private Vector2 halfScreen;

        public Matrix CameraMatrix
        {
            get
            {
                return cameraMatrix;
            }
        }

        public PlayerCamera(GameObject player)
        {
            this.player = player;
            halfScreen = new Vector2((780 - 165) * 0.5f, (500 - 165) * 0.5f);
            UpdateCameraMatrix();
        }

        public void UpdateCameraMatrix()
        {
            cameraMatrix = Matrix.CreateTranslation(halfScreen.X - player.Transformer.Position.X, 
                halfScreen.Y - player.Transformer.Position.Y, 0.0f);
        }
    }
}

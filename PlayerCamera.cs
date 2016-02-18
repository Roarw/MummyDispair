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
            halfScreen = new Vector2((GameWorld.Instance.ScreenWidth - 165) * 0.5f, (GameWorld.Instance.ScreenHeight - 165) * 0.5f);
            UpdateCameraMatrix();
        }

        public void UpdateCameraMatrix()
        {
            cameraMatrix = Matrix.CreateTranslation(halfScreen.X - player.Transformer.Position.X, 
                halfScreen.Y - player.Transformer.Position.Y, 0.0f);
        }
    }
}

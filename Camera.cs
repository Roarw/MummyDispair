using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MummyDispair
{
    class Camera
    {
        public Matrix cameraMatrix;
        private Vector2 playerPosition;
        private Vector2 halfScreen;
        Player player;



        public Camera()
        {

            halfScreen = new Vector2(600 * 0.5f, 0 * 0.5f);
            UpdateCameraMatrix();
        }

        public Vector2 Pos
        {
            get
            {
                return playerPosition;
            }

            set
            {
                playerPosition = player.GetGameObject.transformer.Position;
                UpdateCameraMatrix();
            }
        }

        private void UpdateCameraMatrix()
        {

            cameraMatrix = Matrix.CreateTranslation(halfScreen.X - playerPosition.X, halfScreen.Y - playerPosition.Y, 0.0f);
        }
    }
}

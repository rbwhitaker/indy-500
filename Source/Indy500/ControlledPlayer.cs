using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Indy500
{
    class ControlledPlayer : IPlayer
    {
        private Keys LeftKey = GameSettings.LeftKey;
        private Keys RightKey = GameSettings.RightKey;
        private Keys Accelerate = GameSettings.Accelerate;

        public float AccelerationAmount { get; set;}
        public float TurnAmount { get; set; }

        public PlayerInput Update(GameTime gameTime, Race race, Car carToControl)
        {
            TurnAmount = 0f;
            AccelerationAmount = 0f;

            if (Keyboard.GetState().IsKeyDown(LeftKey))
                TurnAmount = -1f;
            if (Keyboard.GetState().IsKeyDown(RightKey))
                TurnAmount =  1f;

            if (Keyboard.GetState().IsKeyDown(Accelerate))
                AccelerationAmount = 1f;

            return new PlayerInput(AccelerationAmount, TurnAmount);
        }
    }
}
